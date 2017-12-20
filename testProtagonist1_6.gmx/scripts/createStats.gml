///createStats(?extroversion, ?magic, ?speed, ?control, ?damage)
// creates a list that represents a set of stats.
// can be applied to both weapons and the player

var s = ds_list_create();
s[| STAT_EXTROVERSION] = 0;
s[| STAT_MAGIC] = 0;
s[| STAT_SPEED] = 0;
s[| STAT_CONTROL] = 0;
s[| STAT_DAMAGE] = 0;

// set stats
for (var i = 0; i < 5 && i < argument_count; i++)
{
    s[| i] = argument[i];
}

return s;