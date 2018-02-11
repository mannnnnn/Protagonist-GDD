///createFireEffect(x1, y1, x2, y2, ps_shape, ps_distr, fireNum, cinderNum)
var x1 = argument0;
var y1 = argument1;
var x2 = argument2;
var y2 = argument3;
var ps_shape = argument4;
var ps_distr = argument5;
var fireNum = argument6;
var cinderNum = argument7;

part_emitter_region(obj_particles.systems[? PARTSYS_PUZ],
obj_particles.emitters[? PARTSYS_PUZ], x1, x2, y1, y2, ps_shape, ps_distr);
part_emitter_burst(obj_particles.systems[? PARTSYS_PUZ],
obj_particles.emitters[? PARTSYS_PUZ], obj_particles.effects[? PARTEFF_FIRE], fireNum);
part_emitter_burst(obj_particles.systems[? PARTSYS_PUZ],
obj_particles.emitters[? PARTSYS_PUZ], obj_particles.effects[? PARTEFF_CINDER], cinderNum);
