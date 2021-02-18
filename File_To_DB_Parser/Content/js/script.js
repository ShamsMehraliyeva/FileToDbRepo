function showLoading(title) {
    return swal.fire({
        title: title,
        allowEscapeKey: false,
        allowOutsideClick: false,
        onOpen: () => {
            swal.showLoading();
        }
    });
}

function stopLoading() {
    swal.close();
}

function isSomething(value) {
    return value != undefined && value != null && value != "";
}
function showMessage(type, title, message) {

    var msg = "";
    if (Array.isArray(message)) {
        var counter = 1;
        var msgs = [];

        if (message.length > 1) {
            message.forEach(function (m) {
                msgs.push("<p>" + counter + ". " + m + "</p>");
                counter++;
            });

            msg = msgs.join("\n");
        }
        else if (message.length == 1) {
            msg = "<p>" + message[0] + "</p>";
        }
    }
    else if (isSomething(message)) {
        msg = message;
    }

    return Swal.fire({
        html: msg,
        icon: type,
        title: title,
        showConfirmButton: true
    });
}

function showSuccessMessage(title, message) {
    return showMessage("success", title, message);
}

function showErrorMessage(title, message) {
    return showMessage("error", title, message);
}

function showWarningMessage(title, message) {
    return showMessage("warning", title, message);
}