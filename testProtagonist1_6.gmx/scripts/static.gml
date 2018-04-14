///static()
// makes sure only one object of that type can exist.
// returns true if the instance is not destroyed.

var count = 0;
var objtype = object_index;
with (objtype)
{
    if (object_index == objtype)
    {
        count++;
    }
}
if (count > 0)
{
    instance_destroy();
    return false;
}
return true;
