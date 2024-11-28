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

    // Login account
    $(document).ready(function () {
        $('#loginForm').submit(function (event) {
            event.preventDefault();  // Ngừng gửi form theo cách truyền thống

            var username = $('#username').val();
            var password = $('#password').val();

            // Kiểm tra thông tin đăng nhập (nếu trống thì hiển thị lỗi)
            if (username === "" || password === "") {
                $('#errorMessage .error-message').text("Tên người dùng và mật khẩu không được để trống.").show();
                $('#errorMessage').removeAttr('hidden');
                return;
            }

            // Gửi thông tin đăng nhập bằng AJAX
            $.ajax({
                type: 'POST',
                url: $('#loginForm').data('action-url'),  // Lấy URL từ data-action-url của form
                data: {
                    username: username,
                    password: password
                },
                success: function (response) {
                    if (response.success) {
                        // Nếu đăng nhập thành công, chuyển hướng đến trang đã định sẵn
                        var redirectUrl = $('#btnLogin').data('redirect-url'); // Lấy URL từ data-redirect-url của nút button
                        window.location.href = redirectUrl;
                    } else {
                        // Nếu đăng nhập không thành công, hiển thị thông báo lỗi
                        $('#errorMessage .error-message').text("Tên đăng nhập hoặc mật khẩu không chính xác.").show();
                        $('#errorMessage').removeAttr('hidden');
                    }
                },
                error: function () {
                    $('#errorMessage .error-message').text("Đã có lỗi xảy ra. Vui lòng thử lại.").show();
                    $('#errorMessage').removeAttr('hidden');
                }
            });
        });
    });

    // Logout account
    $(document).ready(function () {
        $('#registerForm').submit(function (event) {
            event.preventDefault();  // Ngừng gửi form theo cách truyền thống

            var firstName = $('#firstName').val();
            var lastName = $('#lastName').val();
            var username = $('#username').val();
            var password = $('#password').val();
            var confirmPassword = $('#confirmPassword').val();
            var gender = $('input[name="gender"]:checked').val();  // Lấy giới tính đã chọn
            var day = $('#day').val();
            var month = $('#month').val();
            var year = $('#year').val();

            // Kiểm tra thông tin đăng ký (nếu trống thì hiển thị lỗi)
            if (firstName === "" || lastName === "" || username === "" || password === "" || confirmPassword === "" || !gender || !day || !month || !year) {
                $('#errorMessage .error-message').text("Vui lòng điền đầy đủ thông tin.").show();
                $('#errorMessage').removeAttr('hidden');
                return;
            }

            // Kiểm tra mật khẩu và xác nhận mật khẩu
            if (password !== confirmPassword) {
                $('#errorMessage .error-message').text("Mật khẩu và xác nhận mật khẩu không khớp.").show();
                $('#errorMessage').removeAttr('hidden');
                return;
            }

            // Kiểm tra ngày tháng năm hợp lệ
            if (!isValidDate(day, month, year)) {
                $('#errorMessage .error-message').text("Ngày tháng năm không hợp lệ. Vui lòng kiểm tra lại.").show();
                $('#errorMessage').removeAttr('hidden');
                return;
            }

            // Gửi thông tin đăng ký bằng AJAX
            $.ajax({
                type: 'POST',
                url: $('#registerForm').data('action-url'),  // Lấy URL từ data-action-url của form
                data: {
                    firstName: firstName,
                    lastName: lastName,
                    username: username,
                    password: password,
                    gender: gender,
                    day: day,
                    month: month,
                    year: year
                },
                success: function (response) {
                    if (response.success) {
                        // Nếu đăng ký thành công, chuyển hướng đến trang đăng nhập
                        var redirectUrl = $('#btnRegister').data('redirect-url'); // Lấy URL từ data-redirect-url của nút button
                        window.location.href = redirectUrl;
                    } else {
                        // Nếu đăng ký không thành công, hiển thị thông báo lỗi
                        $('#errorMessage .error-message').text("Đăng ký thất bại, vui lòng thử lại.").show();
                        $('#errorMessage').removeAttr('hidden');
                    }
                },
                error: function () {
                    $('#errorMessage .error-message').text("Đã có lỗi xảy ra. Vui lòng thử lại.").show();
                    $('#errorMessage').removeAttr('hidden');
                }
            });
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


})(jQuery);

