///combatEndCast(x, y, action)
// when the first letter hits
var X = argument0;
var Y = argument1;
var action = argument2;

// example
if (action == "act")
{
    with (obj_dustBunnyCreator)
    {
        /// EXPLOOOSION!
        if (point_in_rectangle(X, Y, getX(id), getY(id), getX(id) + sprite_width, getY(id) + sprite_height))
        {
            effect_create_above(ef_explosion, getX(id) + 0.5 * sprite_width, getY(id) + 0.5 * sprite_height, 5, c_orange);
            effect_create_above(ef_explosion, getX(id) + 0.5 * sprite_width, getY(id) + 0.5 * sprite_height, 5, c_dkgray);
            
            // create bunnies
            repeat(n)
            {
                var bunny = instance_create(getX(id) + 0.5 * sprite_width, getY(id) + 0.5 * sprite_height, obj_dustBunny);
                bunny.timer = bunny.durationMax;
            }
            
            active = false;
        }
    }
    
    with (obj_letter)
    {
        // if part of the question
        if (!spell)
        {
            // if within hitbox
            if (point_in_rectangle(X, Y, getX(id) - 0.5 * sprite_width, getY(id) - 0.5 * sprite_height,
            getX(id) + 0.5 * sprite_width, getY(id) + 0.5 * sprite_height))
            {
                // if string matches
                var success = false;
                switch (str)
                {
                    case "a cat in the":
                        success = inrange(pos, 9, 13);
                        break;
                    case "the game":
                        success = inrange(pos, 0, 3);
                        break;
                    case "compleete":
                        success = inrange(pos, 5, 7);
                        break;
                }
                // finish if successful
                if (success)
                {
                    var ef = createGroupEffect(obj_spellHitEffect, ds_list_create());
                    with (obj_spellString)
                    {
                        // add contents to effect list
                        for (var i = 0; i < ds_list_size(list); i++)
                        {
                            ds_list_add(ef.list, list[| i]);
                        }
                        // remove the spellString
                        instance_destroy();
                    }
                    // do another thing
                    if (str != "compleete")
                    {
                        createLetterString(100, 130, "puzzle", false, 2, 60);
                        createLetterString(100, 230, "compleete", false, 2, 74);
                    }
                    break;
                }
            }
        }
    }
}
