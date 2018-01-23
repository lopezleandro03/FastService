function ajaxGet(url, success, error) {
    $.ajax({
        type: 'GET',
        url: url,
        contentType: "html",
        dataType: "html",
        beforeSend: function () {
            $(".spinner").show();
        },
        success: function (data) { success(data) },
        error: function () { error() },
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
        success: function (data) { success(data) },
        error: function () { alert("Ocurrio un error, intente de nuevo") },
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
        success: function (data) { success(data) },
        error: function () { error() },
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
        success: function (data) { success(data) },
        error: function () { alert("Ocurrio un error, intente de nuevo") },
        complete: function () {
            $(".spinner").hide();
        }
    });
}