///createParticleTypeSearBack(sys)
// Creates the searBack particle type and returns it.
var part = part_type_create();
part_type_shape(part, pt_shape_smoke);
part_type_scale(part, 0.32, 0.25);
part_type_size(part, 3, 4, 0.05, 0);
part_type_speed(part, 0, 0, 0, 0);
part_type_direction(part, 0, 0, 0, 0);
part_type_orientation(part, 2, 306, 2, 0, false);
part_type_gravity(part, 0.03, 270);
part_type_colour_mix(part, 3917311, 9606655);
part_type_alpha2(part, 1, 0);
part_type_blend(part, true);
part_type_life(part, 30, 30);
return part;
