mergeInto(LibraryManager.library, {
	OnSubmitScore: function (score) {
		submitScore(score);
	},

	OnExit: function () {
		exit();
	},

	OnPlay: function () {
		play();
	},
});