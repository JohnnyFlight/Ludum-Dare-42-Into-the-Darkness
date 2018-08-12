color([0.72, 0.45, 0.2])
linear_extrude(height = 20, twist = 360 * 2, $fn = 200)
{
    translate([5, 0])
    circle($fn = 32);
}