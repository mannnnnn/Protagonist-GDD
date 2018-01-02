///dialogueTextBlock(text, name)

// open
if (obj_dialogue.display.state == CLOSED || obj_dialogue.display.state == CLOSING)
{
    obj_dialogue.display.state = OPENING;
}

// reset text
draw_set_font(fnt_dialogue);
obj_dialogue.display.text = breakText(argument0, obj_dialogue.display.width - (2 * obj_dialogue.display.textBorder));
obj_dialogue.display.currentText = "";
obj_dialogue.display.textshow = 0;
obj_dialogue.display.name = argument1;