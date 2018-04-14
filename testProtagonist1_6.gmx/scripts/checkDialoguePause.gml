///checkDialoguePause()
// checks if everything should be paused for dialogue

// pause everything if it's a special case
return checkDialogueActive() && instanceof(obj_dialogue.display, obj_dialogueBoxSpecial);
