using System;
using static System.Console;
using static System.Math;
public class main{
    public static int Main(){
        WriteLine("---Task A-------");
        WriteLine(@"The implementation of the integrate function is tested
and checked that my integrator returns results within the given accuracy goals:");
        WriteLine();

        Func<double,double> f = delegate(double x){return Sqrt(x);};
        double I = integ.integral(f, 0, 1);
        double expected = 2.0/3;
        WriteLine($"∫₀¹ dx √(x) = {I : 0.0000}     Expected: 2/3   Within the given accuracy goals?: {funs.approx(I, expected, 1e-3, 1e-3)}");

        f = delegate(double x){return 1.0/Sqrt(x);};
        I = integ.integral(f, 0, 1);
        expected = 2.0;
        WriteLine($"∫₀¹ dx 1/√(x) = {I : 0.0000}     Expected: 2    Within the given accuracy goals?: {funs.approx(I, expected, 1e-3, 1e-3)}");

        f = delegate(double x){return 4*Sqrt(1-x*x);};
        I = integ.integral(f, 0, 1);
        expected = PI;
        WriteLine($"∫₀¹ dx 4√(1-x²) = {I : 0.0000}     Expected: π     Within the given accuracy goals?: {funs.approx(I, expected, 1e-3, 1e-3)}");

        f = delegate(double x){return Log(x)/Sqrt(x);};
        I = integ.integral(f, 0, 1);
        expected = -4.0;
        WriteLine($"∫₀¹ dx ln(x)/√(x) = {I : 0.0000}     Expected: -4    Within the given accuracy goals?: {funs.approx(I, expected, 1e-3, 1e-3)}");

        var outstream = new System.IO.StreamWriter("Out.integ_erf.data", append:false);
        for(double i = -3.0; i < 3.0; i+=1.0/32){
            outstream.WriteLine($"{i} {integ.erf(i)}");
        }
        outstream.Close();

        outstream = new System.IO.StreamWriter("Out.singleprec_erf.data", append:false);
        for(double i = -3.0; i < 3.0; i+=1.0/32){
            outstream.WriteLine($"{i} {funs.erf(i)}");
        }
        outstream.Close();
        WriteLine();
        WriteLine(@"See 'Out.integ_erf.svg':
        Compares the integrator implemented error function, the single precision error function from the plots exercise and some tabulated values for the error function.");


        return 0;
    }
}