function validateCPF(obj, evt) {
    if (navigator.appName.indexOf('Internet Explorer') > (-1)) {
        var key = evt.keyCode;
        if (key < 48 || key > 57)
            return false;
        else {
            var size = obj.value.length;
            if (size == 3 || size == 7)
                obj.value = obj.value + ".";
            else if (size == 11)
                obj.value = obj.value + "-";
            else if (size == 14)
                return false;
            return true;
        }
    } else if (navigator.userAgent.indexOf('Firefox') > (-1)) {
        var key = evt.charCode;
        if ((key < 48 || key > 57) && (key != 0))
            return false;
        else {
            return true;
        }
    }
}

function validateCNPJ(obj, evt) {
    if (navigator.appName.indexOf('Internet Explorer') > (-1)) {
        var key = evt.keyCode;
        if (key < 48 || key > 57)
            return false;
        else {
            var size = obj.value.length;
            if (size == 2 || size == 6)
                obj.value = obj.value + ".";
            else if (size == 10)
                obj.value = obj.value + "/";
            else if (size == 15)
                obj.value = obj.value + "-";
            else if (size == 18)
                return false;
            return true;
        }
    } else if (navigator.userAgent.indexOf('Firefox') > (-1)) {
        var key = evt.charCode;
        if ((key < 48 || key > 57) && (key != 0))
            return false;
        else {
            var size = obj.value.length;
            if (size == 2 || size == 6)
                obj.value = obj.value + ".";
            else if (size == 10)
                obj.value = obj.value + "/";
            else if (size == 15)
                obj.value = obj.value + "-";
            else if (size == 18)
                return false;
            return true;
        }
    }
}