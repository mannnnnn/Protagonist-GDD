/// stringToValue(str)
// converts a string to value

var str = argument0;
// if it has quotes, it's a string
if (checkquote(str))
{
    return unquote(str);
}
// if it's a number, it's a number
if (checkReal(str))
{
    return real(str);
}
// return the string otherwise
return str;