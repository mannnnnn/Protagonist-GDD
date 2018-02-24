/// createLetterString(x, y, str, ?animate, ?delay, ?startdelay, ?scale, ?list, ?obj)
// creates an unused letter string

var X = argument[0];
var Y = argument[1];
var str = string_lower(argument[2]);

// whether or not letter flickers to greek or not
var anim = false;
if (argument_count >= 4)
{
    anim = argument[3];
}

// delay between each letter
var delay = 0;
if (argument_count >= 5)
{
    delay = argument[4];
}

// delay between each letter
var startdelay = 0;
if (argument_count >= 6)
{
    startdelay = argument[5];
}

// scale of letters
var scale = 1;
if (argument_count >= 7)
{
    scale = argument[6];
}

// list to store letters in
var list = noone;
if (argument_count >= 8 && is_real(argument[7]))
{
    list = argument[7];
    ds_list_clear(list);
}

// obj
var type = obj_letter;
if (argument_count >= 9)
{
    type = argument[8];
}

// create
if (delay <= 0)
{
    for (var i = 0; i < string_length(str); i++)
    {
        var obj = createLetter(X + (i * sprite_get_width(spr_Spells) * scale), Y, string_char_at(str, i), type);
        obj.depth = object_get_depth(obj.object_index) + 1;
        obj.spell = false;
        obj.str = str;
        obj.pos = i;
        obj.image_xscale = scale;
        obj.image_yscale = scale;
        if (!anim)
        {
            obj.normalSpd = 0;
        }
        if (list >= 0)
        {
            ds_list_add(list, obj);
        }
    }
}
else
{
    var obj = instance_create(X, Y, obj_spellString);
    obj.scale = scale;
    obj.anim = anim;
    if (list != noone)
    {
        ds_list_destroy(obj.list);
        obj.list = list;
        obj.selflist = false;
    }
    obj.type = type;
    obj.delay = delay;
    obj.str = str;
    obj.timer = -startdelay / delay;
    return obj;
}

return noone;
