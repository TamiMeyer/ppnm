using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
public class main{
    public static int Main(string[] args){
        int n_example = 7;//recommend n>=7 in order to see the entire structure of the A matrix
        double lambda_example =1.0;
        int maxmaxN_timing = 0;
        foreach(var arg in args){
	        var words = arg.Split(':');
    	    if(words[0]=="-maxmaxN"){maxmaxN_timing= (int)float.Parse(words[1]);}
        }

        WriteLine("---Task A-------");
        WriteLine("The least-squares signal smoothing is implemented using Gram-Schmidt QR-decomposition to solve the linear equation A*y_smooth=y_raw. \n");
        
        //print matrices (just for demonstration)
        WriteLine("Example of matrix D: The second derivative of a discrete signal is approximated by its secondorder difference, Dx,");
        WriteLine($"where the matrix D (e.g. for a set of {n_example} datapoints) is given as:");
        matrix D = smooth.secondDerivative(n_example);
        D.print("D = ");
        WriteLine();
        WriteLine($"Example of matrix A for smoothing parameter lambda = {lambda_example} and {n_example} data points:");
        matrix A = smooth.lineqMatrix(lambda_example,n_example);
        A.print("A = 1+λD^TD =  ");
        WriteLine();

        WriteLine(@"See 'Out.smoothQR.svg':
        The figure shows a noisy signal (generated by chatGPT) and the smoothed signal (using QR-decomposition) with 3 
        different smoothing parameters lambda.");
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
        var outstream=new System.IO.StreamWriter("Out.smoothsignalQR.data", append:false);
        outstream.WriteLine($"x y_raw lambda={lambda_a} lambda={lambda_b} lambda={lambda_c}");
        for(int i=0; i<y_raw_vec.size; i++){
            outstream.WriteLine($"{x_raw[i]} {y_raw[i]} {y_smooth_a[i]} {y_smooth_b[i]} {y_smooth_c[i]}");
        }

        WriteLine("---Task B-------");
        //P:generate clean and noisy signal with polynom function
        int numDatapoints = 300;
        Func<double, double> clean_func = delegate(double z){return Pow(z,6)-3*Pow(z,5)-7*Pow(z,4)+15*Pow(z,3);};
        var (x_P, y_clean_P, y_noisy_P) = smooth.generateCleanAndNoisySignal(numDatapoints, clean_func, -2.5, 2.5, 15);

        //P:smoothing with QR-decomposition
        double lambda = 8000;
        vector y_smooth = smooth.smoothQR(y_noisy_P, lambda);

        //P:write clean,noisy and smooth data to new file
        var outstream2=new System.IO.StreamWriter("Out.smoothsignalQR_generated.data", append:false);
        outstream2.WriteLine($"x y_clean y_noisy lambda={lambda} (clean function f(z)=z^6-3z^5-7z^4+15z^3)");
        for(int i=0; i<y_noisy_P.size; i++){
            outstream2.WriteLine($"{x_P[i]} {y_clean_P[i]} {y_noisy_P[i]} {y_smooth[i]}");
        }
        outstream2.WriteLine($"\n\n");

        //S:generate clean and noisy signal with polynom function
        clean_func = delegate(double z){return Math.Sin(2*Math.PI*z/5);};
        var (x_S, y_clean_S, y_noisy_S) = smooth.generateCleanAndNoisySignal(numDatapoints, clean_func, 0, 5, 0.4);

        //S:smoothing with QR-decomposition
        y_smooth = smooth.smoothQR(y_noisy_S, lambda);

        //S:write clean,noisy and smooth data to new file
        outstream2.WriteLine($"x y_clean y_noisy lambda={lambda} (clean function f(z)=sin(z*2*PI/5))");
        for(int i=0; i<y_noisy_S.size; i++){
            outstream2.WriteLine($"{x_S[i]} {y_clean_S[i]} {y_noisy_S[i]} {y_smooth[i]}");
        }
        
        WriteLine($@"See 'Out.smoothQR_generated.svg':
        A clean signal with {numDatapoints} data points is generated and random noise is added to generate a noisy signal ('Out.smoothsignalQR_generated.data').
        This is done for a clean sin-wave and a clean polynom f(z)=z^6-3z^5-7z^4+15z^3.
        Then the smoothing with QR-decomposition is applied.");
        WriteLine();

        WriteLine("---Task C.1-------");
        WriteLine(@"The matrix A in this linear equation is pentadiagonal banded (has only 5 non-zero diagonals)
therefore in order to make the smoothing efficient, instead of QR-decomposition, we use this fact in LU factorization.
Before making use of the banded structure, I will implement LU factorization of a matrix. And show with an example that it works.

The linear equation A*y_smooth=y_raw is solved by LU factorization (here I still work with matrices which contain lots of zeros). 
For the example A from above:
");

        var (L,U) = smooth.LUdecomp(A);
        L.print("L = ");
        U.print("U = ");
        matrix LU = L*U;
        LU.print("Test: LU = ");
        WriteLine($"LU=A? => {LU.approx(A)} \n");

        WriteLine(@"See 'Out.smoothsignalLU_generated.svg':
        The LU factorization of the A matrix is applied for smoothing the generated data from the previous task (to make sure the
        LU factorization works). The implementation with LU achieves the same result as the implementation with QR, as can be seen from the figure or by comparing the data files 
        of the smoothed signals ('Out.smoothsignalQR_generated.data' and 'Out.smoothsignalLU_generated.data').");
        WriteLine();

        //clean and noisy data has already been generated: y_clean_P, y_noisy_P
        //smoothing with LU-decomposition
        y_smooth = smooth.smoothLU(y_noisy_P, lambda);
        //write clean,noisy and smooth data to new file
        var outstream3=new System.IO.StreamWriter("Out.smoothsignalLU_generated.data", append:false);
        outstream3.WriteLine($"x y_clean y_noisy lambda={lambda} (clean function f(z)=z^6-3z^5-7z^4+15z^3)");
        for(int i=0; i<y_noisy_P.size; i++){
            outstream3.WriteLine($"{x_P[i]} {y_clean_P[i]} {y_noisy_P[i]} {y_smooth[i]}");
        }
        outstream3.WriteLine($"\n\n");
        //clean and noisy data has already been generated: y_clean__S, y_noisy_S
        //smoothing with LU-decomposition
        y_smooth = smooth.smoothLU(y_noisy_S, lambda);
        //write clean,noisy and smooth data to new file
        outstream3.WriteLine($"x y_clean y_noisy lambda={lambda} (clean function f(z)=sin(z*2*PI/5))");
        for(int i=0; i<y_noisy_S.size; i++){
            outstream3.WriteLine($"{x_S[i]} {y_clean_S[i]} {y_noisy_S[i]} {y_smooth[i]}");
        }

        WriteLine("---Task C.2-------");
        WriteLine(@"Now, we make use of the banded structure of the A matrix, the fact that A is symmetric and that each non-zero 
diagonal is just a constant except from the ends of the diagonal (i.e. the corner elements).

First, I reduced the number of computations required to determine the matrix A and reduced the required storage for A by getting 
rid of the zeros in the A matrix.
The A matrix is fully determined by only 9 values: 4 values of the main diagonal (a), 3 values of the sub- and superdiagonal (b), 2 values
of the subsub- and supersuperdiagonal (c).
The diagonals of the A matrix of the previous example are determined by the following elements:");
        var (a,b,c) = smooth.lineqMatrix_eff(lambda_example);
        WriteLine("");
        a.print("a=");
        b.print("b=");
        c.print("c=");
        WriteLine();

        WriteLine(@"Secondly, an LU decomposition method for the A matrix with its special structure was implemented ('LUdecomp_eff(int n, vector a, vector b, vector c)'). 
L and U are banded matrices due to the structure of A. The diagonals of L from the previous example are determined or given by:");
        var(l,ll,lll,u,uu,uuu) = smooth.LUdecomp_eff(n_example, a, b, c);
        WriteLine("The main diagonal of L is determined by:");
        l.print("l= ");
        WriteLine("The subdiagonal of L is given by:");
        ll.print("ll= ");
        WriteLine("The subsubdiagonal of L is given by:");
        lll.print("lll= ");
        WriteLine("The supersuperdiagonal of U is determined by:");
        uuu.print("uuu= ");
        WriteLine("The superdiagonal of U is given by:");
        uu.print("uu= ");
        WriteLine("The maindiagonal of U is given by:");
        u.print("u= ");
        WriteLine();

        WriteLine(@"Thirdly, a method 'smoothLU_eff' was implemented, that smoothes a noisy signal vector and makes use of the banded structure of 
the L and U matrices. ");
        //clean and noisy data has already been generated: y_clean_P, y_noisy_P
        //smoothing with efficient LU-decomposition
        vector y_smooth_eff = smooth.smoothLU_eff(y_noisy_P, lambda);
        //write clean,noisy and smooth data to new file
        var outstream4=new System.IO.StreamWriter("Out.smoothsignalLU_generated_eff.data", append:false);
        outstream4.WriteLine($"x y_clean y_noisy lambda={lambda} (clean function f(z)=z^6-3z^5-7z^4+15z^3)");
        for(int i=0; i<y_noisy_P.size; i++){
            outstream4.WriteLine($"{x_P[i]} {y_clean_P[i]} {y_noisy_P[i]} {y_smooth_eff[i]}");
        }
        outstream4.WriteLine("\n\n");
        //clean and noisy data has already been generated: y_clean_S, y_noisy_S
        //smoothing with efficient LU-decomposition
        y_smooth_eff = smooth.smoothLU_eff(y_noisy_S, lambda);
        //write clean,noisy and smooth data to new file
        outstream4.WriteLine($"x y_clean y_noisy lambda={lambda} (clean function f(z)=sin(z*2*PI/5))");
        for(int i=0; i<y_noisy_S.size; i++){
            outstream4.WriteLine($"{x_S[i]} {y_clean_S[i]} {y_noisy_S[i]} {y_smooth_eff[i]}");
        }
       
        WriteLine(@"See 'Out.smoothsignalLU_generated_eff.svg':
        The more efficient LU factorization of the A matrix, which makes use of the special structure of the A, L and U matrices, is applied 
        for smoothing the generated data from the previous task.
        The efficient-LU-smoothing achieves the same result as LU and QR, as can be seen from the figure or by comparing the data files of 
        the smoothed signals.");
        WriteLine($"-> I sucessfully implemented a smoothing method that is more efficent than the QR-smoothing.\n");



        WriteLine("---Task C.3-------");
        WriteLine(@"The last step is to have a look at the operations count for smoothing with QR-decomposition ('smoothQR') in comparison to 
the efficient LU-factorization smoothing ('smoothLU_eff'), for several noisy data sets of different length N. Therefore, I generate a large noisy 
signal data set by adding random noise to clean sin-wave data. Then, the required time for smoothing of a subdataset consisting of the first N 
datapoints is measured. This is done for several values of N and for both methods smoothQR and smoothLU_eff.
");

        //generate noisy signal data with random noise for the timing
        Func<double, double> clean_func_time = delegate(double z){return Math.Sin(2*Math.PI*z/5);};
        var (x_time, y_clean_time, y_noisy_time) = smooth.generateCleanAndNoisySignal(maxmaxN_timing, clean_func_time, 0, maxmaxN_timing/300*5, 0.4);
        var outstream5=new System.IO.StreamWriter("Out.signalForTime.data", append:false);
        outstream5.WriteLine($"x_time y_clean_time y_noisy_time");
        for(int i=0; i<y_noisy_time.size; i++){
            outstream5.WriteLine($"{x_time[i]} {y_clean_time[i]} {y_noisy_time[i]}");
        }
         
        outstream.Close();
        outstream2.Close();
        outstream3.Close();
        outstream4.Close();
        outstream5.Close();
        WriteLine($@"See 'Out.smoothTiming.svg':
        The measured time for reading the noisy raw data from a file and smoothing the noisy data is shown in three graphs for the smoothQR-method 
        and smoothLU_eff-method. Both methods are applied to a small dataset. The efficient LU-smoothing is in addition also applied to a much 
        larger dataset of {maxmaxN_timing} data points.
        
        -> Conclusion: The smoothLU_eff-method is much faster than the QR method, more than 400 times faster for a dataset of 1500 points and more 
        than 1000 times faster for a dataset of 2000 points (under the hardware and software conditions of my measurement).
        - Be aware that the time includes aquiring the noisy data from the 'Out.signalForTime.data'-file. This causes a constant offset in the measured time. 
        For the large dataset this offset is of course larger than for the small dataset. (For comparability, for the timing with the small/large 
        dataset, the program reads the complete small/large data set, even if the time for a smaller N is measured).
        - smoothQR timing: The timing of the smoothing method which uses QR decomposition scales as N^3, as expected.
        - smoothLU_eff timing: The timing of the efficient smoothing method which uses LU and makes use of the special banded structure of the 
        involved matrices, is expected to scale as N. We would expect a slight increase in the operation time, as N is increased. The graph with 
        linear fit shows a slope very close to zero. In the range of, at least, up to {maxmaxN_timing} data points, the operation time of the 
        efficient LU-smoothing is nearly constant.

We see that making use of the matrix structure allows for a tremendous improvement in the operation time of the smoothing, especially for very large datasets!
        ");

        return 0;
    }
}