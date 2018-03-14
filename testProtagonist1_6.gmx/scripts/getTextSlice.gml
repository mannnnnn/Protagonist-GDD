///getTextSlice(str, textshow, textspeed)
var str = argument0;
var textshow = argument1;
var textspeed = argument2;
var slice = "";
// slice by moving character by character
var p = 0;
var n = textshow * textspeed;
for (var i = 0; i < n; i = i)
{
    // if pausing speaking due to punctuation
    if (p > 0)
    {
        p--;
        n--;
    }
    // otherwise, write out more characters
    else
    {
        var c = string_char_at(str, i + 1);
        // pause if punctuation is seen
        if (ds_map_exists(obj_dialogue.speakpause, c))
        {
            p = obj_dialogue.speakpause[? c];
        }
        // print out more chars if invisible newline char is seen
        if (string_pos(newline(), c) > 0)
        {
            n++;
        }
        // add it to the string
        slice += c;
        // advance character
        i++;
        if (string_length(slice) == string_length(str))
        {
            return slice;
        }
    }
}
return slice;