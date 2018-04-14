///createConfWindow(obj, text, options)
if (!objectof(argument0, obj_confWindow))
{
    show_error("Error in createConfWindow(obj, text, options): " + object_get_name(argument0) + " is not a child of obj_confWindow.", true);
}
var conf = instance_create(0, 0, argument0);
conf.text = argument1;
conf.options = argument2;
return conf;
