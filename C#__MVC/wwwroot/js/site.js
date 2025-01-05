// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).on('click', '.avatar_img', function () {
	var profileMenu = $('.profile');
	if (profileMenu.css('display') === 'none') {
		profileMenu.css('display', 'block'); // Hiển thị menu
	} else {
		profileMenu.css('display', 'none'); // Ẩn menu
	}
});

// Ẩn menu khi nhấp ra ngoài menu
$(document).click(function (e) {
	if (!$(e.target).closest('.wrap_login').length) {
		$('.profile').hide(); // Ẩn danh sách khi nhấp ra ngoài avatar
	}
});

