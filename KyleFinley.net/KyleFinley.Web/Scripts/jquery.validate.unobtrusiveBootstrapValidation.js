(function ($) {
    $.fn.unobtrusiveBootstrapValidation = function (options) {
        var settings = {
            errorClass: "has-error",
            validClass: "has-success",
            simpleStyles: options != undefined && options.simpleStyles != undefined ? options.simpleStyles : true,
            highlight: options != undefined && options.highlight ? options.highlight : null,
            unhighlight: options != undefined && options.unhighlight ? optioins.unhighlight : null,
            onError: function (error, inputElement) {
                $(inputElement).closest(".form-group").find(".help-block").removeClass("field-validation-error").show().find(".has-error").removeClass("has-error");
                if (options != undefined && options.onError) {
                    options.onError(error, inputElement);
                }
            },
            onSuccess: options != undefined && options.onSuccess ? options.onSuccess : function (error) { },
            callBaseError: options != undefined && options.callBaseError != undefined ? options.callBaseError : true,
            callBaseSuccess: options != undefined && options.callBaseSuccess != undefined ? options.callBaseSuccess : true
        };

        this.each(function () {
            var form = $(this);

            var validator = form.data("validator");
            var unobtrusiveValidator = form.data("unobtrusiveValidation");

            if (validator != undefined && unobtrusiveValidator != undefined) {
                validator.settings.validClass = settings.validClass;
                validator.settings.errorClass = settings.errorClass;
                unobtrusiveValidator.options.validClass = settings.validClass;
                unobtrusiveValidator.options.errorClass = settings.errorClass;

                var baseErrorPlacement = validator.settings.errorPlacement;
                validator.settings.errorPlacement = function (error, inputElement) {
                    if (settings.callBaseError) {
                        baseErrorPlacement(error, inputElement);
                    }
                    settings.onError(error, inputElement);
                };
                var baseSuccess = validator.settings.success;
                validator.settings.success = function (error) {
                    if (settings.callBaseSuccess) {
                        baseSuccess(error);
                    }
                    settings.onSuccess(error);
                };
                validator.settings.highlight = settings.highlight != null ? options.highlight : function (element, errorClass, validClass) {
                    if ($(".help-block[data-valmsg-for='" + element.name + "']").length == 0) {
                        var error = $("<" + unobtrusiveValidator.options.errorElement + "/>").addClass("help-block").attr("data-valmsg-for", element.name).attr("data-valmsg-replace", "true");
                        $(element).closest(".form-group").append(error);
                    }
                    $(element).closest(".form-group").removeClass(validClass).addClass(errorClass).find(".help-block").show();
                };
                validator.settings.unhighlight = settings.unhighlight != null ? options.unhighlight : function (element, errorClass, validClass) {
                    $(element).closest(".form-group").removeClass(errorClass).addClass(validClass).find(".help-block").removeClass("field-validation-valid").hide();
                };
            }
        });
        $(this).on("reset", function (e) {
            $(e.target).find("[class*='has-success']").removeClass(settings.validClass);
            $(e.target).find("[class*='has-error']").removeClass(settings.errorClass);
            $(e.target).find("[class*='help-block']").remove();
        });
        $(".field-validation-valid").addClass("help-block").removeClass("field-validation-valid").hide();
        if (settings.simpleStyles && $("style#simpleValidationStyles").length == 0) {
            $("<style id='simpleValidationStyles'>.form-control:focus, .single-line:focus { border-color: #66afe9; } .has-success .form-control { border-color: #ccc !important; } .has-success .form-control:focus { border-color: #66afe9 !important; -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6) !important; box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6) !important; } .has-warning .form-control { border-color: #ccc !important; } .has-warning .form-control:focus { border-color: #66afe9 !important; -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6) !important; box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6) !important; } .has-success .control-label { color: #ccc !important; } .has-success .control-label:focus { border-color: #66afe9 !important; -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6) !important; box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6) !important; } .has-warning .control-label { color: #ccc !important; } .has-warning .control-label:focus { border-color: #66afe9 !important; -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6) !important; box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6) !important;} .has-success .input-group-addon { color: #555 !important; background-color: #eee !important; border-color: #ccc !important;} .has-warning .input-group-addon { color: #555 !important; background-color: #eee !important; border-color: #ccc !important;} </style>").appendTo("head")
        }
    };
})(jQuery);