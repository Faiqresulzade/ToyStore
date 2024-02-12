var count = 1;

$(function () {
    $(document).on('click', '#loadmore', function () {
        $.ajax({
            method: "GET",
            url: "/Shop/LoadMore",
            data: {
                count:count
            },
            success: function (result) {
                $("#ShopProducts").append(result);
                count++;
            },
            error: function () {
                alert("error");
            }
        })
    })
})



