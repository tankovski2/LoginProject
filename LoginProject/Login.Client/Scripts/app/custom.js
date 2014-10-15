var LoginClient = LoginClient || {};

LoginClient.Persister = (function () {

    function sendRequst(url) {
        var errorsDiv = $("#errors");
        var userNameInput = $("#UserName");
        var passwordInput = $("#Password");

        errorsDiv.html("");
        userNameInput.removeClass("input-validation-error");
        passwordInput.removeClass("input-validation-error");

        var user =
            {
                username: userNameInput.val(),
                password: passwordInput.val()
            };
        
        httpRequester.postJSON(url, user).then(function (data) {
            if (data.valid) {
                location = data.url;
            }
            else {
                var errors = JSON.parse(data.validationErrors);
                       
                if (errors.Password.length > 0) {
                    passwordInput.addClass("input-validation-error");
                    for (var i = 0; i < errors.Password.length; i++) {
                        errorsDiv.append($("<div></div>").html(errors.Password[i]))
                    }
                }
                if (errors.UserName.length > 0) {
                    userNameInput.addClass("input-validation-error");
                    for (var i = 0; i < errors.UserName.length; i++) {
                        errorsDiv.append($("<div></div>").html(errors.UserName[i]))
                    }
                }
            }
        });
    }                                 

    return {
        request:sendRequst
    }
}());