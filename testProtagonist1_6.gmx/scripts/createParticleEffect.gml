///createParticleEffect(SYS, EFF, x1, y1, x2, y2, ps_shape, ps_distr, num)
var sys = argument0;
var eff = argument1;
var x1 = argument2;
var y1 = argument3;
var x2 = argument4;
var y2 = argument5;
var ps_shape = argument6;
var ps_distr = argument7;
var num = argument8;

part_emitter_region(obj_particles.systems[? sys],
obj_particles.emitters[? sys], x1, x2, y1, y2, ps_shape, ps_distr);
part_emitter_burst(obj_particles.systems[? sys],
obj_particles.emitters[? sys], obj_particles.effects[? eff], num);
