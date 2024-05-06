using System;
using static System.Console;
using static System.Math;
public class main{
    public static void Main(){
        WriteLine("---Task A-------");
        var outstream_t = new System.IO.StreamWriter("Out.train.data", append:false);

        int neurons = 6;
        ann annA = new ann(neurons);
        
        /*produce training data*/
        vector x = new vector(41);
        vector y = new vector(41);
        for(int i = 0; i<=40;i++){
            x[i] = -1 + 0.05*i;
            y[i] = Cos(5*x[i]-1)*Exp(-x[i]*x[i]);
            outstream_t.WriteLine($"{x[i]} {y[i]}");
        }

        /*train*/
        annA.train(x, y);
        WriteLine($"1.The network ({neurons} hidden neurons) is trained to approximate the function g(x)=Cos(5*x-1)*Exp(-x*x) with the data from 'Out.train.data'.");
        WriteLine($"For debugging: The resulting network parameters (shifts ai, scales bi, weight-factors wi) are: {annA.parameters()}\n");
        WriteLine($"2.Response data of the network in the interval [-1,1] is produced and can be found in 'Out.response.data'.");

        /*produce response data*/
        var outstream = new System.IO.StreamWriter("Out.response.data", append:false);
        for(double d = -1.0; d<=1.0; d+=1.0/128 ){
            outstream.WriteLine($"{d} {annA.response(d)}");
        }

        /*produce derivative data using the determined network parameter*/
        outstream = new System.IO.StreamWriter("Out.derivative.data", append:false);
        for(double d = -1.0; d<=1.0; d+=1.0/128 ){
            outstream.WriteLine($"{d} {annA.deriv_gausswv(d)}");
        }
        /*produce anti-derivative data using the determined network parameter*/
        

        outstream_t.Close();
        outstream.Close();

        WriteLine(@"See 'Out.response_taskA.svg': 
        The training data points are plot and the resulting response.");
    }
}
