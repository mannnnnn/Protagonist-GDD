///getTextSlice(str, textshow, textspeed)
var str = argument0;
var textshow = argument1;
var textspeed = argument2;
var chars = round(textshow * textspeed);
// slice
var str = string_copy(str, 1, min(chars, string_length(str)));
// count number of newlines
var newlines = string_count(newline(), str);
// add 2 * newlines
chars = chars - 1 + (2 * newlines);
if (chars < string_length(str))
{
    return string_copy(str, 1, chars);
}
return str;