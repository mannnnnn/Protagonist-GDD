///combatStartCast(x, y, action)
// when the cast begins
var X = argument0;
var Y = argument1;
var action = argument2;
var spell = false;

// create spell effect
if (ds_map_exists(obj_spellbook.spelleffects, action))
{
    var obj = obj_spellbook.spelleffects[? action];
    if (object_exists(obj))
    {
        var eff = instance_create(0, 0, obj);
        eff.targetX = X;
        eff.targetY = Y;
        eff.action = action;
        spell = true;
    }
}

// tell the handler about this
if (instance_exists(obj_combat.handler))
{
    obj_combat.handler.X = X;
    obj_combat.handler.Y = Y;
    obj_combat.handler.action = action;
    with (obj_combat.handler)
    {
        event_user(SPELLCAST_START);
    }
}

// if the spell exists, and we discovered this spell, notify the player.
if (ds_map_exists(obj_spellbook.spelleffects, action) && obj_spellbook.spelleffects[? action] != noone
&& !obj_spellbook.spellfound[? action])
{
    obj_spellbook.spellfound[? action] = true;
    addNotification(createNotification("You discovered the spell " + string_upper(action) + "!"));
}

// break letters if in combat
if (instance_exists(obj_combatPuzzle))
{
    // for every letter
    for (var i = 0; i < ds_list_size(obj_spellInput.letters); i++)
    {
        var c = obj_spellInput.letters[| i];
        var letterX = obj_spellInput.posX + ((i + 0.5) * sprite_get_width(spr_Spells) * obj_spellInput.spellScale) + obj_spellInput.bordW;
        var letterY = obj_spellInput.posY + (0.5 * sprite_get_width(spr_Spells) * obj_spellInput.spellScale) + obj_spellInput.bordH;
        // if letter is in action, there is a chance of breakage by heat
        if (string_pos(c, action) > 0)
        {
            var heat = obj_spellInput.letterCooldowns[? c];
            if (heat > 50 && irandom_range(0, 100) < heat)
            {
                // break the letter
                obj_spellInput.letterCooldowns[? c] = -obj_spellInput.recovery;
                // explode
                effect_create_above(ef_explosion, letterX, letterY, obj_spellInput.spellScale, c_black);
                effect_create_above(ef_explosion, letterX, letterX, obj_spellInput.spellScale, make_color_rgb(200, 228, 7));
                // create fragments
                var n = irandom_range(3, 6);
                for (var i = 0; i < 360; i += 360 / n)
                {
                    var dir = i + irandom_range(-n / 360, n / 360);
                    var f = instance_create(GUIToRoomX(letterX), GUIToRoomY(letterY), obj_spellFragment);
                    f.dir = dir;
                    f.image_xscale = image_xscale;
                    f.image_yscale = image_yscale;
                    f.spd = f.spd * image_xscale;
                }
                playSound(SFX, snd_break, false);
            }
            // vowels can't be disabled
            else if (c != 'a' && c != 'e')
            {
                obj_spellInput.letterCooldowns[? c] += 20;
            }
        }
    }
}

return spell;
