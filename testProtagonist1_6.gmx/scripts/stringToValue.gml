/// stringToValue(str, ?line)
// converts a string to value

var str = argument[0];
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

// if it's a boolean, return that
if (str == "true")
{
    return true;
}
if (str == "false")
{
    return false;
}

// return it as a variable value if dialogue-related
if (argument_count >= 2)
{
    return getDialogueVar(str, argument[1]);
}
// otherwise just return the str
else
{
    return str;
}