function GenUUID(idfieldoutput) {
	var seed = Date.now();
	if (window.performance && typeof window.performance.now === "function") {
		seed += performance.now();
	}

	var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
		var r = (seed + Math.random() * 16) % 16 | 0;
		seed = Math.floor(seed/16);

		return (c === 'x' ? r : r & (0x3|0x8)).toString(16);
	});

	document.getElementById(idfieldoutput).value = uuid;
}

function download(filename, text) {var element = document.createElement('a');element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));element.setAttribute('download', filename);element.style.display = 'none';document.body.appendChild(element);element.click();document.body.removeChild(element);}

function GenManifest() {
	if ( document.getElementById("formver").value == ""
	  || document.getElementById("packdesc").value == ""
	  || document.getElementById("packname").value == ""
	  || document.getElementById("packid").value == ""
	  || document.getElementById("packver").value == ""
	  || document.getElementById("packminengver").value == ""
	  || document.getElementById("moddesc").value == ""
	  || document.getElementById("packtype").value == ""
	  || document.getElementById("modid").value == ""
	  || document.getElementById("modver").value == "" ) {
		console.log("Error: some value is missing.");
		document.getElementById("logoutput").style.color = "red";
		document.getElementById("logoutput").innerHTML = "Error: some value is missing.";
	
	} else {
		let manjson = `{
	"format_version": ` + document.getElementById("formver").value + `,
	"header": {
		"description": "` + document.getElementById("packdesc").value + `",
		"name": "` + document.getElementById("packname").value + `",
		"uuid": "` + document.getElementById("packid").value + `",
		"version": [` + document.getElementById("packver").value + `],
		"min_engine_version": [` + document.getElementById("packminengver").value + `]
	},
	"modules": [
		{
		"description": "` + document.getElementById("moddesc").value + `",
		"type": "` + document.getElementById("packtype").value + `",
		"uuid": "` + document.getElementById("modid").value + `",
		"version": [` + document.getElementById("modver").value + `]
		}
	]
}`;
	
		console.log("JSON Successfully Generated");
		document.getElementById("logoutput").style.color = "green";
		document.getElementById("logoutput").innerHTML = "JSON Successfully Generated";
		download("manifest.json", manjson);
}}