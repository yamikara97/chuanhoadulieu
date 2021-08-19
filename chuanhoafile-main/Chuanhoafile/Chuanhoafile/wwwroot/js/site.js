// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    $('.table-data').DataTable().destroy();

    $('.table-data').DataTable({
        "dom": '<"top"flp>',
        fixedHeader: {
            header: true
        },
        "language": {
            "lengthMenu": "Hiển  thị _MENU_ dòng trên một trang",
            "zeroRecords": "Không có bản ghi nào",
            "info": "Trang _PAGE_ trên _PAGES_",
            "infoEmpty": "Không có bản ghi nào",
            "infoFiltered": "(Kết quả trả về được duyệt trên tổng _MAX_ bản ghi)",
            "search": "Tìm kiếm:",
            "paginate": {
                "first": "Về đầu",
                "last": "Đến cuối",
                "next": "Tiếp",
                "previous": "Trước"
            },
        },
        "pageLength": 15,
        "bLengthChange": false,
    });
    $(".dataTables_filter input").unbind();
    $(".dataTables_filter input").bind('keyup', function (e) {
        if (e.keyCode == 13) {
            $('.table-data').DataTable().search(this.value).draw();
        }
    });
});