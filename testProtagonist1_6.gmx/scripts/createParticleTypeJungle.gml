///createParticleTypeJungle()
// Creates the jungle particle type and returns it.
var part = part_type_create();
part_type_shape(part, pt_shape_flare);
part_type_scale(part, 0.80, 1);
part_type_size(part, 0.01, 0.15, 0, 0);
part_type_speed(part, 0.10, 0.50, 0.01, 0.10);
part_type_direction(part, 31, 155, 0, 0);
part_type_orientation(part, 0, 360, 0, 0, false);
part_type_gravity(part, 0, 0);
part_type_colour3(part, 3604467, 16776846, 46430);
part_type_alpha3(part, 0, 0.47, 0);
part_type_blend(part, true);
part_type_life(part, 50, 100);
return part;
