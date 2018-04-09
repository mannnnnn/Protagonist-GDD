///dialogueAdvance()
// execute instructions until dialogueNextAction() returns false
// and as long as active, execute instructions

// advancing is only allowed if dialogue isn't in a menu
// if dialogue IS in a menu, dialogue can only advance by selecting an option

// advancing is also disabled when dialogue is on wait mode
if (!dialogueCheckMenu() && !checkDialogueWait())
{
    // advance
    var pause = dialogueNextAction();
    while (obj_dialogue.active && !pause)
    {
        pause = dialogueNextAction();
    }
    
    return true;
}
return false;
