///parseToTree(str, rootList)
// turns an evaluatable string into a parse tree.
// except it's actually a super-nested list.
// aka a discount tree with lists standing in as discount objects.
// T.T thanks game maker for killing my soul

var str = string_trim(argument0);
var root = argument1;
var expression = splitByParen(str);

// dissolve surrounding parens, only used on str
if (ds_list_size(expression) == 1 && checkparen(str))
{
    str = unparen(str);
    root[| OPERATOR_TYPE] = OP_PAREN;
    root[| OPERAND_A] = parseToTree(str, ds_list_create());
    ds_list_mark_as_list(root, OPERAND_A);
    ds_list_destroy(expression);
    return root;
}
// parse expression
else
{
    // if one expression only
    if (ds_list_size(expression) == 1)
    {
        // parse it
        root[| OPERATOR_TYPE] = OP_GET;
        // string or variable or value
        if !(checkquote(expression[| 0]) || string_lettersdigits(expression[| 0]) == expression[| 0])
        {
            show_error("Unexpected token: The value or variable " + expression[| 0] + " is not valid.", true);
        }
        root[| OPERAND_A] = expression[| 0];
        ds_list_destroy(expression);
        return root;
    }
    else
    {
        // check for unary operators
        if (ds_list_size(expression) == 2)
        {
            switch (expression[| 0])
            {
                case '!':
                    root[| OPERATOR_TYPE] = OP_NOT;
                    root[| OPERAND_A] = parseToTree(expression[| 1], ds_list_create());
                    ds_list_mark_as_list(root, OPERAND_A);
                    ds_list_destroy(expression);
                    return root;
            }
        }
        // use expression list
        for (var i = 0; i < ds_list_size(expression); i++)
        {
            // start consuming input
            // split using binary operators && and ||
            switch (expression[| i])
            {
                case '&&':
                    root[| OPERATOR_TYPE] = OP_AND;
                    root[| OPERAND_A] = parseToTree(string_join(expression, "", 0, i), ds_list_create());
                    ds_list_mark_as_list(root, OPERAND_A);
                    root[| OPERAND_B] = parseToTree(string_join(expression, "", i + 1, ds_list_size(expression)), ds_list_create());
                    ds_list_mark_as_list(root, OPERAND_B);
                    ds_list_destroy(expression);
                    return root;
                case '||':
                    root[| OPERATOR_TYPE] = OP_OR;
                    root[| OPERAND_A] = parseToTree(string_join(expression, "", 0, i), ds_list_create());
                    ds_list_mark_as_list(root, OPERAND_A); 
                    root[| OPERAND_B] = parseToTree(string_join(expression, "", i + 1, ds_list_size(expression)), ds_list_create());
                    ds_list_mark_as_list(root, OPERAND_B); 
                    ds_list_destroy(expression);
                    return root;

            }
        }
        for (var i = 0; i < ds_list_size(expression); i++)
        {
            // split using binary operator ==, since it has lower priority
            switch (expression[| i])
            {
                case '==':
                    root[| OPERATOR_TYPE] = OP_EQ;
                    root[| OPERAND_A] = parseToTree(string_join(expression, "", 0, i), ds_list_create());
                    ds_list_mark_as_list(root, OPERAND_A);
                    root[| OPERAND_B] = parseToTree(string_join(expression, "", i + 1, ds_list_size(expression)), ds_list_create());
                    ds_list_mark_as_list(root, OPERAND_B);
                    ds_list_destroy(expression);
                    return root;
            }
        }
    }
}