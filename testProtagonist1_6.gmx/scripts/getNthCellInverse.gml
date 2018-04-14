///getNthCellInverse(start, border, width, pos)
// gets the nth cell position
var startX = argument0;
var border = argument1;
var width = argument2;
var pos = argument3;
return floor((pos - startX - border) / (border + width));
