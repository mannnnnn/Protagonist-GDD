///checkparen(str)
var str = argument0;
// if first and last are ( and )
return (string_char_at(str, 1) == '(' && string_char_at(str, string_length(str)) == ')');
