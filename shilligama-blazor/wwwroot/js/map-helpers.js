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
                    
                    // Sincronizar texto del input
                    const input = document.getElementById('map-search-input') || document.getElementById('map-search-input-checkout');
                    if (input) {
                        input.value = address;
                    }

                    _marker.bindPopup(`<b>${address}</b>`).openPopup();
                    await _dotnetRef.invokeMethodAsync('OnMapAddressSelected', address);
                } catch {
                    const fallback = `${lat.toFixed(5)}, ${lng.toFixed(5)}`;
                    await _dotnetRef.invokeMethodAsync('OnMapAddressSelected', fallback);
                }
            });

            // Configurar buscador dinámico con sugerencias de autocompletado
            const input = document.getElementById('map-search-input') || document.getElementById('map-search-input-checkout');
            if (input) {
                input.value = ""; // Limpiar al iniciar
                
                // Crear contenedor de sugerencias
                let suggestionsBox = document.createElement('div');
                suggestionsBox.className = 'list-group position-absolute w-100 shadow-sm';
                suggestionsBox.style.zIndex = '9999';
                suggestionsBox.style.maxHeight = '200px';
                suggestionsBox.style.overflowY = 'auto';
                suggestionsBox.style.marginTop = '2px';
                suggestionsBox.style.display = 'none';
                input.parentNode.appendChild(suggestionsBox);

                let debounceTimer = null;
                input.oninput = function () {
                    clearTimeout(debounceTimer);
                    const query = input.value.trim();
                    if (query.length > 1) {
                        debounceTimer = setTimeout(() => {
                            window.mapHelpers.getSuggestions(query, suggestionsBox, input);
                        }, 200);
                    } else {
                        suggestionsBox.innerHTML = "";
                        suggestionsBox.style.display = 'none';
                    }
                };

                // Ocultar sugerencias al hacer clic fuera
                document.addEventListener('click', function (e) {
                    if (e.target !== input && !suggestionsBox.contains(e.target)) {
                        suggestionsBox.innerHTML = "";
                        suggestionsBox.style.display = 'none';
                    }
                });
            }

            // Force map to recalculate its size after modal opens
            _map.invalidateSize();
        }, 300);
    },

    clearSearchInput: function () {
        const input = document.getElementById('map-search-input') || document.getElementById('map-search-input-checkout');
        if (input) {
            input.value = '';
            const suggestionsBox = input.parentNode.querySelector('.list-group');
            if (suggestionsBox) {
                suggestionsBox.innerHTML = '';
                suggestionsBox.style.display = 'none';
            }
        }

        if (_marker && _map) {
            _map.removeLayer(_marker);
            _marker = null;
        }

        if (_dotnetRef) {
            _dotnetRef.invokeMethodAsync('OnMapAddressSelected', '');
        }
    },

    destroyMap: function () {
        if (_map) {
            _map.remove();
            _map = null;
            _marker = null;
        }
    },

    getSuggestions: async function (query, suggestionsBox, input) {
        if (!query) return;
        try {
            const res = await fetch(
                `https://nominatim.openstreetmap.org/search?q=${encodeURIComponent(query)}&format=json&limit=5`,
                { headers: { 'Accept-Language': 'es' } }
            );
            const data = await res.json();
            suggestionsBox.innerHTML = "";

            if (data && data.length > 0) {
                suggestionsBox.style.display = 'block';
                data.forEach(item => {
                    let btn = document.createElement('button');
                    btn.type = 'button';
                    btn.className = 'list-group-item list-group-item-action py-2 px-3 text-start small text-truncate';
                    btn.style.fontSize = '0.85rem';
                    btn.style.border = '1px solid #e5e7eb';
                    btn.style.cursor = 'pointer';
                    btn.innerHTML = `<i class="fa-solid fa-location-dot me-2 text-muted"></i>${item.display_name}`;

                    btn.onclick = async function () {
                        input.value = item.display_name;
                        suggestionsBox.innerHTML = "";
                        suggestionsBox.style.display = 'none';

                        const latitude = parseFloat(item.lat);
                        const longitude = parseFloat(item.lon);

                        _map.setView([latitude, longitude], 16);

                        if (_marker) {
                            _marker.setLatLng([latitude, longitude]);
                        } else {
                            _marker = L.marker([latitude, longitude]).addTo(_map);
                        }

                        _marker.bindPopup(`<b>${item.display_name}</b>`).openPopup();
                        if (_dotnetRef) {
                            await _dotnetRef.invokeMethodAsync('OnMapAddressSelected', item.display_name);
                        }
                    };
                    suggestionsBox.appendChild(btn);
                });
            } else {
                suggestionsBox.style.display = 'none';
            }
        } catch (err) {
            console.error("Error obteniendo sugerencias:", err);
        }
    }
};
