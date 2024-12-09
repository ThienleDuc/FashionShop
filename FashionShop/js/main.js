(function ($) {
    "use strict";

    // Dropdown on hover
    $(document).ready(function () {
        function toggleNavbar() {
            if ($(window).width() > 992) {
                $('.navbar .dropdown').hover(
                    function () { $('.dropdown-toggle', this).trigger('click'); },
                    function () { $('.dropdown-toggle', this).trigger('click').blur(); }
                );
            } else {
                $('.navbar .dropdown').off('mouseenter mouseleave');
            }
        }
        toggleNavbar();
        $(window).resize(toggleNavbar);
    });

    // Back to top button
    $(window).scroll(function () {
        $(this).scrollTop() > 100 ? $('.back-to-top').fadeIn('slow') : $('.back-to-top').fadeOut('slow');
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 1500, 'easeInOutExpo');
    });

    // Owl Carousel
    function initCarousel(selector, itemsConfig) {
        $(selector).owlCarousel({
            loop: true,
            margin: 29,
            nav: false,
            autoplay: true,
            smartSpeed: 1000,
            responsive: itemsConfig
        });
    }
    initCarousel('.vendor-carousel', { 0: { items: 2 }, 576: { items: 3 }, 768: { items: 4 }, 992: { items: 5 }, 1200: { items: 6 } });
    initCarousel('.related-carousel', { 0: { items: 1 }, 576: { items: 2 }, 768: { items: 3 }, 992: { items: 4 } });

    // Quantity buttons
    $('.quantity button').on('click', function () {
        var input = $(this).closest('.quantity').find('input');
        var value = parseFloat(input.val());
        input.val($(this).hasClass('btn-plus') ? value + 1 : Math.max(1, value - 1));
    });

    // Toggle other reason field
    $('input[name="cancelReason"]').change(function () {
        $('#otherReasonDiv').toggle($(this).val() === 'other');
    });

    // Select all checkbox
    $('#selectAll').change(function () {
        $('.product-checkbox').prop('checked', $(this).prop('checked'));
    });
    $('.product-checkbox').change(function () {
        $('#selectAll').prop('checked', $('.product-checkbox:checked').length === $('.product-checkbox').length);
    });

    // Cart actions
    function addToCart() { /* Add product to cart logic */ }
    function loadCheckout() { /* Load checkout details logic */ }

    $('#AddToCart').click(addToCart);
    $('#CartAndPurchase, #purchase').click(function () {
        addToCart();
        loadCheckout();
        window.location.href = $(this).data('redirect-url');
    });

    // Order status modal
    $('#orderStatusModal').on('show.bs.modal', function () {
        var isSuccess = Math.random() > 0.5;
        $('#orderStatusTitle').text(isSuccess ? 'Thành công' : 'Thất bại');
        $('#orderStatusDescription').text(isSuccess ?
            'Bạn đã đặt hàng thành công. Sau khi bấm OK, bạn sẽ được chuyển về trang chủ.' :
            'Đặt hàng thất bại. Vui lòng kiểm tra thông tin và thử lại.');
    });
    $('#orderModalOk').click(function () {
        if ($('#orderStatusTitle').text() === 'Thành công') {
            window.location.href = $('#OrderProduct').data('redirect-url');
        }
    });

    // Product availability check
    $(function () {
        var availableQuantity = 12;
        if (availableQuantity === 0) {
            $('#errorMessage').removeAttr('hidden').find('.error-message').text("Hiện đang hết hàng!");
            $('#AddToCart, #CartAndPurchase').prop('disabled', true);
        }
        $('#productQuantity').on('input', function () {
            if (+$(this).val() > availableQuantity) {
                $(this).val(availableQuantity);
            }
        });
    });

    $(function () {
        var maNganHangLienKet = null;  // Mã ngân hàng liên kết
        var soTaiKhoan = null;          // Số tài khoản ngân hàng

        // Khi chọn ngân hàng từ dropdown
        $('#bankSelect').change(function () {
            maNganHangLienKet = parseInt($(this).val(), 10);  // Lấy giá trị mã ngân hàng liên kết

            // Gửi yêu cầu AJAX để lấy danh sách chi nhánh của ngân hàng
            $.ajax({
                url: '/MyAccount/LayMaChiNhanh',  // Địa chỉ API để lấy danh sách chi nhánh
                type: 'GET',
                data: { maNganHangLienKet: maNganHangLienKet },  // Gửi mã ngân hàng liên kết
                success: function (data) {
                    console.log(data);  // Debug: kiểm tra dữ liệu trả về

                    // Xóa các chi nhánh cũ trong dropdown
                    $('#branch').empty();

                    // Thêm lại placeholder cho dropdown chi nhánh
                    $('#branch').append('<option value="" disabled selected>Chọn Chi Nhánh</option>');

                    // Duyệt qua danh sách chi nhánh trả về và thêm vào dropdown
                    $.each(data, function (index, item) {
                        $('#branch').append(`<option value="${item.MaChiNhanh}">${item.TenChiNhanh}</option>`);
                    });
                },
                error: function (xhr, status, error) {
                    console.error("Lỗi khi gọi API: ", status, error);
                    alert('Lỗi khi tải chi nhánh ngân hàng');
                }
            });
        });

        // Khi người dùng rời khỏi trường nhập số tài khoản
        $('#accountNumber').on('blur', function () {
            soTaiKhoan = $(this).val();  // Lấy giá trị số tài khoản từ trường nhập

            if (soTaiKhoan) {
                // Gửi yêu cầu AJAX để lấy tên chủ sở hữu tài khoản
                $.ajax({
                    url: '/MyAccount/GetTenChuSoHuuBySoTaiKhoan',  // Địa chỉ API để lấy tên chủ sở hữu
                    type: 'GET',
                    data: { soTaiKhoan: soTaiKhoan, maNganHangLienKet: maNganHangLienKet },  // Gửi số tài khoản và mã ngân hàng liên kết
                    success: function (data) {
                        if (data.success) {
                            // Nếu trả về tên chủ sở hữu, hiển thị
                            $('#accountOwner').text('Chủ sở hữu: ' + data.tenChuSoHuu);
                        } else {
                            // Nếu không có kết quả, hiển thị thông báo không tồn tại tài khoản này
                            $('#accountOwner').removeAttr('hidden');
                            $('#accountOwner').text('Không tồn tại tài khoản này.');
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error("Lỗi khi gọi API: ", status, error);
                        $('#accountOwner').text('Lỗi khi kiểm tra tài khoản.');
                    }
                });
            }
        });
    });


    $(document).ready(function () {
        // Khi modal delete được mở
        $('#deleteBankModal').on('show.bs.modal', function (e) {
            var maTaiKhoan = $(e.relatedTarget).data('maTaiKhoan');
            if (maTaiKhoan) {
                // Chuyển giá trị sang int (nếu cần) và gán vào input hidden
                $('#maTaiKhoanToDelete').val(parseInt(maTaiKhoan));
            }
        });

        // Khi người dùng xác nhận xóa, form sẽ được submit
        $('#deleteBankForm').on('submit', function (e) {
            e.preventDefault();
            var maTaiKhoan = $('#maTaiKhoanToDelete').val();
            if (maTaiKhoan) {
                // Xử lý gửi request POST đến controller
                // Chạy thêm các logic AJAX hoặc form submit thông thường.
                this.submit();
            }
        });
    });


})(jQuery);
