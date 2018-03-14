///hadesSpecialExitingExtravaganza()
// Custom dialogue script, for when Hades exits the game.

var exiter = instance_create(0, 0, obj_mouseController);
exiter.endGame = true;