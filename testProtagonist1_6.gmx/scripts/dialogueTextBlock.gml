///dialogueTextBlock(text, name, ?side)

// argument check
if (argument_count < 2)
{
    show_error("dialogueTextBlock requires at least 2 arguments.", true);
}
var side = noone;
if (argument_count >= 3)
{
    side = argument[2];
}

// open
if (obj_dialogue.display.state == CLOSED || obj_dialogue.display.state == CLOSING)
{
    obj_dialogue.display.state = OPENING;
}

// reset text
draw_set_font(fnt_dialogue);
obj_dialogue.display.text = breakText(argument[0], obj_dialogue.display.width - (2 * obj_dialogue.display.textBorder));
obj_dialogue.display.currentText = "";
obj_dialogue.display.textshow = 0;


// if it's to be displayed on the left
if (side != RIGHT)
{
    obj_dialogue.display.name = argument[1];
    obj_dialogue.display.nameR = "";
}
// otherwise, display it on the right
else
{
    obj_dialogue.display.nameR = argument[1];
    obj_dialogue.display.name = "";
}