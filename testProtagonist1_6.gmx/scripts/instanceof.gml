///instanceof(instance, type)
// checks instance of.
var inst = argument0;
var obj = argument1;
return object_is_ancestor(inst.object_index, obj) || inst.object_index == obj;