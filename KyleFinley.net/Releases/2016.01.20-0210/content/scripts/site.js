function ajaxError(xhr, status, error) {
    alert("Error")
};
function error(message, globalAlertElement) {
    if (globalAlertElement != null) {
        globalAlertElement.text(message).show();
        globalAlertElement.css("display", "block");
    } else {
        alert(message);
    }
};
function ajaxError(xhr, status, error, globalAlertElement) {
    var generalMessage = "We have experienced an error. ";
    var errorDetails = "(ReadyState: " + ((xhr.readyState) ? xhr.readyState : xhr) + ", Status: " + status + ((error != null) ? ", Error: " + error : "") + ")";
    Error(generalMessage + errorDetails, globalAlertElement);
};
function log(message) {
    try {
        console.log(timeStamp() + " : " + message);
    } catch (ex) { }
};
function timeStamp() {
    var date = new Date();
    var hours = date.getHours();
    var minutes = date.getMinutes();
    if (minutes <= 9) {
        minutes = "0" + minutes;
    }
    var seconds = date.getSeconds();
    if (seconds <= 9) {
        seconds = "0" + seconds;
    }
    return "{0}:{1}:{2}".format(hours, minutes, seconds);
};
String.prototype.format = function () {
    var pattern = /\{\d+\}/g;
    var args = arguments;
    return this.replace(pattern, function (capture) {
        return args[capture.match(/\d+/)];
    });
};
String.prototype.hasValue = function () {
    return !isEmpty(this);
};
function hasValue(s) {
    return !isEmpty(s);
};
function isInteger(s) {
    var i;
    if (isEmpty(s)) {
        return false;
    }

    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (!isDigit(c)) {
            return false;
        }
    }
    return true;
};
function isEmpty(s) {
    if (s) {
        //if (s != null || s != NaN || typeof(s) != "undefined") {
        return s.length > 0 ? false : true;
    }
    return true;
};
function isDigit(c) {
    return ((c >= "0") && (c <= "9"));
};
function isNumeric(val) {
    if (isNaN(parseFloat(val))) {
        return false;
    }
    return true;
};
function disableElement(element) {
    element.addClass("disabled");
    element.data("disabledEvents", element.data("events"));
    element.data("events", null);
};
function enableElement(element) {
    if (element.hasClass("disabled")) {
        element.removeClass("disabled");
        element.data("events", element.data("disabledEvents"));
        element.data("disabledEvents", null);
    }
};
function disableElements(elementArray) {
    elementArray.forEach(disableElement);
};
function enableElements(elementArray) {
    elementArray.forEach(enableElement);
};
function resetGlobalControls() {
    $("#globalAlert").hide();
};

/** UTM Paramater Code **/
function removeParam(key, sourceURL) {
    var rtn = sourceURL.split("?")[0],
        param,
        params_arr = [],
        queryString = (sourceURL.indexOf("?") !== -1) ? sourceURL.split("?")[1] : "";
    if (queryString !== "") {
        params_arr = queryString.split("&");
        for (var i = params_arr.length - 1; i >= 0; i -= 1) {
            param = params_arr[i].split("=")[0];
            if (param === key) {
                params_arr.splice(i, 1);
            }
        }
        rtn = rtn + "?" + params_arr.join("&");
    }
    return rtn;
};
var removeUtms = function () {
    var l = window.location.href;

    if (utmParams.length > 0) {
        for (var i = 0; i < utmParams.length; i++) {
            l = removeParam(utmParams.propertyAt(i), l);
        }
        if (l.indexOf("?") == l.length - 1) {
            l = l.replace("?", "");
        }
        history.replaceState({}, '', l);
    }
};
var utmParams = function () {
    var utms = {};
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    length = 0;
    items = {};
    for (var i = 0; i < vars.length; i++) {
        if (vars[i].match(/(utm)+/)) {
            var pair = vars[i].split("=");
            if (typeof utms[pair[0]] === "undefined") {
                items[this.length] = pair[0];
                length++;
                utms[pair[0]] = pair[1];
            } else if (typeof utms[pair[0]] === "string") {
                var arr = [query_string[pair[0]], pair[1]];
                items[this.length] = pair[0];
                length++;
                utms[pair[0]] = arr;
            } else {
                items[this.length] = pair[0];
                length++;
                utms[pair[0]].push(pair[1]);
            }
        }
    }
    utms.length = Object.keys(utms).length;
    utms.valueOfIndex = function (index) {
        return utms[items[index]];
    }
    utms.propertyAt = function (index) {
        return items[index];
    }
    return utms;
}();

/** End UTM Code **/

function getKey(list, value) {
    for (var key in list) {
        if (list[key] == value) {
            return key;
        }
    }
    return null;
};

String.prototype.toTitleCase = function () {
    var i, j, str, lowers, uppers;
    str = this.replace(/([^\W_]+[^\s-]*) */g, function (txt) {
        return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
    });

    // Certain minor words should be left lowercase unless 
    // they are the first or last words in the string
    lowers = ['A', 'An', 'The', 'And', 'But', 'Or', 'For', 'Nor', 'As', 'At',
    'By', 'For', 'From', 'In', 'Into', 'Near', 'Of', 'On', 'Onto', 'To', 'With'];
    for (i = 0, j = lowers.length; i < j; i++)
        str = str.replace(new RegExp('\\s' + lowers[i] + '\\s', 'g'),
          function (txt) {
              return txt.toLowerCase();
          });

    // Certain words such as initialisms or acronyms should be left uppercase
    uppers = ['Id', 'Tv'];
    for (i = 0, j = uppers.length; i < j; i++)
        str = str.replace(new RegExp('\\b' + uppers[i] + '\\b', 'g'),
          uppers[i].toUpperCase());

    return str;
};

Date.prototype.setRFC3339 = function (dString) {
    var utcOffset, offsetSplitChar;
    var offsetMultiplier = 1;
    var dateTime = dString.split("T");
    var date = dateTime[0].split("-");
    var time = dateTime[1].split(":");
    var offsetField = time[time.length - 1];
    var offsetString;
    offsetFieldIdentifier = offsetField.charAt(offsetField.length - 1);
    if (offsetFieldIdentifier == "Z") {
        utcOffset = 0;
        time[time.length - 1] = offsetField.substr(0, offsetField.length - 2);
    } else {
        if (offsetField[offsetField.length - 1].indexOf("+") != -1) {
            offsetSplitChar = "+";
            offsetMultiplier = 1;
        } else {
            offsetSplitChar = "-";
            offsetMultiplier = -1;
        }
        offsetString = offsetField.split(offsetSplitChar);
        time[time.length - 1] == offsetString[0];
        offsetString = offsetString[1].split(":");
        utcOffset = (offsetString[0] * 60) + offsetString[1];
        utcOffset = utcOffset * 60 * 1000;
    }

    this.setTime(Date.UTC(date[0], date[1] - 1, date[2], time[0], time[1], time[2]) + (utcOffset * offsetMultiplier));
    return this;
};