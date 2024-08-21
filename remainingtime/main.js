if (!Math.trunc) {
	// Polyfill for IE
    Math.trunc = function (v) {
        return v < 0 ? Math.ceil(v) : Math.floor(v);
    };
}

var NowDate = getDateWithTimezone();
var UntilDate = getDateWithTimezone();

if (getParameterByName("y") || getParameterByName("m") || getParameterByName("d") ||
	getParameterByName("h") || getParameterByName("min") || getParameterByName("s")) {
	UntilDate.setFullYear(getParameterByName("y") ? getParameterByName("y") : UntilDate.getFullYear());
	UntilDate.setMonth(getParameterByName("m") ? getParameterByName("m") - 1 : UntilDate.getMonth());
	UntilDate.setDate(getParameterByName("d") ? getParameterByName("d") : UntilDate.getDate());
	UntilDate.setHours(getParameterByName("h") ? getParameterByName("h") : 0);
	UntilDate.setMinutes(getParameterByName("min") ? getParameterByName("min") - UntilDate.getTimezoneOffset() : -UntilDate.getTimezoneOffset());
	UntilDate.setSeconds(getParameterByName("s") ? getParameterByName("s") : 0);
	UntilDate.setMilliseconds(0);
}

var confettishowedup = UntilDate - NowDate <= 0 ? true : false;

document.getElementById("timelabelUntilFull").innerHTML = 'Remaining time until ' + UntilDate.toISOString().replace('T', ',<small> ').slice(0, -5) + '</small>';
window.document.title = "Remaining time until " + UntilDate.toISOString().split("T")[0];

function ChangeCountdown() {
	NowDate = getDateWithTimezone();
	document.getElementById("daylabelToday").innerHTML = "Today is " + NowDate.toISOString().split("T")[0];
	document.getElementById("timelabelToday").innerHTML = "and it's " + NowDate.toISOString().split("T")[1].slice(0, -5);
	var diffMilli = Math.abs(UntilDate - NowDate);
	var diffSeconds = TruncateDecimal(diffMilli / 1000);
	var diffMinutes = TruncateDecimal(diffMilli / (1000 * 60));
	var diffHours = TruncateDecimal(diffMilli / (1000 * 60 * 60));
	var diffDays = TruncateDecimal(diffMilli / (1000 * 60 * 60 * 24));
	document.getElementById("DaysLabel").innerHTML = diffDays + " days";
	document.getElementById("HoursLabel").innerHTML = diffHours + " hours";
	document.getElementById("MinutesLabel").innerHTML = diffMinutes + " minutes";
	document.getElementById("SecondsLabel").innerHTML = diffSeconds + " seconds";
	document.getElementById("MilliLabel").innerHTML = diffMilli + " milliseconds";
	if (UntilDate - NowDate <= 0 && !confettishowedup) {
		confettishowedup = true;
		var confettiSettings = {"target":"confetti-holder","max":"500","size":"1","animate":true,"props":["square","triangle","line"],"colors":[[165,104,246],[230,61,135],[0,199,228],[253,214,126]],"clock":"35","rotate":true,"start_from_edge":true,"respawn":false};
		var confetti = new ConfettiGenerator(confettiSettings);
		confetti.render();
		try {
			document.getElementById("partyhorn").play();
		} catch(e) {}
	}
}

var loop = setInterval(ChangeCountdown, 10);

function getDateWithTimezone() {
	var tempDate = new Date();
	tempDate.setMinutes(tempDate.getMinutes() - tempDate.getTimezoneOffset());
	return tempDate;
}

function TruncateDecimal(value, precision) {
	if (!precision) precision = 1;
    var step = Math.pow(10, precision);
    var tmp = Math.trunc(step * value);
    return (tmp / step).toLocaleString("en", {useGrouping: false, minimumFractionDigits: precision});
}

function getParameterByName(name, url) {
	url = url || window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}