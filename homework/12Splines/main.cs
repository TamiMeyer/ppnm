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

        outstream3.Close();
        outstream4.Close();

        return 0;
    }

}