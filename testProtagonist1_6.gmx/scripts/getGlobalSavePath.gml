///getGlobalSavePath(?filename)
var s = "userdata/saves";
if (argument_count >= 1)
{
    s = s + "/" + string(argument[0]);
}
return s;
