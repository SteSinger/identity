var inputs = document.querySelectorAll(".validation-digit");
var toggles = document.querySelectorAll(".toggle-pw");
function onToggle(sender, event) {
    var children = sender.parentElement.getElementsByTagName("input");
    var toggleOn = false;
    var icon = sender.getElementsByTagName('i').item(0);
    if (icon instanceof HTMLElement) {
        if (icon.classList.contains('fa-eye-slash')) {
            toggleOn = true;
            icon.classList.remove('fa-eye-slash');
            icon.classList.add('fa-eye');
        }
        else {
            icon.classList.remove('fa-eye');
            icon.classList.add('fa-eye-slash');
        }
    }
    if (children.length > 0) {
        var input = children.item(0);
        if (toggleOn) {
            if (input.type == 'password') {
                input.type = 'text';
            }
        }
        else {
            if (input.type == 'text') {
                input.type = 'password';
            }
        }
    }
}
function onPaste(event) {
    event.stopPropagation();
    event.preventDefault();
    var clipboardData = event.clipboardData;
    var pastedData = clipboardData === null || clipboardData === void 0 ? void 0 : clipboardData.getData('Text');
    if (pastedData) {
        for (var i = 0; i < inputs.length; i++) {
            if (pastedData.length > i) {
                inputs[i].value = pastedData[i];
            }
            else {
                inputs[i].focus();
                break;
            }
        }
    }
}
function onKeyUp(event) {
    var _a;
    if (event.isComposing || event.keyCode === 229) {
        return;
    }
    if (event.key === 'v' && event.ctrlKey) {
        return;
    }
    var target = event.srcElement || event.target;
    if (target instanceof HTMLInputElement) {
        var maxLength = target.maxLength;
        var myLength = target.value.length;
        if (myLength >= maxLength) {
            var next = target;
            while (next = next.nextElementSibling) {
                if (next == null) {
                    break;
                }
                if (next instanceof HTMLInputElement) {
                    next.focus();
                    return;
                }
            }
            if ((_a = target === null || target === void 0 ? void 0 : target.form) === null || _a === void 0 ? void 0 : _a.elements['submit']) {
                var submitButton = target.form.elements['submit'];
                if (submitButton instanceof HTMLInputElement) {
                    submitButton.focus();
                }
            }
        }
        else if (myLength === 0 && event.key === 'Backspace') {
            var previous = target;
            while (previous = previous.previousElementSibling) {
                if (previous == null)
                    break;
                if (previous instanceof HTMLInputElement) {
                    previous.focus();
                    previous.select();
                    break;
                }
            }
        }
    }
}
function showBusyIndicator() {
    var indicators = document.querySelectorAll(".busy-indicator");
    indicators.forEach(function (p) { return p.classList.add('active'); });
}
window.addEventListener('beforeunload', function (p) { return showBusyIndicator(); });
inputs.forEach(function (input) {
    input.addEventListener('keyup', onKeyUp);
    input.addEventListener('paste', onPaste);
});
toggles.forEach(function (input) {
    input.addEventListener('click', function (p) { return onToggle(input, p); });
});