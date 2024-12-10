(function ($) {
    "use strict";

    $(document).ready(function () {
        // Dropdown on hover
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
        // ----------------------------------------------------------------------------------------------------------------------------------------------------------//
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


         // --------------------------------------------------------------------------Start Thiện-----------------------------------------------------------------------------//

        // Bank and Account form handling
        $('#bankSelect').change(function () {
            var maNganHangLienKet = $(this).val();
            if (maNganHangLienKet) {
                $.get('/MyAccount/GetChiNhanhByNganHang', { maNganHangLienKet }, function (response) {
                    var branchSelect = $('#branch');
                    branchSelect.empty().append('<option value="" disabled selected>Chọn Chi Nhánh</option>');
                    if (response.success) {
                        $.each(response.data, function (index, branch) {
                            branchSelect.append('<option value="' + branch.TenChiNhanh + '">' + branch.TenChiNhanh + '</option>');
                        });
                    } else {
                        alert(response.message);
                    }
                });
            }
        });

        $('#addBankAccountForm').submit(function (event) {
            event.preventDefault(); // Ngừng form submit mặc định

            // Lấy giá trị các trường từ form
            var maNganHangLienKet = $('#bankSelect').val();  // Lấy giá trị mã ngân hàng
            var soTaiKhoan = $('#accountNumber').val();      // Lấy giá trị số tài khoản
            var tenChuSoHuu = $('#ownerName').val();         // Lấy giá trị tên chủ sở hữu
            var tenChiNhanh = $('#branch').val();            // Lấy giá trị chi nhánh

            // Kiểm tra nếu có trường nào không hợp lệ
            if (!maNganHangLienKet || !soTaiKhoan || !tenChuSoHuu || !tenChiNhanh) {
                alert('Vui lòng điền đầy đủ các thông tin.');
                return;  // Nếu thiếu trường nào thì dừng việc gửi dữ liệu
            }

            // Tạo đối tượng dữ liệu
            var formData = {
                maNganHangLienKet: maNganHangLienKet,
                soTaiKhoan: soTaiKhoan,
                tenChuSoHuu: tenChuSoHuu,
                tenChiNhanh: tenChiNhanh
            };

            // Gửi yêu cầu POST với dữ liệu form
            $.post($(this).attr('action'), formData, function (response) {
                if (response.success) {
                    // Nếu thành công, thông báo và đóng modal
                    alert('Tài khoản ngân hàng đã được thêm thành công.');
                    $('#addBankAccountModal').modal('hide');  // Đóng modal
                    window.location.href = '/MyAccount/NganHang';
                } else {
                    // Nếu có lỗi
                    alert(response.message);
                }
            });
        });

        $('#accountNumber').change(function () {
            var soTaiKhoan = $(this).val();
            var maNganHangLienKet = $('#bankSelect').val();
            if (soTaiKhoan && maNganHangLienKet) {
                $.get('/MyAccount/GetTenChuSoHuuBySoTaiKhoan', { soTaiKhoan, maNganHangLienKet }, function (response) {
                    $('#ownerName').val(response.success ? response.tenChuSoHuu : '');
                    if (!response.success) alert(response.message);
                });
            }
        });

        // Address form handling
        $('#province').on('change', function () {
            var maTinhThanh = $(this).val();

            if (maTinhThanh) {
                // Gửi AJAX request
                $.ajax({
                    url: '/MyAccount/GetDistricts', // Đường dẫn đến controller action
                    type: 'GET',
                    data: { maTinhThanh: maTinhThanh }, // Truyền mã tỉnh thành
                    success: function (response) {
                        if (response.success) {
                            // Xóa các tùy chọn cũ của dropdown Quận/Huyện
                            $('#district').empty().append('<option value="" selected disabled>Chọn Quận / Huyện</option>');

                            // Thêm các tùy chọn mới
                            $.each(response.data, function (index, district) {
                                $('#district').append(
                                    $('<option></option>').val(district.MaQuanHuyen).text(district.TenQuanHuyen)
                                );
                            });
                        } else {
                            alert(response.message || 'Không lấy được danh sách Quận/Huyện.');
                        }

                        // Làm trống dropdown Xã/Phường
                        $('#ward').empty().append('<option value="" selected disabled>Chọn Xã / Phường</option>');
                    },
                    error: function () {
                        alert('Đã xảy ra lỗi khi lấy danh sách Quận/Huyện.');
                    },
                });
            } else {
                // Reset dropdown nếu không có tỉnh/thành phố được chọn
                $('#district').empty().append('<option value="" selected disabled>Chọn Quận / Huyện</option>');
                $('#ward').empty().append('<option value="" selected disabled>Chọn Xã / Phường</option>');
            }
        });

        $('#district').on('change', function () {
            var maQuanHuyen = $(this).val();

            if (maQuanHuyen) {
                // Gửi AJAX request để lấy danh sách xã/phường
                $.ajax({
                    url: '/MyAccount/GetWards', // Đường dẫn đến controller action
                    type: 'GET',
                    data: { maQuanHuyen: maQuanHuyen }, // Truyền mã quận/huyện
                    success: function (response) {
                        if (response.success) {
                            // Xóa các tùy chọn cũ của dropdown Xã/Phường
                            $('#ward').empty().append('<option value="" selected disabled>Chọn Xã / Phường</option>');

                            // Thêm các tùy chọn mới
                            $.each(response.data, function (index, ward) {
                                $('#ward').append(
                                    $('<option></option>').val(ward.MaXaPhuong).text(ward.TenXaPhuong)
                                );
                            });
                        } else {
                            alert(response.message || 'Không lấy được danh sách Xã/Phường.');
                        }
                    },
                    error: function () {
                        alert('Đã xảy ra lỗi khi lấy danh sách Xã/Phường.');
                    },
                });
            } else {
                // Reset dropdown nếu không có quận/huyện được chọn
                $('#ward').empty().append('<option value="" selected disabled>Chọn Xã / Phường</option>');
            }
        });

        $('#addAddressForm').on('submit', function (e) {
            e.preventDefault(); // Ngăn không cho form gửi thông thường

            // Lấy dữ liệu từ form
            var formData = {
                maTinhThanh: $('#province').val(),
                maQuanHuyen: $('#district').val(),
                maXaPhuong: $('#ward').val(),
                tenKhachHang: $('#name').val(),
                phone: $('#phoneNumber').val(),
                diaChi: $('#addLocation').val()
            };

            // Kiểm tra các trường bắt buộc
            if (!formData.maTinhThanh || !formData.maQuanHuyen || !formData.maXaPhuong || !formData.tenKhachHang || !formData.phone || !formData.diaChi) {
                alert('Vui lòng điền đầy đủ thông tin!');
                return;
            }

            // Gửi dữ liệu qua AJAX
            $.ajax({
                url: '/MyAccount/ThemDiaChi',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(formData),
                success: function (response) {
                    if (response.success) {
                        alert(response.message); // Thông báo thành công
                        location.reload(); // Reload lại trang hoặc cập nhật danh sách địa chỉ
                    } else {
                        alert(response.message); // Hiển thị thông báo lỗi
                    }
                },
                error: function (xhr, status, error) {
                    console.error(error); // Log lỗi
                    alert('Đã xảy ra lỗi khi thêm địa chỉ.');
                }
            });
        });

        
        $('.btnUpdateAddress').on('click', function () {
            // Lấy giá trị của 'data-addressid' từ nút được click và chuyển thành int
            var maDiaChi = parseInt($(this).data('addressid'), 10);  // Chuyển đổi thành số nguyên (int)

            // Kiểm tra xem giá trị 'maDiaChi' có được lấy đúng không
            console.log("Mã địa chỉ:", maDiaChi);

            // Gọi hàm getAddressDetails với MaDiaChi
            getAddressDetails(maDiaChi);

            // Có thể hiển thị một thông báo hoặc thực hiện hành động khác nếu cần
            console.log("Đã gọi hàm getAddressDetails với mã địa chỉ:", maDiaChi);
        });

        function getAddressDetails(maDiaChi) {
            // Kiểm tra và chuyển maDiaChi thành int nếu cần
            maDiaChi = parseInt(maDiaChi, 10);  // Chuyển đổi thành số nguyên

            // Gửi request GET đến server để lấy thông tin địa chỉ
            $.ajax({
                url: '/MyAccount/LayDuLieuDeCapNhat',
                type: 'GET',
                data: { MaDiaChi: maDiaChi },  // Chuyển MaDiaChi về dạng số nguyên
                success: function (response) {
                    if (response.error) {
                        alert(response.error);
                    } else {
                        // Điền thông tin vào các input trong modal
                        $('#AddressToUpdate').val(response.maDiaChi);
                        $('#name').val(response.TenKhachHang);
                        $('#phoneNumber').val(response.SDT);
                        $('#address').val(response.DiaChiGiaoHang);

                        // Cập nhật các select cho tỉnh, quận, xã
                        updateDropdowns(response.TinhThanh, response.QuanHuyen, response.XaPhuong);

                        // Hiển thị modal
                        $('#updateAddressModal').modal('show');
                    }
                },
                error: function () {
                    alert("Có lỗi khi lấy thông tin địa chỉ!");
                }
            });
        }

        function updateDropdowns(maTinhThanh, maQuanHuyen, maXaPhuong) {
            // Cập nhật Tỉnh
            $.ajax({
                url: '/MyAccount/GetProvinces',
                type: 'GET',
                success: function (response) {
                    var provinceSelect = $('#updateProvince');
                    provinceSelect.empty(); // Xóa các options cũ
                    provinceSelect.append('<option value="" selected disabled>Chọn Tỉnh / Thành phố</option>');

                    $.each(response, function (index, province) {
                        var isSelected = (province.MaTinhThanh === maTinhThanh) ? 'selected' : '';
                        provinceSelect.append('<option value="' + province.MaTinhThanh + '" ' + isSelected + '>' + province.TenTinhThanh + '</option>');
                    });

                    // Cập nhật Quận sau khi tỉnh được chọn
                    updateDistrict(maTinhThanh, maQuanHuyen);
                },
                error: function () {
                    alert('Không thể tải dữ liệu tỉnh thành!');
                }
            });

            function updateDistrict(maTinhThanh, maQuanHuyen) {
                if (maTinhThanh) {
                    $.ajax({
                        url: '/MyAccount/GetDistricts',
                        type: 'GET',
                        data: { maTinhThanh: maTinhThanh },
                        success: function (response) {
                            var districtSelect = $('#updateDistrict');
                            districtSelect.empty(); // Xóa các options cũ
                            districtSelect.append('<option value="" selected disabled>Chọn Quận / Huyện</option>');

                            $.each(response, function (index, district) {
                                var isSelected = (district.MaQuanHuyen === maQuanHuyen) ? 'selected' : '';
                                districtSelect.append('<option value="' + district.MaQuanHuyen + '" ' + isSelected + '>' + district.TenQuanHuyen + '</option>');
                            });

                            // Cập nhật Xã sau khi quận được chọn
                            updateWard(maQuanHuyen, maXaPhuong);
                        },
                        error: function () {
                            alert('Không thể tải dữ liệu quận huyện!');
                        }
                    });
                }
            }

            function updateWard(maQuanHuyen, maXaPhuong) {
                if (maQuanHuyen) {
                    $.ajax({
                        url: '/MyAccount/GetWards',
                        type: 'GET',
                        data: { maQuanHuyen: maQuanHuyen },
                        success: function (response) {
                            var wardSelect = $('#updateWard');
                            wardSelect.empty(); // Xóa các options cũ
                            wardSelect.append('<option value="" selected disabled>Chọn Xã / Phường</option>');

                            $.each(response, function (index, ward) {
                                var isSelected = (ward.MaXaPhuong === maXaPhuong) ? 'selected' : '';
                                wardSelect.append('<option value="' + ward.MaXaPhuong + '" ' + isSelected + '>' + ward.TenXaPhuong + '</option>');
                            });
                        },
                        error: function () {
                            alert('Không thể tải dữ liệu xã phường!');
                        }
                    });
                }
            }
        }

        
         // --------------------------------------------------------------------------END Thiện-----------------------------------------------------------------------------//

    });

})(jQuery);
