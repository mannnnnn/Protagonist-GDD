///splitByParen(str, ?list)

var list = noone;
if (argument_count >= 2)
{
    list = argument[1];
}
else
{
    list = ds_list_create();
}

var str = string_trim(argument[0]);
var parens = 0;
var parenStr = "";
for (var i = 1; i <= string_length(str); i++)
{
    var c = string_char_at(str, i);
    if (parens == 0)
    {
        // ! is its own operator
        if (c == '!')
        {
            ds_list_add(list, c);
            ds_list_add(list, "");
            continue;
        }
        // && is its own operator
        if (c == '&')
        {
            if (ds_list_size(list) == 0 || list[| ds_list_size(list) - 1] != '&')
            {
                ds_list_add(list, c);
            }
            else
            {
                list[| ds_list_size(list) - 1] += c;
                ds_list_add(list, "");
            }
            continue;
        }
        // || is its own operator
        if (c == '|')
        {
            if (ds_list_size(list) == 0 || list[| ds_list_size(list) - 1] != '|')
            {
                ds_list_add(list, c);
            }
            else
            {
                list[| ds_list_size(list) - 1] += c;
                ds_list_add(list, "");
            }
            continue;
        }
        // == is its own operator
        if (c == '=')
        {
            if (ds_list_size(list) == 0 || list[| ds_list_size(list) - 1] != '=')
            {
                ds_list_add(list, c);
            }
            else
            {
                list[| ds_list_size(list) - 1] += c;
                ds_list_add(list, "");
            }
            continue;
        }
    }
    // parens
    if (c == '(')
    {
        parens++;
    }
    if (c == ')')
    {
        parens--;
        // if back to bottom level, save parenStr to list
        if (parens == 0)
        {
            parenStr += c;
            ds_list_add(list, parenStr);
            ds_list_add(list, "");
            parenStr = "";
        }
    }
    if (parens > 0)
    {
        parenStr += c;
    }
    // if not covered by parenStr, add non-whitespace chars to last entry
    if (parens == 0 && c != ')')
    {
        if (c == ' ')
        {
            ds_list_add(list, "");
        }
        else
        {
            if (ds_list_empty(list))
            {
                ds_list_add(list, "");
            }
            list[| ds_list_size(list) - 1] += c;
        }
    }
}
// remove empty strings
for (var i = 0; i < ds_list_size(list); i++)
{
    if (list[| i] == '')
    {
        ds_list_delete(list, i);
        i--;
    }
}

return list;