///createDrawer(queueEntry, type)
var entry = argument0;
var type = argument1;
var obj = noone;

if (entry[| DRAWER_QUEUE_TYPE] == DRAWER_QUEUE_TYPE_NORMAL)
{
    // show normal
    if (entry[| DRAWER_QUEUE_ACTION] == DRAWER_QUEUE_ACTION_SHOW)
    {
        var channel = entry[| DRAWER_QUEUE_CHANNEL];
        // find and end old
        if (ds_map_exists(obj_dialogue.drawers, channel))
        {
            var old = obj_dialogue.drawers[? channel];
            if (instance_exists(old))
            {
                removeDrawer(old);
            }
        }
        // start new
        if (ds_map_exists(obj_dialogue.drawerKeywords, type))
        {
            obj = obj_dialogue.drawerKeywords[? type];
        }
        else
        {
            show_error("The show effect " + string(type) + " is not defined.", true);
        }
        var drawer = instance_create(0, 0, obj);
        // settings
        drawer.sprite = asset_get_index(entry[| DRAWER_QUEUE_SPRITENAME]);
        drawer.image = entry[| DRAWER_QUEUE_IMAGE];
        drawer.posX = entry[| DRAWER_QUEUE_POS_X];
        drawer.posY = entry[| DRAWER_QUEUE_POS_Y];
        drawer.scaleX = entry[| DRAWER_QUEUE_SCALE_X];
        drawer.scaleY = entry[| DRAWER_QUEUE_SCALE_Y];
        drawer.angle = entry[| DRAWER_QUEUE_ANGLE];
        drawer.alpha = entry[| DRAWER_QUEUE_ALPHA];
        obj_dialogue.drawers[? channel] = drawer;
    }
    // hide normal
    else
    {
        var channel = entry[| DRAWER_QUEUE_CHANNEL];
        // if same type, find and end old, and we're done
        var old = noone;
        if (ds_map_exists(obj_dialogue.drawers, channel))
        {
            old = obj_dialogue.drawers[? channel];
            if (instance_exists(old))
            {
                if (old.type == type)
                {
                    // if we can just remove the old one to the same effect, do that
                    removeDrawer(old);
                    return noone;
                }
            }
            else
            {
                // if old doesn't exist, then we're already hidden
                return noone;
            }
        }
        // if nothing is drawn on this channel, we're already hidden
        else
        {
            return noone;
        }
        // if different types... (old exists)
        if (ds_map_exists(obj_dialogue.drawerHideKeywords, type))
        {
            obj = obj_dialogue.drawerHideKeywords[? type];
        }
        else
        {
            show_error("The hide effect " + string(type) + " is not defined.", true);
        }
        var drawer = instance_create(0, 0, obj);
        // take settings
        drawer.sprite = old.sprite;
        drawer.image = old.image;
        drawer.posX = old.posX;
        drawer.posY = old.posY;
        drawer.scaleX = old.scaleX;
        drawer.scaleY = old.scaleY;
        drawer.angle = old.angle;
        drawer.alpha = old.alpha;
        // destroy the old
        with (old)
        {
            instance_destroy();
        }
        // set the new one in its place
        obj_dialogue.drawers[? channel] = drawer;
    }
}
else
{
    // show scene
    if (entry[| DRAWER_QUEUE_ACTION] == DRAWER_QUEUE_ACTION_SHOW)
    {
        var channel = entry[| DRAWER_QUEUE_CHANNEL];
        // find and end old
        if (ds_map_exists(obj_dialogue.drawers, channel))
        {
            var old = obj_dialogue.drawers[? channel];
            if (instance_exists(old))
            {
                removeDrawer(old);
            }
        }
        // start new
        if (ds_map_exists(obj_dialogue.sceneDrawerKeywords, type))
        {
            obj = obj_dialogue.sceneDrawerKeywords[? type];
        }
        else
        {
            show_error("The scene effect " + string(type) + " is not defined.", true);
        }
        var drawer = instance_create(0, 0, obj);
        drawer.sprite = noone;
        drawer.image = noone;
        drawer.background = noone;
        // settings
        var sprbg = asset_get_index(entry[| DRAWER_QUEUE_SPRITENAME]);
        if (entry[| DRAWER_QUEUE_SPRITENAME] == sprite_get_name(sprbg))
        {
            drawer.sprite = sprbg;
            drawer.image = entry[| DRAWER_QUEUE_IMAGE];
        }
        else if (entry[| DRAWER_QUEUE_SPRITENAME] == background_get_name(sprbg))
        {
            drawer.background = sprbg;
        }
        drawer.alpha = entry[| DRAWER_QUEUE_ALPHA];
        obj_dialogue.drawers[? channel] = drawer;
    }
    // hide scene
    else
    {
        var channel = entry[| DRAWER_QUEUE_CHANNEL];
        // if same type, find and end old, and we're done
        var old = noone;
        if (ds_map_exists(obj_dialogue.drawers, channel))
        {
            old = obj_dialogue.drawers[? channel];
            if (instance_exists(old))
            {
                if (old.type == type)
                {
                    // if we can just remove the old one to the same effect, do that
                    removeDrawer(old);
                    return noone;
                }
            }
            else
            {
                // if old doesn't exist, then we're already hidden
                return noone;
            }
        }
        // if nothing is drawn on this channel, we're already hidden
        else
        {
            return noone;
        }
        // if different types... (old exists)
        if (ds_map_exists(obj_dialogue.sceneDrawerHideKeywords, type))
        {
            obj = obj_dialogue.sceneDrawerHideKeywords[? type];
        }
        else
        {
            show_error("The unscene effect " + string(type) + " is not defined.", true);
        }
        var drawer = instance_create(0, 0, obj);
        // take settings
        drawer.sprite = old.sprite;
        drawer.image = old.image;
        drawer.background = old.background;
        drawer.alpha = old.alpha;
        // destroy the old
        with (old)
        {
            instance_destroy();
        }
        // set the new one in its place
        obj_dialogue.drawers[? channel] = drawer;
    }
}