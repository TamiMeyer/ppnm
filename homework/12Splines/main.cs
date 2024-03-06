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
        var outstream1=new System.IO.StreamWriter("Out.linspline.sin.data", append:false);
        var outstream2=new System.IO.StreamWriter("Out.raw.sin.data", append:false);
        int n = 10; //number of given datapoints
        double[] x_arr=  new double[n];
        double[] y_arr=  new double[n];
        for(int i = 0; i<10;i++){
            x_arr[i]=i;
            y_arr[i]=Math.Sin(i);
            outstream2.WriteLine($"{i} {y_arr[i]}");
        }

        for(double z = 1.0/64; z<=9; z+=1.0/64){  
            outstream1.WriteLine($"{z} {linspline.linterp(x_arr, y_arr, z)}");
        }

        outstream1.Close();
        outstream2.Close();

        return 0;
    }

}