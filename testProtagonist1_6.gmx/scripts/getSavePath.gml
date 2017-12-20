///getSavePath(?save, ?filename)
var s = "userdata/saves";
if (argument_count >= 1)
{
    s = s + "/" + "save" + string(argument[0]);
}
if (argument_count >= 2)
{
    s = s + "/" + string(argument[1]);
}
return s;