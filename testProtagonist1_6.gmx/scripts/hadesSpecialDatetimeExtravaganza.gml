///hadesSpecialDatetimExtravaganza()
// Custom dialogue script, for when Hades knows the date.

var date = current_day;
var datestr = string(date);
switch (date mod 10)
{
    case 1:
        datestr += "st";
        break;
    case 2:
        datestr += "nd";
        break;
    case 3:
        datestr += "rd";
        break;
    default:
        datestr += "th";
        break;
}

var str = 'H: "Speaking of era, what day is today? The ' + datestr + '?"';
dialogueParse(str);
obj_dialogue.scriptPause = true;


