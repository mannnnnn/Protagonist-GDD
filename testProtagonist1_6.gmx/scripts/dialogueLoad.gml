///dialogueLoad(path)
// load dialogue from file

// stop previous dialogue
dialogueStop();

// destroy data structures
// dialogue
if (ds_exists(obj_dialogue.dialogue, ds_type_list))
{
    ds_list_destroy(obj_dialogue.dialogue);
}
// characters
if (ds_exists(obj_dialogue.characters, ds_type_map))
{
    var current = ds_map_find_first(obj_dialogue.characters);
    var size = ds_map_size(obj_dialogue.characters);
    for (var i = 0; i < size; i++)
    {
        var ch = obj_dialogue.characters[? current];
        // if the character exists
        if (ds_exists(ch, ds_type_list))
        {
            // destroy it
            ds_list_destroy(ch);
        }
        current = ds_map_find_next(obj_dialogue.characters, current);
    }
    ds_map_destroy(obj_dialogue.characters);
}
// labels
if (ds_exists(obj_dialogue.labels, ds_type_map))
{
    var current = ds_map_find_first(obj_dialogue.labels);
    var size = ds_map_size(obj_dialogue.labels);
    for (var i = 0; i < size; i++)
    {
        var label = obj_dialogue.labels[? current];
        // if the label exists
        if (ds_exists(label, ds_type_list))
        {
            // destroy it
            ds_list_destroy(label);
        }
        current = ds_map_find_next(obj_dialogue.labels, current);
    }
    ds_map_destroy(obj_dialogue.labels);
}

// load the new one
dialogue = readDialogueFile(argument0);
characters = findCharacters(dialogue);
labels = findLabels(dialogue);