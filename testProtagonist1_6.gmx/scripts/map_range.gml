///map_range(value, rangeOneMin, rangeOneMax, rangeTwoMin, rangeTwoMax)
// linearly interpolates one range to another.
var value = argument0;
var rangeOneMin = argument1;
var rangeOneMax = argument2;
var rangeTwoMin = argument3;
var rangeTwoMax = argument4;

return (((value - rangeOneMin) * ((rangeTwoMax - rangeTwoMin) / (rangeOneMax - rangeOneMin))) + rangeTwoMin);