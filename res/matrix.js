var canvas = document.querySelector('canvas');
var ctx = canvas.getContext('2d', { willReadFrequently: true });

const letters = '\u{FF78}\u{4E8C}\u{30B3}\u{30BD}\u{30E4}\u{65E5}\u{FF8A}\u{FF90}\u{FF8B}\u{FF70}\u{FF73}\u{FF7C}\u{FF85}\u{FF93}\u{FF86}\u{FF7B}\u{FF9C}\u{FF82}\u{FF75}\u{FF98}\u{FF71}\u{FF8E}\u{FF83}\u{FF8F}\u{FF79}\u{FF92}\u{FF74}\u{FF76}\u{FF77}\u{FF91}\u{FF95}\u{FF97}\u{FF7E}\u{FF88}\u{FF7D}\u{FF80}\u{FF87}\u{FF8D}\u{FF66}\u{FF72}\u{FF78}\u{FF7A}\u{FF7F}\u{FF81}\u{FF84}\u{FF89}\u{FF8C}\u{FF94}\u{FF96}\u{FF99}\u{FF9A}\u{FF9B}\u{FF9D}\u{0190}\u{0030}\u{0031}\u{0032}\u{0034}\u{0035}\u{0037}\u{0038}\u{0039}\u{005A}\u{003A}\u{30FB}\u{002E}\u{0022}\u{003D}\u{002A}\u{002B}\u{002D}\u{003C}\u{003E}\u{00A6}\u{FF5C}\u{005F}\u{00E7}'.split('');
const fontSize = window.innerHeight / 50;
var drops = [];

function draw() {
	// Resize canvas if the window's size changes
	if (canvas.width != window.innerWidth || canvas.height != window.innerHeight) {
		let prevFrame = ctx.getImageData(0, 0, canvas.width, canvas.height);
		canvas.width = window.innerWidth;
		canvas.height = window.innerHeight;
		ctx.putImageData(prevFrame, 0, 0);
		drops.splice(canvas.width / fontSize);
	}
	
	// Draw transparent mask that hides previous letters every frame
	ctx.fillStyle = 'rgba(0, 0, 0, .1)';
	ctx.fillRect(0, 0, canvas.width, canvas.height);
	
	ctx.font = fontSize + "px monospace";
	for (let i = 0; i < canvas.width / fontSize; i++) {
		if (typeof drops[i] === 'undefined'
			|| (drops[i] * fontSize > canvas.height && Math.random() > .95)) {
			drops[i] = 0; // Restart column
		}
		// Draw letter
		let text = letters[Math.floor(Math.random() * letters.length)];
		ctx.fillStyle = '#0f0';
		ctx.fillText(text, i * fontSize, drops[i] * fontSize);
		drops[i]++;
	}
}
setInterval(draw, 33);