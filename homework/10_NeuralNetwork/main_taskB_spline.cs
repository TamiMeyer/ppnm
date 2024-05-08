using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;

public class main_taskB_spline{
    public static void Main(){
        //get the x points of the training data
        List<double> x_train_list = new List<double>{};
        List<double> y_train_list = new List<double>{};
        var instream=new System.IO.StreamReader("Out.train.data");
        char[] split_delimiters = {' ','\t','\n'};
        var split_options = System.StringSplitOptions.RemoveEmptyEntries;
        for(string line=instream.ReadLine(); line!=null; line=instream.ReadLine()){  
	        var numbers = line.Split(split_delimiters,split_options);
            x_train_list.Add(double.Parse(numbers[0]));
            y_train_list.Add(double.Parse(numbers[1]));
        }
        instream.Close();
        double[] x_train = x_train_list.ToArray();
        double[] y_train = y_train_list.ToArray();

        //produce cspline result
        var outstream=new System.IO.StreamWriter("Out.spline_antiderivative.data", append:false);
        outstream.WriteLine("Antiderivative of g(x)=Cos(5*x-1)*Exp(-x*x) determined by cubic spline using the same training data as for ANN");

        cspline cspline1 = new cspline(new vector(x_train), new vector(y_train));
        for(double z = -1; z<=1; z+=1.0/64){  
          outstream.WriteLine($"{z} {cspline1.integral(z)}");
        }
        outstream.Close();


    }
}