using System;
using static System.Console;
using System.Collections.Generic;
public class main{
    public static int Main(){
        WriteLine("---Task A-------");
        WriteLine("The least-squares signal smoothing is implemented using QR-decomposition to solve the linear equation A*y_smooth=y_raw. \n");
        
        //print matrices (just for demonstration)
        WriteLine("Example of matrix D: The second derivative of a discrete signal is approximated by its secondorder difference, Dx,");
        WriteLine("where the matrix D (e.g. for a set of 5 datapoints) is given as:");
        matrix D = smooth.secondDerivative(5);
        D.print("D = ");
        WriteLine();
        WriteLine("Example of matrix A for smoothing parameter lambda = 1 and 5 data points:");
        matrix A = smooth.lineqMatrix(1,5);
        A.print("A = 1+λD^TD =  ");
        WriteLine();

        WriteLine(@"See 'Out.smoothQR.svg':
        The figure shows a noisy signal (generated by chatGPT) and the smoothened signal 
        (using QR-decomposition) with 3 different smoothing parameters lambda.");
        WriteLine();
        //Read the raw noisy signal data from the file
        List<double> x_raw_list = new List<double>{};
        List<double> y_raw_list = new List<double>{};
        var instream=new System.IO.StreamReader("signal.data");
        instream.ReadLine(); //skip the first line (it contains the title)
        char[] split_delimiters = {' ','\t','\n'};
        var split_options = System.StringSplitOptions.RemoveEmptyEntries;
        for(string line=instream.ReadLine(); line!=null; line=instream.ReadLine()){  
	        var numbers = line.Split(split_delimiters,split_options);
            x_raw_list.Add(double.Parse(numbers[0]));
            y_raw_list.Add(double.Parse(numbers[1]));
        }
        instream.Close();
        double[] x_raw = x_raw_list.ToArray();
        double[] y_raw = y_raw_list.ToArray();
        vector y_raw_vec = new vector(y_raw);

        //smoothing with QR-decomposition for different lambda values
        double lambda_a = 1;
        double lambda_b = 8;
        double lambda_c = 80;
        vector y_smooth_a = smooth.smoothQR(y_raw_vec, lambda_a);
        vector y_smooth_b = smooth.smoothQR(y_raw_vec, lambda_b);
        vector y_smooth_c = smooth.smoothQR(y_raw_vec, lambda_c);

        //write noisy and smooth data to new file
        var outstream=new System.IO.StreamWriter("Out.smoothsignal.data", append:false);
        outstream.WriteLine($"x y_raw lambda={lambda_a} lambda={lambda_b} lambda={lambda_c}");
        for(int i=0; i<y_raw_vec.size; i++){
            outstream.WriteLine($"{x_raw[i]} {y_raw[i]} {y_smooth_a[i]} {y_smooth_b[i]} {y_smooth_c[i]}");
        }

        WriteLine("---Task B-------");
        WriteLine("Signal with random noise is generated. And the smoothing with QR-decomposition is applied.");


        WriteLine(@"See 'xxxx.svg': 
        xxxx");

        return 0;
    }
}