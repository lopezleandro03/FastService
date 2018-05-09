function ajaxGet(url, success, error) {
    $.ajax({
        type: 'GET',
        url: url,
        contentType: "html",
        cache: false,
        dataType: "html",
        beforeSend: function () {
            $(".spinner").show();
        },
        success: function (data) {
            success(data);
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
        cache: false,
        dataType: "html",
        beforeSend: function () {
            $(".spinner").show();
        },
        success: function (data) {
            success(data);
        },
        error: function () {
            $("#modalError").modal();
        },
        complete: function () {
            $(".spinner").hide();
        }
    });
}

function ajaxGetNoCustomErrorSyncNoLoading(url, success) {
    $.ajax({
        type: 'GET',
        url: url,
        async: false,
        contentType: "html",
        cache: true,
        dataType: "html",
        success: function (data) {
            success(data);
        },
        error: function () {
            $("#modalError").modal();
        }
    });
}

function ajaxPost(url, success, error) {
    $.ajax({
        type: 'POST',
        url: url,
        contentType: "html",
        cache: false,
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
        cache: false,
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