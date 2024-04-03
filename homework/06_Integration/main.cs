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
        WriteLine($"∫₀¹ dx √(x) = {I : 0.0000}     Analytic: 2/3   Within the given accuracy goals?: {funs.approx(I, expected, 1e-3, 1e-3)}");

        f = delegate(double x){return 1.0/Sqrt(x);};
        I = integ.integral(f, 0, 1);
        expected = 2.0;
        WriteLine($"∫₀¹ dx 1/√(x) = {I : 0.0000}     Analytic: 2    Within the given accuracy goals?: {funs.approx(I, expected, 1e-3, 1e-3)}");

        f = delegate(double x){return 4*Sqrt(1-x*x);};
        I = integ.integral(f, 0, 1);
        expected = PI;
        WriteLine($"∫₀¹ dx 4√(1-x²) = {I : 0.0000}     Analytic: π     Within the given accuracy goals?: {funs.approx(I, expected, 1e-3, 1e-3)}");

        f = delegate(double x){return Log(x)/Sqrt(x);};
        I = integ.integral(f, 0, 1);
        expected = -4.0;
        WriteLine($"∫₀¹ dx ln(x)/√(x) = {I : 0.0000}     Analytic: -4    Within the given accuracy goals?: {funs.approx(I, expected, 1e-3, 1e-3)}");

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
        WriteLine();

        WriteLine("---Task B-------");
        WriteLine("Two integrals with integrable divergencies at the end-points of the intervals are calculated using adaptive integrator with and without the Clenshaw–Curtis variable transformation and using python's scipy.integrate.quad routine.");
        WriteLine("The number of integrand evaluations is compared. \n");
        var instreamPy = new System.IO.StreamReader("Out.pythonIntegrals.txt");

        string line;
        double I_py_a = 0;
        double I_py_b = 0;
        int ncalls_py_a = 0;
        int ncalls_py_b = 0;
        for(int lineCount = 0; lineCount<=8;lineCount++){
            line = instreamPy.ReadLine();
            if(lineCount == 1) I_py_a = double.Parse(line);
            else if (lineCount == 3) ncalls_py_a = int.Parse(line);
            else if(lineCount == 6) I_py_b = double.Parse(line);
            else if (lineCount == 8) ncalls_py_b = int.Parse(line);        
        }

        WriteLine("Calculate ∫₀¹ dx 1/√(x)");
        int ncalls = 0;
        f = delegate(double x){ncalls++; return 1.0/Sqrt(x);};
        double I_Clenshaw = integ.clenshawIntegral(f, 0, 1);
        WriteLine($"With Clenshaw–Curtis variable transformation: {I_Clenshaw } ,  number of calls: {ncalls}");
        ncalls = 0;
        I = integ.integral(f, 0, 1);
        WriteLine($"Without variable transformation: {I} ,  number of calls: {ncalls}");
        WriteLine($"Python/numpy's integration routines: {I_py_a} ,  number of calls: {ncalls_py_a}");
        WriteLine( "Analytic: 2");
        WriteLine();

        WriteLine("Calculate ∫₀¹ dx ln(x)/√(x)");
        ncalls = 0;
        f = delegate(double x){ncalls++; return Log(x)/Sqrt(x);};
        I_Clenshaw = integ.clenshawIntegral(f, 0, 1);
        WriteLine($"With Clenshaw–Curtis variable transformation: {I_Clenshaw} ,  number of calls: {ncalls}");
        ncalls = 0;
        I = integ.integral(f, 0, 1);
        WriteLine($"Without variable transformation: {I} ,  number of calls: {ncalls}");
        WriteLine($"Python/numpy's integration routines: {I_py_b} ,  number of calls: {ncalls_py_b}");
        WriteLine( "Analytic: -4");

        instreamPy.Close();

        return 0;
    }
}