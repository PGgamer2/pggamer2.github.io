function DragVar0(){dragging = "false";}
function DragVar1(){dragging = "true";}

function MusPlay(){
	if (dragging == "true") {
document.getElementById("bg_music").play();
document.getElementById("disk").draggable = false;
document.getElementById("disk").style.cursor = "default";
document.title = "Now playing: C418 - Sweden";}}