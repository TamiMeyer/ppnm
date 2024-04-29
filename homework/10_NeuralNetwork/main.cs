using System;
using static System.Console;
using static System.Math;
public class main{
    public static void Main(){
        WriteLine("---Task A-------");
        var outstream_t = new System.IO.StreamWriter("Out.train.data", append:false);

        int neurons = 7;
        ann annA = new ann(neurons);
        WriteLine($"Number of hidden neurons: {neurons}");
        WriteLine($"Starting network parameters are: {annA.parameters()}\n");
        
        /*produce training data*/
        vector x = new vector(40);
        vector y = new vector(40);
        for(int i = 0; i<40;i++){
            x[i] = -1 + 0.05*i;
            y[i] = Cos(5*x[i]-1)*Exp(-x[i]*x[i]);
            outstream_t.WriteLine($"{x[i]} {y[i]}");
        }

        /*train*/
        annA.train(x, y);
        WriteLine("The network is trained to approximate the function g(x)=Cos(5*x-1)*Exp(-x*x) with the data from 'Out.train.data'.");
        WriteLine($"The resulting network parameters are: {annA.parameters()}\n");

        /*produce response data*/
        var outstream = new System.IO.StreamWriter("Out.response.data", append:false);
        for(double d = -1.0; d<=1.0; d+=1.0/128 ){
            outstream.WriteLine($"{d} {annA.response(d)}");
        }

        //TODO change starting parameters and tidy up out.txt

        outstream_t.Close();
        outstream.Close();

        WriteLine(@"See 'Out.response_taskA.svg': 
        xxxx");
    }
}
