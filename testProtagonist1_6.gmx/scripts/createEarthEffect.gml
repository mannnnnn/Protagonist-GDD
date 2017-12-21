/// createEarthEffect(x1, x2, y1, xDir, yDir, length, dur, fadeDur, stayDur, delay)
var x1 = argument0;
var x2 = argument1;
var y1 = argument2;
var xDir = argument3;
var yDir = argument4;
var length = argument5;
var dur = argument6;
var fadeDur = argument7;
var stayDur = argument8;
var delay = argument9;

var eff = instance_create(0, 0, obj_earthEffect);
eff.x1 = x1;
eff.x2 = x2;
eff.y1 = y1;
eff.xDir = xDir;
eff.yDir = yDir;
eff.length = length;
eff.dur = dur;
eff.fadeDur = fadeDur;
eff.stayDur = stayDur;
eff.timer = -delay;
return eff;
