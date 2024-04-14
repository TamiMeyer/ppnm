using System;
using static System.Console;
public class main{
    public static int Main(){
        WriteLine("---Task A-------");
        WriteLine("- Simple one- and two-dimensional equations:");
        String[] title_arr ={
            "1. Problem: 0 = x^2 - 4 = f(x)",
            "2. Problem: (0,0) = (x^2 + y^2 - 13, x - 2) = f(x, y)",
            }; //titles
        Func<vector,vector>[] f_arr = {
            delegate(vector y){return new vector(y[0]*y[0] - 4);},
            delegate(vector y){return new vector(y[0]*y[0] + y[1]*y[1] - 13, y[0] - 2);},
            }; //functions
        vector[] x_arr = {
            new vector(1.0),
            new vector(1.0, 1.0),
            }; //starting points
        vector fres, res;

        for(int i = 0; i<f_arr.Length; i++){
            res = root.newton(f_arr[i], x_arr[i]);
            fres =f_arr[i](res);
            WriteLine(title_arr[i]);
            x_arr[i].print("Start point: ");
            res.print($"Newton's result: r =  ", "{0,10:g8} ");
            fres.print("f(r) = ");
            WriteLine();
            WriteLine();
        }

        WriteLine("- Rosenbrock: Find the extremum(s) of the Rosenbrock's valley function f(x,y) = (1-x)^2+100(y-x^2)^2,");
        WriteLine("I.e.df/dx=0 and df/dy=0: (0, 0) = (-2*(1-x) - 2*x*200*(y-x^2), 200*(y-x^2)) has to be solved.");
        WriteLine("It is determined using Newton's method with several different starting points:");
        WriteLine();
        Func<vector,vector> f_rosenbrock_deri = delegate(vector y){return new vector(-2*(1 - y[0]) - 2*y[0]*200*(y[1] - y[0]*y[0]), 200 * (y[1] - y[0]*y[0]));};
        vector[] x_arr_ros = {
            new vector(2.0, 2.0),
            new vector(0.5,2.0)
            }; //starting points

        for(int i = 0; i<f_arr.Length; i++){
            res = root.newton(f_rosenbrock_deri, x_arr_ros[i]);
            fres =f_rosenbrock_deri(res);
            x_arr_ros[i].print("Start point: ");
            res.print($"Newton's result for extremum: r =  ", "{0,10:g8} ");
            fres.print("f(r) = ");
            WriteLine();
        }
        WriteLine("analytic: minimum at (1, 1) \n\n");


        WriteLine("-  Himmelblau: Find the minimum(s) of the Himmelblau's function f(x,y) = (x^2+y-11)^2+(x+y^2-7)^2.");
        WriteLine("I.e.df/dx=0 and df/dy=0: (0, 0) = (2x*2*(x^2+y-11) + 2(x+y^2-7), 2*(x^2+y-11) + 2*y*2*(x+y^2-7)) has to be solved.");
        WriteLine("It is determined using Newton's method with several different starting points:");
        WriteLine();
        Func<vector,vector> f_himmel_deri = delegate(vector y){return new vector(2*y[0]*2*(y[0]*y[0]+y[1]-11) + 2 * (y[0]+y[1]*y[1]-7), 2*(y[0]*y[0]+y[1]-11) + 2*y[1]*2*(y[0]+y[1]*y[1]-7));};
        vector[] x_arr_him = {
            new vector(4.0, 3.0),
            new vector(-3.0, 3.0),
            new vector(-3.0, -3.0),
            new vector(3.0, -2.0)
            }; //starting points

        for(int i = 0; i<x_arr_him.Length; i++){
            res = root.newton(f_himmel_deri, x_arr_him[i]);
            fres =f_himmel_deri(res);
            x_arr_him[i].print("Start point: ");
            res.print($"Newton's result for extremum: r =  ", "{0,10:g8} ");
            fres.print("f(r) = ");
            WriteLine();
        }
        WriteLine("analytic: minima at (3.0, 2.0), (-2.805118, 3.131312), (-3.779310, -3.283186) and (3.584428, -1.848126)");



        return 0;
    }
}