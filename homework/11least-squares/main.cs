using System;
using static System.Console;
using System.Collections.Generic;

public static class main{
    public static int Main(string[] args){
        /*obtain the experimental data from the input file*/
        List<double> x_raw_list = new List<double>{};
        List<double> y_raw_list = new List<double>{};
        List<double> dy_raw_list = new List<double>{};
        string infile=null;
        foreach(var arg in args){
	        var words = arg.Split(':');
    	    if(words[0]=="-input"){infile=words[1];}
	    }
        if(infile==null){
            Error.WriteLine("wrong filename argument");
            return 1;
        }
        var instream=new System.IO.StreamReader(infile);
        instream.ReadLine(); //skip the first line (it contains the coloumn labels)
        char[] split_delimiters = {' ','\t','\n'};
        var split_options = System.StringSplitOptions.RemoveEmptyEntries;
        for(string line=instream.ReadLine(); line!=null; line=instream.ReadLine()){  
	        var numbers = line.Split(split_delimiters,split_options);
            x_raw_list.Add(double.Parse(numbers[0]));
            y_raw_list.Add(double.Parse(numbers[1]));
            dy_raw_list.Add(double.Parse(numbers[2]));
        }
        instream.Close();
        double[] x_raw = x_raw_list.ToArray();
        double[] y_raw = y_raw_list.ToArray();
        double[] dy_raw = dy_raw_list.ToArray();

        WriteLine("---Task A--------");
        double[] d_ln_y_raw = new double[y_raw.Length];
        double[] ln_y_raw = new double[y_raw.Length];
        for(int i = 0; i<y_raw.Length;i++){
            d_ln_y_raw[i] = dy_raw[i]/y_raw[i];
            ln_y_raw[i] = Math.Log(y_raw[i]);
        }
        Func<double,double>[] fs = new Func<double,double>[] { z => 1.0, z => -z};
        vector x = new vector(x_raw);
        vector ln_y = new vector(ln_y_raw);
        vector d_ln_y = new vector(d_ln_y_raw);

        var (c,S) = fit.lsfit(fs, x, ln_y, d_ln_y); //least-square fit

        string outfile="Out.experiment_fitted.data";
        var outstream=new System.IO.StreamWriter(outfile, append:false);

        c.fprint(outstream, "Coefficients of least square fit (z => 1.0, z => -z):\n", "{0,10:g3} ");
        //for(int i=0;i<x_raw.size;i++){
        //    outstream.WriteLine($"{x_raw[i]} {y_raw[i]} {dy_raw[i]}");
        //}
        outstream.Close();
        WriteLine("Fit the experimental data with exponential function in the usual logarithmic way: (See Out.experiment_fitted.data)");
        WriteLine($"    ln(t)=ln(a)-lambda*t with ln(a)={c[0]:0.000} and lambda={c[1]:0.000}");
        WriteLine();

        WriteLine($"Half-life time of ThX from fitted experimental data: T½ = ln(2)/λ = {Math.Log(2)/c[1]:0.000} days");
        WriteLine("Half-life time of 224Ra: 3.632 days");
        WriteLine();

        WriteLine("See Out.fit.svg: Plot the experimental data (with error-bars) and your best fit.");

        WriteLine("---Task B--------");
        S.print("Covariance matrix S = ");


        return 0;
        
    }


}