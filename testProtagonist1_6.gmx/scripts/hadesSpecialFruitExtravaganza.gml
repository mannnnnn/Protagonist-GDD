///hadesSpecialFruitExtravaganza()
// add one of 3 random fruits to your inventory.
addItem("fruit" + string(choose(1, 2, 3)));
addNotification(createNotification("You got some fruit!"));
