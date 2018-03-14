///createLetter(x, y, value, ?obj)
// creates a combat letter
var type = obj_letter;
if (argument_count >= 4)
{
    type = argument[3];
}
var obj = instance_create(argument[0], argument[1], type);
obj.value = argument[2];
return obj;