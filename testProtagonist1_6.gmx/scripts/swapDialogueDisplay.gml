///swapDialogueDisplay(obj)
// changes obj_dialogue.display to another object type
// use at your own risk. if the target display doesn't support this, it won't work.

var current = obj_dialogue.display;

// hiding or not
var oldhide = current.hide;
var oldwait = current.wait;

// show or hide name, text, and window
var oldshowName = current.showName;
var oldshowText = current.showText;
var oldshowWindow = current.showWindow;

// full block of text to be displayed
var oldtext = current.text;
// what is being showed
var oldcurrentText = current.currentText;
// name to display
var oldname = current.name;

// text incrementing (typewriter effect)
var oldtextshow = current.textshow;
var oldtextspeed = current.textspeed;

// state
var oldstate = current.state;
var oldtimer = current.timer;
var oldduration = current.duration;
var oldstatus = current.status;

with (current)
{
    instance_change(argument0, true);
}

// hiding or not
current.hide = oldhide;
current.wait = oldwait;

// show or hide name, text, and window
current.showName = oldshowName;
current.showText = true;
current.showWindow = oldshowWindow;

// full block of text to be displayed
current.text = oldtext;
// what is being showed
current.currentText = oldcurrentText;
// name to display
current.name = oldname;

// text incrementing (typewriter effect)
current.textshow = oldtextshow;
current.textspeed = oldtextspeed;

// state
current.state = oldstate;
current.timer = oldtimer;
current.duration = oldduration;
current.status = oldstatus;

return current;
