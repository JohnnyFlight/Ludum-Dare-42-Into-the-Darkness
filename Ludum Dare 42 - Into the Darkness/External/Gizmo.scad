difference()
{
    color("red")
    sphere(20);
    
    translate([0, 0, 20])
    color("pink")
    sphere(10);
    
    translate([0, 20, 0])
    color("pink")
    sphere(10);
    
    translate([20, 0, 0])
    color("pink")
    sphere(10);
    
    translate([0, -20, 0])
    color("pink")
    sphere(10);
}

translate([0, 0, 10])
color("orange")
sphere(10);

translate([10, 0, 0])
color("orange")
sphere(10);

translate([0, 10, 0])
color("orange")
sphere(10);