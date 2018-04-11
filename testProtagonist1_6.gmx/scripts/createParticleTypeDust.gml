///createParticleTypeDust(sys)
// Creates the dust particle type and returns it.
var part = part_type_create();
part_type_shape(part, pt_shape_explosion);
part_type_scale(part, 2.5, 2.5);
part_type_size(part, 0.9, 1, 0.15, 0);
part_type_speed(part, 0, 0, 0, 0);
part_type_direction(part, 0, 0, 0, 0);
part_type_orientation(part, 0, 360, 0, 0, false);
part_type_gravity(part, 0, 0);
part_type_colour1(part, c_dkgray);
part_type_alpha2(part, 1, 0);
part_type_blend(part, false);
part_type_life(part, 13, 13);
return part;
