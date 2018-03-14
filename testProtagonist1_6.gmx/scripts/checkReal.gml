///checkReal(str)
// checks if number is a valid real.
var str = argument0;
var pt = false;
var minus = true;
for (var i = 1; i <= string_length(str); i++)
{
    var char = string_char_at(str, i);
    if (ord(char) >= 48 && ord(char) <= 57)
    {
        // numbers are OK
        // but no minus signs after a number
        minus = false;
    }
    else if (char == '.')
    {
        // if we've already seen a decimal point, invalid number
        if (pt)
        {
            return false;
        }
        // we now have seen a decimal point
        pt = !pt;
        // but no minus signs after a decimal point
        minus = false;
    }
    else if (char == '-')
    {
        if (minus)
        {
            // fine if it's the first character
        }
        else
        {
            // otherwise, not a number
            return false;
        }
    }
    // if not digit or number
    else
    {
        // not a number
        return false;
    }
}
// if all passes, it's a number
return true;