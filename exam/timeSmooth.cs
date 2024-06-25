using System; 
using static System.Console;
using System.Collections.Generic;

public class main{
    public static void Main(string[] args){
        String data = "Out.signalForTime.data";
        int N = 7;//number of data points that will be smoothed
        int maxN = int.MaxValue; //maximum number of datapoints that should be read from the noisy data file
        String method = "QR";
        double lambda = 8000;
        foreach(var arg in args){
	        var words = arg.Split(':');
    	    if(words[0]=="-size"){N= (int)float.Parse(words[1]);}
    	    if(words[0]=="-method"){method= words[1];}
    	    if(words[0]=="-maxN"){maxN= (int)float.Parse(words[1]);}
        }

        //Read the raw noisy signal data from the file
        /*We do not only read N data points but all the data points from the file in order to achieve a constant offset in time,
        otherwise there would be a contribution from the reading which depends on N */
        ///List<double> x_raw_list = new List<double>{};//(not required for timing, but for debugging)
        List<double> y_raw_list = new List<double>{};
        var instream=new System.IO.StreamReader(data);
        instream.ReadLine(); //skip the first line (it contains the title)
        char[] split_delimiters = {' ','\t','\n'};
        var split_options = System.StringSplitOptions.RemoveEmptyEntries;
        int linenr =0;
        for(string line=instream.ReadLine(); line!=null && linenr<maxN; line=instream.ReadLine()){  
	        var numbers = line.Split(split_delimiters,split_options);
            ///x_raw_list.Add(double.Parse(numbers[0]));//(not required for timing, but for debugging)
            y_raw_list.Add(double.Parse(numbers[2]));
            linenr++;
        }
        instream.Close();

        //discard the signal data and keep only the first N datapoints
        int signalCount = y_raw_list.Count;
        if(signalCount < N ) throw new ArgumentException("The noisy data set contains less points than N");
        if (signalCount > N) {
            ///x_raw_list.RemoveRange(N, signalCount - N);//(not required for timing, but for debugging)
            y_raw_list.RemoveRange(N, signalCount - N);
        }
        ///double[] x_raw = x_raw_list.ToArray();//(not required for timing, but for debugging)
        double[] y_raw = y_raw_list.ToArray();

        //smoothing with the chosen method (QR or efficientLU)
        vector y_raw_vec = new vector(y_raw);
        #pragma warning disable CS0219
        vector y_smooth;
        if(String.Equals(method, "QR")){
            y_smooth = smooth.smoothQR(y_raw_vec, lambda);
        } else if(String.Equals(method, "LUeff")){
            y_smooth = smooth.smoothLU_eff(y_raw_vec, lambda);

                    /*//For debugging: write clean,noisy and smooth data to new file
                    String outName = "Out.smoothsignal_" + method + "_" + N + ".data";
                    var outstream=new System.IO.StreamWriter(outName, append:false);
                    outstream.WriteLine($"x y_noisy lambda={lambda}");
                    for(int i=0; i<y_raw_vec.size; i++){
                    outstream.WriteLine($"{x_raw[i]} {y_raw[i]} {y_smooth[i]}");
                    }
                    outstream.Close();*/

        }
        #pragma warning restore CS0219
    }
}