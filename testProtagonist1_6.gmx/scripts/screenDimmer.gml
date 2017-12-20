///screenDimmer(alpha, duration, ?openSpeed, ?closeSpeed, ?color)

//destroy all old ones
var startingAlpha = 0;
with (obj_screenDimmer)
{
    startingAlpha = map_range(timer, 0, duration, start, finish);
    instance_destroy();
}
// create the new dimmer
var dimmer = instance_create(0, 0, obj_screenDimmer);
dimmer.start = startingAlpha;
dimmer.finish = argument[0];
dimmer.duration = argument[1];
dimmer.state = OPENING;

//optional open/close speed args
if (argument_count >= 3)
{
    dimmer.openSpeed = argument[2];
}
if (argument_count >= 4)
{
    dimmer.closeSpeed = argument[3];
}
if (argument_count >= 5)
{
    dimmer.color = argument[4];
}

return dimmer;

// afterwards, you can state = CLOSING; to make it disappear.