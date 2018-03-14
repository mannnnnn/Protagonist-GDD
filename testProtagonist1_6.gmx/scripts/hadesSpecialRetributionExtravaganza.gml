/// hadesSpecialRetributionExtravaganza
// hades unpauses combat for your dying pleasure.
with (obj_jokeTestAth)
{
    if (surface_exists(surf))
    {
        draw_surface(surf, 0, 0);
        surface_free(surf);
    }
    
    // swap the new handler back
    obj_combat.handler = handler;
    
    // activate everything again
    instance_activate_all();
}
