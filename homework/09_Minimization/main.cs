using System;
using static System.Console;
using static System.Math;

public class main{
    public static void Main(){
        WriteLine("---Task A-------");
        vector x_min; int step; bool exceeded_step_max;

        WriteLine("- Rosenbrock: Find a minimum of the Rosenbrock's valley function f(x,y) = (1-x)^2+100(y-x^2)^2 using Newton's method with forward gradient");
        Func<vector,double> f_rosenbrock = delegate(vector x){return Pow((1-x[0]), 2) + 100 * Pow((x[1] - x[0]*x[0]), 2);};
        
        vector[] x_start = {new vector(3,3), new vector(2, -2), new vector(3,-3), new vector(-2,2)};
        for(int i = 0; i<x_start.Length; i++){
            (x_min, step, exceeded_step_max) = minimization.newton(f_rosenbrock, x_start[i]);
            double f_min = f_rosenbrock(x_min);
            WriteLine($"Startpoint: {x_start[i].getString()} Minimum: xmin = {x_min.getString("{0,4:g7}")} ; f(xmin) = {f_min}; Steps: {step}; Excedded max number of steps?: {exceeded_step_max}");
        }
        WriteLine("Exact minimum: xmin = (1, 1) ;  f(xmin) = 0  \n");

        WriteLine("- Himmelblau: Find a minimum of the Himmelblau's function f(x,y)=(x^2+y-11)^2+(x+y^2-7)^2 using Newton's method with forward gradient");
        Func<vector,double> f_himmel = delegate(vector x){return Pow(x[0]*x[0] + x[1] - 11, 2) + Pow(x[0] + x[1]*x[1]-7 ,2);};
        vector[] x_start2 = {new vector(5,5), new vector(-5, 5), new vector(-5,-5), new vector(5,-5)};
        for(int i = 0; i<x_start.Length; i++){
            (x_min, step, exceeded_step_max) = minimization.newton(f_himmel, x_start2[i]);
            double f_min = f_himmel(x_min);
            WriteLine($"Startpoint: {x_start2[i].getString()} Minimum: xmin = {x_min.getString("{0,4:g7}")} ; f(xmin) = {f_min}; Steps: {step}; Excedded max number of steps?: {exceeded_step_max}");
        }
        WriteLine("Exact minima: xmin = (3,2) ;  f(xmin) = 0"); 
        WriteLine("              xmin = (-2.805118,3.131312) ;  f(xmin) = 0");    
        WriteLine("              xmin = (-3.779310,-3.283186) ;  f(xmin) = 0");    
        WriteLine("              xmin = (3.584428, -1.848126) ;  f(xmin) = 0 \n");    


        /*WriteLine(@"See 'xxxx.svg': 
        xxxx");*/

        
    }
}