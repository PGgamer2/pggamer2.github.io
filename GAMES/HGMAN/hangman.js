window.onload = function () {

  var alphabet = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
        'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
        't', 'u', 'v', 'w', 'x', 'y', 'z'];
  
  var categories;         // Array of topics
  var chosenCategory;     // Selected catagory
  var getHint ;          // Word getHint
  var word ;              // Selected word
  var guess ;             // Geuss
  var geusses = [ ];      // Stored geusses
  var lives ;             // Lives
  var counter ;           // Count correct geusses
  var space;              // Number of spaces in word '-'

  // Get elements
  var showLives = document.getElementById("mylives");
  var showCatagory = document.getElementById("scatagory");
  var getHint = document.getElementById("hint");
  var showClue = document.getElementById("clue");



  // create alphabet ul
  var buttons = function () {
    myButtons = document.getElementById('buttons');
    letters = document.createElement('ul');

    for (var i = 0; i < alphabet.length; i++) {
      letters.id = 'alphabet';
      list = document.createElement('li');
      list.id = 'letter';
      list.innerHTML = alphabet[i];
      check();
      myButtons.appendChild(letters);
      letters.appendChild(list);
    }
  }
    
  
  // Select Catagory
  var selectCat = function () {
    if (chosenCategory === categories[0]) {
      catagoryName.innerHTML = "The chosen category is &#34;football teams&#34;";
    } else if (chosenCategory === categories[1]) {
      catagoryName.innerHTML = "The chosen category is &#34;film&#34;";
    } else if (chosenCategory === categories[2]) {
      catagoryName.innerHTML = "The chosen category is &#34;city&#34;";
    } else if (chosenCategory === categories[3]) {
      catagoryName.innerHTML = "The chosen category is &#34;music band&#34;";
    } else if (chosenCategory === categories[4]) {
      catagoryName.innerHTML = "The chosen category is &#34;food&#34;";
    } else if (chosenCategory === categories[5]) {
      catagoryName.innerHTML = "The chosen category is &#34;school supplies&#34;";
    }
  }

  // Create geusses ul
   result = function () {
    wordHolder = document.getElementById('hold');
    correct = document.createElement('ul');

    for (var i = 0; i < word.length; i++) {
      correct.setAttribute('id', 'my-word');
      guess = document.createElement('li');
      guess.setAttribute('class', 'guess');
      if (word[i] === "-") {
        guess.innerHTML = "-";
        space = 1;
      } else {
        guess.innerHTML = "_";
      }

      geusses.push(guess);
      wordHolder.appendChild(correct);
      correct.appendChild(guess);
    }
  }
  
  // Show lives, Win and Game Over
   comments = function () {
    showLives.innerHTML = "You have " + lives + " lives";
    if (lives < 1) {
      showLives.innerHTML = "Game Over";
      document.getElementById("gmovA").play();
      showClue.innerHTML = "The word is &#34;" + word + "&#34;";
	  setTimeout(function(){ ResetGame(); }, 2000);
    }
    for (var i = 0; i < geusses.length; i++) {
      if (counter + space === geusses.length) {
        showLives.innerHTML = "You Win!";
        document.getElementById("winA").play();
		setTimeout(function(){ ResetGame(); }, 2000);
      }
    }
  }

      // Animate man
  var animate = function () {
    var drawMe = lives ;
    drawArray[drawMe]();
  }

  
   // Hangman
  canvas =  function(){

    myStickman = document.getElementById("stickman");
    context = myStickman.getContext('2d');
    context.beginPath();
    context.strokeStyle = "#000000";
    context.lineWidth = 2;
	frame1();
	frame2();
	frame3();
	frame4();
  };
  
    head = function(){
      myStickman = document.getElementById("stickman");
      context = myStickman.getContext('2d');
      context.beginPath();
      context.arc(160, 25, 10, 0, Math.PI*2, true);
      context.stroke();
    }
    
  draw = function($pathFromx, $pathFromy, $pathTox, $pathToy) {
    
    context.moveTo($pathFromx, $pathFromy);
    context.lineTo($pathTox, $pathToy);
    context.stroke(); 
}
   frame1 = function() {
     draw (100, 150, 250, 150);
   };
   
   frame2 = function() {
     draw (110, 0, 110, 600);
   };
  
   frame3 = function() {
     draw (100, 5, 170, 5);
   };
  
   frame4 = function() {
     draw (160, 5, 160, 15);
   };
  
   torso = function() {
     draw (160, 36, 160, 70);
   };
  
   rightArm = function() {
     draw (160, 46, 200, 50);
   };
  
   leftArm = function() {
     draw (160, 46, 120, 50);
   };
  
   rightLeg = function() {
     draw (160, 70, 200, 100);
   };
  
   leftLeg = function() {
     draw (160, 70, 120, 100);
   };
  
  drawArray = [rightLeg, leftLeg, rightArm, leftArm,  torso,  head, frame4, frame3, frame2, frame1]; 


  // OnClick Function
   check = function () {
    list.onclick = function () {
      var geuss = (this.innerHTML);
      this.setAttribute("class", "active");
      this.onclick = null;
      for (var i = 0; i < word.length; i++) {
        if (word[i] === geuss) {
          geusses[i].innerHTML = geuss;
          counter += 1;
        } 
      }
      var j = (word.indexOf(geuss));
      if (j === -1) {
        lives -= 1;
        comments();
        animate();
        document.getElementById("clA").play();
      } else {
        document.getElementById("clA").play();
        comments();
      }
    }
  }
  
    
  // Play
  play = function () {
    categories = [
        ["everton", "liverpool", "swansea", "chelsea", "hull", "manchester-city", "newcastle-united"],
        ["alien", "titanic", "gladiator", "finding-nemo", "jaws"],
        ["manchester", "milan", "madrid", "amsterdam", "prague"],
        ["beatles", "rolling-stones", "queen", "led-zeppelin"],
        ["pizza", "hamburger", "fish-and-chips", "apple", "sushi", "cake"],
        ["rubber", "school-bag", "pencil", "scissors", "calculator", "sharpener"]
    ];

    chosenCategory = categories[Math.floor(Math.random() * categories.length)];
    word = chosenCategory[Math.floor(Math.random() * chosenCategory.length)];
    word = word.replace(/\s/g, "-");
    console.log(word);
    buttons();

    geusses = [ ];
    lives = 6;
    counter = 0;
    space = 0;
    result();
    comments();
    selectCat();
    canvas();
  }

  play();
  
  // Hint

    hint.onclick = function() {

      hints = [
        ["They are the rivals of Liverpool FC", "Based in Mersyside", "First Welsh team to reach the Premier Leauge", "Owned by A russian Billionaire", "Once managed by Phil Brown", "2013 FA Cup runners up", "Gazza's first club"],
        ["Science-Fiction horror film", "The protagonist's name is Jack", "Historical drama", "A lost fish", "Giant great white shark"],
        ["Northern city in the UK", "Home of AC Milan and International FC", "Spanish capital", "Netherlands capital", "Czech Republic capital"],
        ["John Lennon is part of it", "They made &#34;Satisfaction&#34;", "There's a film dedicated to them", "They made &#34;Whole Lotta Love&#34;"],
        ["An italian food", "The classic McDonald's food", "An english traditional food", "The mascotte of a popular informatic company", "Raw fish", "Traditional birthday's food"],
        ["The thing you use when you write something wrong", "The thing where you put the school supplies", "The thing you use to write", "You use it to cut paper", "You use it when there are too many numbers", "You use it when the pencil's top breaks"]
    ];

    var catagoryIndex = categories.indexOf(chosenCategory);
    var hintIndex = chosenCategory.indexOf(word);
    showClue.innerHTML = "Clue: - " +  hints [catagoryIndex][hintIndex];
    document.getElementById("clA").play();
  };

   // Reset

  document.getElementById('reset').onclick = function() {
	  document.getElementById("clA").play();
	  ResetGame();
  }
  function ResetGame(){
	correct.parentNode.removeChild(correct);
    letters.parentNode.removeChild(letters);
    showClue.innerHTML = "";
    context.clearRect(0, 0, 400, 400);
    play();
  }
}


