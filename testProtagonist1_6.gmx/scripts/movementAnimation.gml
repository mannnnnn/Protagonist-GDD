///movementAnimation(obj, target, startX, startY, startVx, startVy)
var obj = argument0;
var target = argument1;
var startX = argument2;
var startY = argument3;
var startVx = argument4;
var startVy = argument5;
var moveAnim = instance_create(0, 0, obj);
moveAnim.target = target;
moveAnim.X = startX;
moveAnim.Y = startY;
moveAnim.VelX = startVx;
moveAnim.VelY = startVy;
return moveAnim;
