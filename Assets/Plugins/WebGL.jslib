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

	OnGetPoint: function () {
		return getPoint();
	},

	OnGetCar: function () {
		return getCar();
	},
});