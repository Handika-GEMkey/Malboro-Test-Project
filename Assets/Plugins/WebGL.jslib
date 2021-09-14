mergeInto(LibraryManager.library, {
	OnSubmitScore: function (score) {
		submitScore(score);
	},

	OnPlay: function () {
		play();
	},

	OnGameQuit1: function () {
		exit();
	},

	OnGameQuit2: function () {
		gameQuit1();
	},

	OnGameQuit3: function () {
		gameQuit2();
	},
	
	OnGameStart: function () {
		gameStart();
	},

	OnPlayAgain: function () {
		playAgain();
	},

	OnShare: function () {
		share();
	},
});