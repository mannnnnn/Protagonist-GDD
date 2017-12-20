///createSpell(str, obj)
var str = argument0;
var obj = argument1;
// adds to playerStats map, inserts into sorted spell list for spellbook, adds false discovered entry to spellbook
obj_spellbook.spelleffects[? str] = obj;
// binary search for insertion point and insert
var index = spellSearchInsert(str);
if (index == noone)
{
    show_debug_message("Omoe wa mou shindeiru");
    show_error("Error in createSpell: Spell " + str + " is already in the spellbook.", true);
}
else if (index == -1)
{
    show_debug_message("Kek");
    show_error("Error in createSpell: Spell " + str + " has fucked up code.", true);
}
ds_list_insert(obj_spellbook.spells, index, str);
// add false discovered entry
obj_spellbook.spellfound[? str] = false;
