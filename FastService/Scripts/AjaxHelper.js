function ajaxGet(url, success, error) {
    $.ajax({
        type: 'GET',
        url: url,
        contentType: "html",
        dataType: "html",
        beforeSend: function () {
            $(".spinner").show();
        },
        success: function (data) {
            success(data);
            $("#modalSuccess").modal();
        },
        error: function () {
            error();
            $("#modalError").modal();
        },
        complete: function () {
            $(".spinner").hide();
        }
    });
}


function ajaxGetNoCustomError(url, success) {
    $.ajax({
        type: 'GET',
        url: url,
        contentType: "html",
        dataType: "html",
        beforeSend: function () {
            $(".spinner").show();
        },
        success: function (data) {
            success(data);
            $("#modalSuccess").modal();
        },
        error: function () {
            $("#modalError").modal();
        },
        complete: function () {
            $(".spinner").hide();
        }
    });
}

function ajaxPost(url, success, error) {
    $.ajax({
        type: 'POST',
        url: url,
        contentType: "html",
        dataType: "html",
        beforeSend: function () {
            $(".spinner").show();
        },
        success: function (data) {
            success(data);
            $("#modalSuccess").modal();
        },
        error: function () {
            error();
            $("#modalError").modal();
        },
        complete: function () {
            $(".spinner").hide();
        }
    });
}


function ajaxPostNoCustomError(url, success) {
    $.ajax({
        type: 'POST',
        url: url,
        contentType: "html",
        dataType: "html",
        beforeSend: function () {
            $(".spinner").show();
        },
        success: function (data) {
            success(data);
            $("#modalSuccess").modal();
        },
        error: function () {
            $("#modalError").modal();
        },
        complete: function () {
            $(".spinner").hide();
        }
    });
}