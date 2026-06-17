// checkout-guard.js — intercept browser back while checkout is active

window.checkoutGuard = {
    _dotnetRef: null,
    _popStateHandler: null,

    enable: function (dotnetRef) {
        this._dotnetRef = dotnetRef;
        history.pushState({ checkoutGuard: true }, '', location.href);

        this._popStateHandler = () => {
            if (!this._dotnetRef) return;
            history.pushState({ checkoutGuard: true }, '', location.href);
            this._dotnetRef.invokeMethodAsync('OnBrowserBackAttempt');
        };

        window.addEventListener('popstate', this._popStateHandler);
    },

    disable: function () {
        if (this._popStateHandler) {
            window.removeEventListener('popstate', this._popStateHandler);
            this._popStateHandler = null;
        }
        this._dotnetRef = null;
    }
};
