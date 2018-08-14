color("brown")
cube([20, 20, 5], true);

translate([0, 0, 8])
{
    color("red")
    sphere(10, $fn = 12);
}

translate([0, 0, 2.5])
for (i = [0 : 3])
{
    rotate([0, 0, 45 + 90 * i])
    color("grey")
    linear_extrude(15, twist = 90, $fn = 32)
    translate([10, 0, 0])
    circle($fn = 32);
}