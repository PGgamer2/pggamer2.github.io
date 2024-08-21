var canvas = document.getElementById('drawableCanvas');
var ctx = canvas.getContext('2d', { willReadFrequently: true });
ctx.lineWidth = 3;
ctx.lineJoin = 'round';
ctx.lineCap = 'round';
ctx.strokeStyle = 'black';

// Undo and redo using CTRL+Z and CTRL+Y
var actionImagesStack = [];
var nextActionIndex = 0;
document.addEventListener('keydown', function(e) {
	if (e.keyCode == 90 && e.ctrlKey) {
		if (nextActionIndex <= 1) {
			if (nextActionIndex == 1) nextActionIndex--;
			ctx.clearRect(0, 0, canvas.width, canvas.height);
			return;
		}
		ctx.putImageData(actionImagesStack[--nextActionIndex - 1], 0, 0);
		return;
	}
	if (e.keyCode == 89 && e.ctrlKey) {
		if (nextActionIndex >= actionImagesStack.length) return;
		ctx.putImageData(actionImagesStack[++nextActionIndex - 1], 0, 0);
		return;
	}
});

// Resize canvas to window size
document.addEventListener('pointermove', function(e) {
	if (canvas.width == window.innerWidth && canvas.height == window.innerHeight)
		return;
	let prevSettings = {width: ctx.lineWidth, join: ctx.lineJoin, cap: ctx.lineCap};
	let prevFrame = ctx.getImageData(0, 0, canvas.width, canvas.height);
	canvas.width = window.innerWidth;
	canvas.height = window.innerHeight;
	ctx.putImageData(prevFrame, 0, 0);
	ctx.lineWidth = prevSettings.width;
	ctx.lineJoin = prevSettings.join;
	ctx.lineCap = prevSettings.cap;
});

// Draw functions
var ptr = {x: 0, y: 0};
canvas.addEventListener('pointermove', function(e) {
	ptr.x = e.pageX - this.offsetLeft;
	ptr.y = e.pageY - this.offsetTop;
});

var onPaint = function() {
    ctx.lineTo(ptr.x, ptr.y);
    ctx.stroke();
}

var listeningForPainting = false;
canvas.addEventListener('pointerdown', function(e) {
    ctx.beginPath();
    ctx.moveTo(ptr.x, ptr.y);
    canvas.addEventListener('pointermove', onPaint, false);
	listeningForPainting = true;
}, false);

var stopPainting = function() {
	canvas.removeEventListener('pointermove', onPaint, false);
	if (listeningForPainting) {
		// Save frame for undo/redo
		actionImagesStack.splice(nextActionIndex++);
		actionImagesStack.push(ctx.getImageData(0, 0, canvas.width, canvas.height));
		listeningForPainting = false;
	}
}
canvas.addEventListener('pointerup', stopPainting, false);
canvas.addEventListener('pointerleave', stopPainting);

// Buttons
function ChangeColor() {
	let chcol = prompt('Type here the color');
	let s = new Option().style;
	s.color = chcol;
	if (s.color != chcol.toLowerCase()) {
		alert('Invalid color!');
		return;
	}
	ctx.strokeStyle = chcol;
	document.getElementById('displayColor').innerHTML = chcol;
}

function ChangeWidth() {
	let chw = parseInt(prompt('Type here the brush width'), 10);
	if (isNaN(chw) || chw <= 0) {
		alert('Invalid number!');
		return;
	}
	ctx.lineWidth = chw;
	document.getElementById('displayWidth').innerHTML = chw;
}

function ChangeType() {
	if (ctx.lineCap == "square") {
		ctx.lineJoin = 'round';
		ctx.lineCap = 'round';
		document.getElementById("typeBtn").innerHTML = "Round";
	} else {
		ctx.lineJoin = 'bevel';
		ctx.lineCap = 'square';
		document.getElementById("typeBtn").innerHTML = "Square";
	}
}

function ResetPage() {
	let surerel = confirm("Are you sure?");
	if (surerel) location.reload();
}

// Download button
var dlElement = document.createElement("a");
document.body.appendChild(dlElement);
dlElement.style = "display: none";
function DLCanv() {
	canvas.toBlob(function(blob) {
		let url = window.URL.createObjectURL(blob);
        dlElement.href = url;
        dlElement.download = "My Painting " + (new Date()).toDateString() + ".png";
        dlElement.click();
        window.URL.revokeObjectURL(url);
	});
}