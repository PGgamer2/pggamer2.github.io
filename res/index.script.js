var mouseY = -1;
document.addEventListener("mousemove", function (e) {
	if ("ontouchstart" in document.documentElement) return;
	mouseY = e.pageY - window.scrollY;
});
document.addEventListener("mouseleave", function (e) {
	mouseY = -1;
});

function onUpdate() {
	// Auto scroll
	if (mouseY > 0) {
		if (mouseY > window.innerHeight * 0.8)
			window.scrollBy(0, 5);
		if (mouseY < window.innerHeight * 0.2)
			window.scrollBy(0, -5);
	}
	
	// Change background
	let hoverOpt = document.elementFromPoint(1, mouseY < 0 ? window.innerHeight / 2 : mouseY);
	if (typeof hoverOpt !== "undefined" && typeof hoverOpt.getAttribute("display") === "string") {
		let bgImgElem = document.getElementById("bgImg");
		let bgFrameElem = document.getElementById("bgFrame");
		bgImgElem.style.display = "none";
		bgFrameElem.style.display = "none";
		switch(hoverOpt.getAttribute("bg-type")) {
		case "iframe":
			if (!bgFrameElem.src.endsWith(hoverOpt.getAttribute("display"))) {
				bgFrameElem.src = hoverOpt.getAttribute("display");
			}
			bgFrameElem.style.display = "";
			break;
		default:
		case "image":
			bgImgElem.src = hoverOpt.getAttribute("display");
			bgImgElem.style.display = "";
			break;
		}
	}
	
	// Change opacity
	let elem = document.getElementsByClassName("sideOpt");
	if (elem[0].getBoundingClientRect().top < 0
		|| elem[elem.length - 1].getBoundingClientRect().top + elem[elem.length - 1].getBoundingClientRect().height > window.innerHeight) {
		for (let i = 0; i < elem.length; i++) {
			let boundingRect = elem[i].getBoundingClientRect();
			let yPos = boundingRect.top + boundingRect.height / 2;
			let opacity = 1;
			if (yPos < window.innerHeight * 0.2)
				opacity = yPos / (window.innerHeight * 0.2);
			if (yPos > window.innerHeight * 0.8)
				opacity = (window.innerHeight - yPos) / (window.innerHeight * 0.2);
			elem[i].style.opacity = opacity > 0 ? opacity : 0;
		}
	}
	
	window.requestAnimationFrame(onUpdate);
}
window.requestAnimationFrame(onUpdate);