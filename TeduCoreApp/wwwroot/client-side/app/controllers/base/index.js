var BaseController = function () {
    this.initialize = function () {
        registerEvents();
        loadHeaderCart();
    }

    function registerEvents() {
        $('body').on('click', '.btn-cart', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: '/Cart/AddToCart',
                type: 'post',
                data: {
                    productId: id,
                    quantity: 1,
                    color: 1,
                    size: 1
                },
                success: function (response) {
                    tedu.notify('The product was added to cart', 'success');
                    loadHeaderCart();
                    //loadMyCart();
                }
            });
        });

        $('body').on('click', '.remove-cart', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: '/Cart/RemoveFromCart',
                type: 'post',
                data: {
                    productId: id
                },
                success: function (response) {
                    tedu.notify('The product was removed', 'success');
                    //loadHeaderCart();
                    loadMyCart();
                }
            });
        });


        $('body').on('click', '.link-quickview', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: '/AjaxContent/SetIdProduct',
                type: 'post',
                data: {
                    productId: id
                },
                success: function (response) {
                    if (parseInt(response) > 0) {
                        $.when(loadQuickReView())
                            .done(function () {
                                $('#quick_view_popup-overlay').show();
                                $('#show-review').show();
                            });
                    }
                }
            });
        });

        $('body').on('click', '#quick_view_popup-close', function (e) {
            e.preventDefault();
            $('#quick_view_popup-overlay').hide();
            $('#show-review').hide();
        })

    }

    function loadHeaderCart() {
        $("#header-cart").load("/AjaxContent/HeaderCart");
    }

    function loadQuickReView() {
        $("#quick_view_popup-wrap").load("/AjaxContent/QuickReView");
    }
}