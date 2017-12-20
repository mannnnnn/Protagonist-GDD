///findLabels(list)
var list = argument0;
var labelMap = ds_map_create();
var split = ds_list_create();
for (var i = 0; i < ds_list_size(list); i++)
{
    // make sure the string is a label
    string_split(list[| i], " ", split);
    if (ds_list_size(split) >= 2)
    {
        if (split[| 0] == "label")
        {
            // get the rest of the label
            var name = split[| 1];
            var line = i;
            var type = LABEL_TYPE_GOTO;
            // check for call or goto type label
            if (i < ds_list_size(list) - 1)
            {
                // if next is a bracket
                if (list[| i + 1] == "{")
                {
                    type = LABEL_TYPE_CALL;
                }
            }
            // if label already exists
            if (ds_map_exists(labelMap, name))
            {
                show_error("Label " + name + " is defined twice.", true);
            }
            // create label marker and add to map
            labelMap[? name] = createLabelMarker(name, line, type);
        }
    }
}
// free the split list
ds_list_destroy(split);
return labelMap;
