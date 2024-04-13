using System;
using static System.Console;
public class main{
    public static int Main(){
        WriteLine("---Task A-------");
        //Func<vector,vector> f;
        //vector x;
        //vector fres, res; 

        String[] title_arr ={
            "1. Problem: 0 = x^2 - 4 = f(x)",
            "2. Problem: "
            }; //titles
        Func<vector,vector>[] f_arr = {
            delegate(vector y){return new vector(y[0]*y[0] - 4);}
            
            }; //functions
        vector[] x_arr = {
            new vector(1.0)
            }; //starting points
        vector fres, res;

        for(int i = 0; i<f_arr.Length; i++){
        res = root.newton(f_arr[i], x_arr[i]);
        fres =f_arr[i](res);
        WriteLine(title_arr[i]);
        x_arr[i].print("Start point: ");
        res.print($"Newton's result: xN =  ");
        fres.print("f(xN) = ");
        WriteLine($"Does xN approximately fulfil the equation? => {fres.approx(new vector(res.size))}");
        WriteLine();
        }


        /* f = delegate(vector y){return new vector(y[0]*y[0] - 4);};
        x = new vector(1.0);
        res = root.newton(f, x);
        fres =f(res);
        WriteLine("1. Problem: 0 = x^2 - 4 = f(x)");
        x.print("Start point: ");
        res.print($"Newton's result: xN =  ");
        fres.print("f(xN) = ");
        WriteLine($"Does xN approximately fulfil the equation? => {fres.approx(new vector(0.0))}");
        WriteLine();*/



        return 0;
    }
}