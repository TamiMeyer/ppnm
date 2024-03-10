using System;
using static System.Console;
public class main{
    public static int Main(){
        /*string outfile=null;
        foreach(var arg in args){
	        var words = arg.Split(':');
    	    if(words[0]=="-output"){outfile=words[1];}
	    }
        if(outfile==null){
            Error.WriteLine("wrong filename argument");
            return 1;
        }*/

        WriteLine("---Task A-------");
        WriteLine("See 'Out.linspline.sin.svg' and 'Out.linspline.exp.svg': The linear spline and integrator are tested for the sin function and exp function.");
        WriteLine();
        var outstream1=new System.IO.StreamWriter("Out.linspline.sin.data", append:false);
        var outstream2=new System.IO.StreamWriter("Out.raw.sin.data", append:false);
        var outstream3=new System.IO.StreamWriter("Out.linspline.exp.data", append:false);
        var outstream4=new System.IO.StreamWriter("Out.raw.exp.data", append:false);

        int n1 = 10; //number of given datapoints
        double[] x_arr=  new double[n1];
        double[] y_arr=  new double[n1];
        for(int i = 0; i<n1;i++){
            x_arr[i]=i;
            y_arr[i]=Math.Sin(i);
            outstream2.WriteLine($"{i} {y_arr[i]}");
        }

        for(double z = 1.0/64; z<=n1-1; z+=1.0/64){  
            outstream1.WriteLine($"{z} {linspline.linterp(x_arr, y_arr, z)} {linspline.linterpInteg(x_arr, y_arr, z)}");
        }

        int n2 = 13; //number of given datapoints
        double[] x2_arr=  new double[n2];
        double[] y2_arr=  new double[n2];
        for(int i = 0; i<n2;i++){
            x2_arr[i]=i;
            y2_arr[i]=Math.Exp(i);
            outstream4.WriteLine($"{i} {y2_arr[i]}");
        }

        for(double z = 1.0/64; z<=n2-1; z+=1.0/64){  
            outstream3.WriteLine($"{z} {linspline.linterp(x2_arr, y2_arr, z)} {linspline.linterpInteg(x2_arr, y2_arr, z)}");
        }
        outstream1.Close();
        outstream2.Close();
        outstream3.Close();
        outstream4.Close();

        WriteLine("---Task B-------");
        var outstream5=new System.IO.StreamWriter("Out.raw.xlin.data", append:false);
        var outstream6=new System.IO.StreamWriter("Out.qspline.xlin.data", append:false);
        var outstream7=new System.IO.StreamWriter("Out.raw.xsquared.data", append:false);
        var outstream8=new System.IO.StreamWriter("Out.qspline.xsquared.data", append:false);
        var outstream9=new System.IO.StreamWriter("Out.raw.const.data", append:false);
        var outstream10=new System.IO.StreamWriter("Out.qspline.const.data", append:false);
        var outstream11=new System.IO.StreamWriter("Out.qspline.sin.data", append:false);

        double[] x3_arr = new double[5];
        double[] y3_arr = new double[5];
    	for(int i=0; i<5; i++){
            x3_arr[i]=i;
            y3_arr[i]=4*i;
            outstream5.WriteLine($"{x3_arr[i]} {y3_arr[i]}");
        }
        qspline xlin = new qspline(new vector(x3_arr), new vector(y3_arr));
        for(double z = 1.0/64; z<=4; z+=1.0/64){  
          outstream6.WriteLine($"{z} {xlin.evaluate(z)} {xlin.integral(z)} {xlin.derivative(z)}");
        }

    	for(int i=0; i<5; i++){
            x3_arr[i]=i;
            y3_arr[i]=i*i;
            outstream7.WriteLine($"{x3_arr[i]} {y3_arr[i]}");
        }
        qspline xsquared = new qspline(new vector(x3_arr), new vector(y3_arr));
        for(double z = 1.0/64; z<=4; z+=1.0/64){  
          outstream8.WriteLine($"{z} {xsquared.evaluate(z)} {xsquared.integral(z)} {xsquared.derivative(z)}");
        }

    	for(int i=0; i<5; i++){
            x3_arr[i]=i;
            y3_arr[i]=4;
            outstream9.WriteLine($"{x3_arr[i]} {y3_arr[i]}");
        }
        qspline consta = new qspline(new vector(x3_arr), new vector(y3_arr));
        for(double z = 1.0/64; z<=4; z+=1.0/64){  
          outstream10.WriteLine($"{z} {consta.evaluate(z)} {consta.integral(z)} {consta.derivative(z)}");//
        }

        qspline sin_qspline = new qspline(new vector(x_arr), new vector(y_arr));
        for(double z = 1.0/64; z<=9; z+=1.0/64){  
          outstream11.WriteLine($"{z} {sin_qspline.evaluate(z)} {sin_qspline.integral(z)} {sin_qspline.derivative(z)}");//
        }

        outstream5.Close();
        outstream6.Close();
        outstream7.Close();
        outstream8.Close();
        outstream9.Close();
        outstream10.Close();
        outstream11.Close();

        WriteLine("See Out.qspline.func1.svg: Shows the quadratic interpolation (implemented by the qspline evaluate function), the integral of the interpolation and the derivative of the interpolation applied to a constant function, a linear function and a quadratic function.");
        WriteLine("The coefficients b_i and c_i of the plotted quadratic-splines calculated by the qspline class ( s_i(x) = y_i + b_i(x - x_i) + c_i(x - x_i)^2) :");
        WriteLine("Coefficents of constant function:");
        consta.print_coefficients();
        WriteLine("Coefficents of linear function:");
        xlin.print_coefficients();
        WriteLine("Coefficents of quadratic function:");
        xsquared.print_coefficients();
        WriteLine();
        WriteLine("See Out.qspline.sin.svg: Shows the quadratic interpolation (implemented by the qspline evaluate function), the integral of the interpolation and the derivative of the interpolation applied to sin(x).")
        WriteLine();

        WriteLine("---Task C-------");



        return 0;
    }

}