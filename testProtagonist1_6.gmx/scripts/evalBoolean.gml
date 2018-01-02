///evalBoolean(str)

// break it up into token types
var tree = parseToTree(argument0, ds_list_create());

// get value
var val = evalTree(tree, argument0);

// everything should be marked as list so don't worry
ds_list_destroy(tree);

// give the value back
return val;
