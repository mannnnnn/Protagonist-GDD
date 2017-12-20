///unquote(str)
var str = argument0;
// if first and last are "
return (string_char_at(str, 1) == '"' && string_char_at(str, string_length(str)) == '"')