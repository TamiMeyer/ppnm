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
        var outstream5=new System.IO.StreamWriter("Out.raw.func1.data", append:false);
        var outstream6=new System.IO.StreamWriter("Out.qspline.func1.data", append:false);
        var outstream7=new System.IO.StreamWriter("Out.raw.xsquared.data", append:false);
        var outstream8=new System.IO.StreamWriter("Out.qspline.xsquared.data", append:false);
        var outstream9=new System.IO.StreamWriter("Out.qspline.sin.data", append:false);

        double[] x3_arr = new double[15];
        double[] y3_arr = new double[15];
    	for(int i=0; i<15; i++){
            x3_arr[i]=i;
            if(i<5) y3_arr[i]=1;
            if(i<10 && i>=5) y3_arr[i]=i;
            if(i<15 && i>=10) y3_arr[i]=i*i;
            outstream5.WriteLine($"{x3_arr[i]} {y3_arr[i]}");
        }
        qspline func1 = new qspline(new vector(x3_arr), new vector(y3_arr));
        for(double z = 1.0/64; z<=14; z+=1.0/64){  
          outstream6.WriteLine($"{z} {func1.evaluate(z)}");//
        }

        double[] x4_arr = new double[10];
        double[] y4_arr = new double[10];
    	for(int i=0; i<10; i++){
            x4_arr[i]=i;
            y4_arr[i]=i*i;
            outstream7.WriteLine($"{x4_arr[i]} {y4_arr[i]}");
        }
        qspline xsquared = new qspline(new vector(x4_arr), new vector(y4_arr));
        for(double z = 1.0/64; z<=9; z+=1.0/64){  
          outstream8.WriteLine($"{z} {xsquared.evaluate(z)}");//
        }

        qspline sin_qspline = new qspline(new vector(x_arr), new vector(y_arr));
        for(double z = 1.0/64; z<=9; z+=1.0/64){  
          outstream9.WriteLine($"{z} {sin_qspline.evaluate(z)}");//
        }
        sin_qspline.print_coefficients();

        outstream5.Close();
        outstream6.Close();
        outstream7.Close();
        outstream8.Close();
        outstream9.Close();




        return 0;
    }

}