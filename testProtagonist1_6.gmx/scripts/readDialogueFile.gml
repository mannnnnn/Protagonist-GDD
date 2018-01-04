///readDialogueFile(path)
// reads a dialogue file in

var filename = argument0;

if (!file_exists(filename))
{
    show_message("File " + filename + "does not exist.");
}

// open the file
var file = file_text_open_read(filename);
var eof = false;

// create the list
var list = ds_list_create();

// while we still have file left to read
while (!eof)
{
    // get string
    var str = file_text_readln(file);
    
    // remove all tabs
    str = string_replace_all(str, chr(9), "");
    // remove line feed and carriage return
    str = string_replace_all(str, chr(10), "");
    str = string_replace_all(str, chr(13), "");
    // remove spaces
    str = string_trim(str, "both", " ");
    
    // if non-empty
    if !(str == "")
    {
        // check for comment
        var pos = string_pos("//", str);
        // if comment is NOT the first char, or there is no comment
        if (pos > 1 || pos == 0)
        {
            // remove everything after the comment mark, if it exists
            if (pos > 0)
            {
                str = string_copy(str, 1, pos - 1);
            }
            // add to the list
            ds_list_add(list, str);
        }
    }
    
    // check end of file
    eof = file_text_eof(file);
}

// close the file
file_text_close(file);

// add exit at the end for safety
ds_list_add(list, "exit");

// send back the list
return list;
