(function ($) {
    "use strict";
    
    // Dropdown on mouse hover
    $(document).ready(function () {
        function toggleNavbarMethod() {
            if ($(window).width() > 992) {
                $('.navbar .dropdown').on('mouseover', function () {
                    $('.dropdown-toggle', this).trigger('click');
                }).on('mouseout', function () {
                    $('.dropdown-toggle', this).trigger('click').blur();
                });
            } else {
                $('.navbar .dropdown').off('mouseover').off('mouseout');
            }

        }
        toggleNavbarMethod();
        $(window).resize(toggleNavbarMethod);
    });
    
    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({scrollTop: 0}, 1500, 'easeInOutExpo');
        return false;
    });


    // Vendor carousel
    $('.vendor-carousel').owlCarousel({
        loop: true,
        margin: 29,
        nav: false,
        autoplay: true,
        smartSpeed: 1000,
        responsive: {
            0:{
                items:2
            },
            576:{
                items:3
            },
            768:{
                items:4
            },
            992:{
                items:5
            },
            1200:{
                items:6
            }
        }
    });


    // Related carousel
    $('.related-carousel').owlCarousel({
        loop: true,
        margin: 29,
        nav: false,
        autoplay: true,
        smartSpeed: 1000,
        responsive: {
            0:{
                items:1
            },
            576:{
                items:2
            },
            768:{
                items:3
            },
            992:{
                items:4
            }
        }
    });

    // Product Quantity
    $('.quantity button').on('click', function () {
        var button = $(this);
        var oldValue = button.parent().parent().find('input').val();
        if (button.hasClass('btn-plus')) {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            if (oldValue > 1) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 1;
            }
        }
        button.parent().parent().find('input').val(newVal);
    });

    // Fill Date of Birth (Ngày, Tháng, Năm)
    $(document).ready(function () {
        fillDateOfBirth();

        function fillDateOfBirth() {
            // Lấy ngày, tháng, năm hiện tại
            var today = new Date();
            var currentDay = today.getDate();
            var currentMonth = today.getMonth() + 1; // tháng bắt đầu từ 0, nên cộng thêm 1
            var currentYear = today.getFullYear();

            // Ngày
            $('#day').empty().append('<option value="" disabled selected>Ngày</option>');
            for (var i = 1; i <= 31; i++) {
                var selected = (i === currentDay) ? 'selected' : '';
                $('#day').append('<option value="' + i + '" ' + selected + '>' + i + '</option>');
            }

            // Tháng
            $('#month').empty().append('<option value="" disabled selected>Tháng</option>');
            for (var i = 1; i <= 12; i++) {
                var selected = (i === currentMonth) ? 'selected' : '';
                $('#month').append('<option value="' + i + '" ' + selected + '>' + i + '</option>');
            }

            // Năm
            $('#year').empty().append('<option value="" disabled selected>Năm</option>');
            for (var i = currentYear; i >= 1900; i--) {
                var selected = (i === currentYear) ? 'selected' : '';
                $('#year').append('<option value="' + i + '" ' + selected + '>' + i + '</option>');
            }
        }
    });

        // Hàm kiểm tra tính hợp lệ của ngày tháng năm
        function isValidDate(day, month, year) {
            // Kiểm tra nếu ngày, tháng, năm là số và hợp lệ
            if (isNaN(day) || isNaN(month) || isNaN(year)) {
                return false;
            }

            // Kiểm tra tháng hợp lệ (1-12)
            if (month < 1 || month > 12) {
                return false;
            }

            // Kiểm tra số ngày trong tháng
            var daysInMonth = getDaysInMonth(month, year);

            // Kiểm tra ngày trong tháng có hợp lệ
            if (day < 1 || day > daysInMonth) {
                return false;
            }

            return true;
        }

        // Hàm trả về số ngày trong một tháng của năm cụ thể
        function getDaysInMonth(month, year) {
            // Lập tháng (tháng 1 bắt đầu từ 0)
            return new Date(year, month, 0).getDate();
        }
    });

    $(document).ready(function () {
        // Lắng nghe sự kiện thay đổi khi chọn radio button
        $('input[name="cancelReason"]').on('change', function () {
            var otherReasonDiv = $('#otherReasonDiv');
            if ($(this).val() === 'other') {
                otherReasonDiv.show();
            } else {
                otherReasonDiv.hide();
            }
        });
    });


    $(document).ready(function () {
        // Lắng nghe sự kiện khi checkbox "Chọn tất cả" thay đổi
        $('#selectAll').on('change', function () {
            // Lấy tất cả các checkbox sản phẩm
            var checkboxes = $('.product-checkbox');

            // Lặp qua tất cả checkbox và thay đổi trạng thái tương ứng với checkbox "Chọn tất cả"
            checkboxes.each(function () {
                $(this).prop('checked', $('#selectAll').prop('checked'));
            });
        });

        // Lắng nghe sự kiện khi một sản phẩm nào đó được chọn hoặc bỏ chọn
        $('.product-checkbox').on('change', function () {
            // Kiểm tra xem có tất cả các checkbox được chọn không
            var allChecked = true;
            $('.product-checkbox').each(function () {
                if (!$(this).prop('checked')) {
                    allChecked = false;
                }
            });

            // Nếu tất cả các checkbox được chọn, đặt checkbox "Chọn tất cả" thành checked
            $('#selectAll').prop('checked', allChecked);
        });
    });

    $(document).ready(function () {

        // function thêm sản phẩm vào trong giỏ hàng
        // Thêm vào các tham số đầu vào
        function AddToCart () {

        }

        // Hiển thị thông tin từ sản phẩm được chọn lên trang thanh toán
        // Thêm vào các tham số đầu vào
        function LoadCheckout() {

        }

        // Xử lý khi click vào nút thêm vào giỏ hàng trong product detail
        $('#AddToCart').on('click', function () {
            AddToCart();
        });

        // Xử lý khi click vào nút  mua hàng trong product detail
        $('#CartAndPurchase').on('click', function () {
            // Gọi lại để thêm vào giỏ hàng
            AddToCart();

            // xử lý hiển thị thông tin cho trang khi click vào

            // Checked vào sản phẩm vừa thêm vào

            // Hiển thị thông tin từ sản phẩm được chọn lên trang thanh toán
            LoadCheckout();

            // Lấy URL từ thuộc tính data-redirect-url của nút
            var redirectUrl = $(this).data('redirect-url');

            // Chuyển hướng người dùng đến trang Checkout
            window.location.href = redirectUrl;
        });

        // Xử lý khi click vào nút mua hàng trong shoping cart
        $('#purchase').on('click', function () {

            // xử lý hiển thị thông tin cho trang đặt hàng khi click vào
            LoadCheckout();

            // Lấy URL từ thuộc tính data-redirect-url của nút
            var redirectUrl = $(this).data('redirect-url');

            // Chuyển hướng người dùng đến trang Checkout
            window.location.href = redirectUrl;
        });
    });

    // Xử lý khi click vào nút đặt hàng
    $(document).ready(function () {
        $('#orderStatusModal').on('show.bs.modal', function () {
            // Giả lập kiểm tra thông tin đặt hàng
            const isSuccess = Math.random() > 0.5;

            // Đặt tiêu đề và mô tả cho modal
            if (isSuccess) {
                $('#orderStatusTitle').text('Thành công');
                $('#orderStatusDescription').text('Bạn đã đặt hàng thành công. Sau khi bấm OK, bạn sẽ được chuyển về trang chủ.');
            } else {
                $('#orderStatusTitle').text('Thất bại');
                $('#orderStatusDescription').text('Đặt hàng thất bại. Vui lòng kiểm tra thông tin và thử lại.');
            }
        });

        $('#orderModalOk').on('click', function () {
            const title = $('#orderStatusTitle').text();
            if (title === 'Thành công') {
                var redirectUrl = $('#OrderProduct').data('redirect-url');
                // Chuyển về trang chủ nếu thành công
                window.location.href = redirectUrl;
            }
        });
    });

    $(document).ready(function () {
        // Số lượng sản phẩm giả định (có thể lấy từ API hoặc dữ liệu sản phẩm)
        var availableQuantity = 12; // Thay đổi theo số lượng thực tế

        // Kiểm tra ngay khi tải trang nếu sản phẩm có sẵn hay không
        if (availableQuantity === 0) {
            $('#errorMessage .error-message').text("Hiện đang hết hàng!").show();
            $('#errorMessage').removeAttr('hidden');  // Hiển thị thông báo
            $('#AddToCart').prop('disabled', true);
            $('#CartAndPurchase').prop('disabled', true);
        }


        // Lắng nghe sự kiện khi thay đổi giá trị số lượng
        $('#productQuantity').on('input', function () {
            var requestedQuantity = parseInt($(this).val());

            // Kiểm tra nếu số lượng yêu cầu lớn hơn số lượng có sẵn
            if (requestedQuantity > availableQuantity) {
                    
                return;  
            }
        });
    });


})(jQuery);

