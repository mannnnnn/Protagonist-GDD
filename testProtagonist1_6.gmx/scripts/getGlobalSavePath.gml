///getGlobalSavePath(?filename)
var s = "userdata/saves";
if (argument_count >= 2)
{
    s = s + "/" + string(argument[1]);
}
return s;