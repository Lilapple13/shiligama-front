// map-helpers.js — Leaflet + Nominatim for address selection

let _map = null;
let _marker = null;
let _dotnetRef = null;

window.mapHelpers = {

    initMap: function (dotnetRef) {
        _dotnetRef = dotnetRef;

        // Destroy previous instance if any
        if (_map) {
            _map.remove();
            _map = null;
            _marker = null;
        }

        // Small delay to ensure the modal div is visible and sized
        setTimeout(() => {
            _map = L.map('address-map').setView([-12.0464, -77.0428], 13); // Lima, Peru

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '© <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>',
                maxZoom: 19
            }).addTo(_map);

            _map.on('click', async function (e) {
                const { lat, lng } = e.latlng;

                // Place/move marker
                if (_marker) {
                    _marker.setLatLng([lat, lng]);
                } else {
                    _marker = L.marker([lat, lng]).addTo(_map);
                }

                // Reverse geocode with Nominatim
                try {
                    const res = await fetch(
                        `https://nominatim.openstreetmap.org/reverse?lat=${lat}&lon=${lng}&format=json`,
                        { headers: { 'Accept-Language': 'es' } }
                    );
                    const data = await res.json();
                    const address = data.display_name || `${lat.toFixed(5)}, ${lng.toFixed(5)}`;
                    _marker.bindPopup(`<b>${address}</b>`).openPopup();
                    await _dotnetRef.invokeMethodAsync('OnMapAddressSelected', address);
                } catch {
                    const fallback = `${lat.toFixed(5)}, ${lng.toFixed(5)}`;
                    await _dotnetRef.invokeMethodAsync('OnMapAddressSelected', fallback);
                }
            });

            // Force map to recalculate its size after modal opens
            _map.invalidateSize();
        }, 300);
    },

    destroyMap: function () {
        if (_map) {
            _map.remove();
            _map = null;
            _marker = null;
        }
    }
};
