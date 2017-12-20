///breakText(str, w)
var str = argument0;
var w = argument1;
var split = ds_list_create();
string_split(str, " ", split);
var brokenStr = "";
var line = "";

for (var i = 0; i < ds_list_size(split); i++)
{
    // if adding a word works
    if (string_width(line + split[| i]) <= w)
    {
        // add the word
        line += split[| i] + " ";
    }
    // otherwise wrap to newline and add to brokenStr
    else
    {
        brokenStr += line + newline();
        // add the word on the next line.
        line = split[| i] + " ";
    }
}

// add the rest of the line to brokenStr
if (line != "")
{
    brokenStr += line;
}

// free split list
ds_list_destroy(split);

// output
return brokenStr;