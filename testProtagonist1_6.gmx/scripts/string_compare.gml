///string_compare(a, b)
var a = string_lower(argument0);
var b = string_lower(argument1);

// smaller strings come first
if (string_length(a) < string_length(b))
{
    return -1;
}
if (string_length(a) > string_length(b))
{
    return 1;
}

// then compare by contents
for (var i = 1; i <= string_length(a); i++)
{
    if (ord(string_char_at(a, i)) < ord(string_char_at(b, i)))
    {
        return -1;
    }
    if (ord(string_char_at(a, i)) > ord(string_char_at(b, i)))
    {
        return 1;
    }
}
// if all equal
return 0;