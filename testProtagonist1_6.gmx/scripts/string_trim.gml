/// string_trim(input)
var str = argument0;
var length = string_length(str);
var start = 1;
var finish = length;

// find first non-whitespace char
for (var i = 1; i <= length; i++)
{
    start = i;
    // space character, and chars 9-13 in ascii are whitespace
    var c = string_char_at(str, i);
    if (c == ' ' || (ord(c) >= 9 && ord(c) <= 13))
    {
    }
    else
    {
        break;
    }
}

// find last non-whitespace char
for (var i = length; i >= max(1, start - 1); i--)
{
    finish = i;
    // space character, and chars 9-13 in ascii are whitespace
    var c = string_char_at(str, i);
    if (c == ' ' || (ord(c) >= 9 && ord(c) <= 13))
    {
        if (i == max(1, start - 1))
        {
            finish--;
        }
    }
    else
    {
        break;
    }
}

// find the substring that is first to last
if (start > finish)
{
    return "";
}
return string_copy(str, start, finish - start + 1);
