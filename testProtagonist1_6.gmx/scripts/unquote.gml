///unquote(str)
var str = argument0;
// if first and last are "
if (string_char_at(str, 1) == '"' && string_char_at(str, string_length(str)) == '"')
{
    // remove them
    return string_copy(str, 2, string_length(str) - 2);
}
// otherwise, return the normal string
return str;