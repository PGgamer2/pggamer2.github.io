window.onload = function () {
    var hash = window.location.hash || "";
	
	document.getElementById("mobT").style.display="none";
	document.getElementById("404").style.display="none";
    
	if (hash === ""){
		document.getElementById("404").style.display="block";
    }
    if (hash === "#mobile"){
		document.getElementById("mobT").style.display="block";
    }

}