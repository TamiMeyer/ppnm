using System;
using static System.Console;
using static System.Math;
using System.Linq;

public class main{
    public static int Main(){

        WriteLine("---Task A-------");
        WriteLine(@"See 'Out.mc2D_pseudo.svg': 
        The estimated error and the actual error as functions of the number of sampling points are plot for the calculation of the area of a unit circle by pseudo-random monte carlo integration
        and for calculation of the area of an ellipse with semi-major and semi-minor axis of length 2 and 1.
        It is shown that the actual error scales as 1/√N");
        WriteLine();

        /* Calculate area of a circle*/
        var outstream = new System.IO.StreamWriter("Out.circle_pseudo.data", append:false);
        outstream.WriteLine("N pseudo_result pseudo_error_estimate pseudo_error_actual");
        Func<vector, double> circle = (vector x) => {if(x.norm()<=1) return 1; else return 0;};
        vector a = new vector(-1.0, -1.0); //lower integration boundary
        vector b = new vector(1.0, 1.0) ; //upper integration boundary

        int[] Ns = new int[100];

        for (int i = 50, j=0; i <= 50000; i += 500, j++) Ns[j] = i;

        double[] result = new double[Ns.Length];
        double[] sigma = new double[Ns.Length];
        double pseudo_res = 0, pseudo_sig = 0;
        for(int i=0; i<Ns.Length; i++){
            (pseudo_res, pseudo_sig) = montecarlo.pseudo_plainmc(circle, a, b, Ns[i]);
            result[i] = pseudo_res;
            sigma[i] = pseudo_sig;      
            outstream.WriteLine($"{Ns[i]} {result[i]} {sigma[i]} {Abs(PI-result[i])}");
        }

        (pseudo_res, pseudo_sig) = montecarlo.pseudo_plainmc(circle, a, b, 10000);
        WriteLine($"area of unit circle calculated with pseudo-random mc (10000 sampling points): result={pseudo_res}, estimated error={pseudo_sig}, analytic: π");
        WriteLine();

        /* Calculate area of an ellipse*/
        var outstream2 = new System.IO.StreamWriter("Out.ellipse_pseudo.data", append:false);
        outstream2.WriteLine("N pseudo_result pseudo_error_estimate pseudo_error_actual");
        Func<vector, double> ellipse = (vector x) => {if(x[0]*x[0]/(2*2)+x[1]*x[1]<=1) return 1; else return 0;}; //ellipse with diameter 4 and 2
        a = new vector(-2.0, -1.0); //lower integration boundary
        b = new vector(2.0, 1.0) ; //upper integration boundary

        Ns = new int[100];
        for (int i = 50, j=0; i <= 50000; i += 500, j++) Ns[j] = i;
        double[] result2 = new double[Ns.Length];
        double[] sigma2 = new double[Ns.Length];
        for(int i=0; i<Ns.Length; i++){
            (pseudo_res, pseudo_sig) = montecarlo.pseudo_plainmc(ellipse, a, b, Ns[i]);
            result2[i] = pseudo_res;
            sigma2[i] = pseudo_sig;      
            outstream2.WriteLine($"{Ns[i]} {result2[i]} {sigma2[i]} {Abs(2*PI-result2[i])}");
        }

        (pseudo_res, pseudo_sig) = montecarlo.pseudo_plainmc(ellipse, a, b, 10000);
        WriteLine($"area of ellipse with semi-major and semi-minor axis of length 2 and 1, calculated with pseudo-random mc (10000 sampling points): result={pseudo_res}, estimated error={pseudo_sig}, analytic: 2π");
        WriteLine();

        /*3d integral*/
        Func<vector, double> funcA = (vector x) => {return 1.0/(1.0-Cos(x[0])*Cos(x[1])*Cos(x[2]))/(Pow(PI, 3));}; 
        a = new vector(0.0, 0.0, 0.0); //lower integration boundary
        b = new vector(PI, PI, PI) ; //upper integration boundary

        (pseudo_res, pseudo_sig) = montecarlo.pseudo_plainmc(funcA, a, b, 500000);
        WriteLine($" ∫0π  dx/π ∫0π  dy/π ∫0π  dz/π [1-cos(x)cos(y)cos(z)]^-1 = Γ(1/4)4/(4π3) calculated with pseudo-random mc (50000 sampling points): result={pseudo_res}, estimated error={pseudo_sig}, analytic:{Pow(3.62561, 4)/(4*PI*PI*PI) : 0.000000} ");



        outstream.Close();
        outstream2.Close();
        return 0;
    }
}