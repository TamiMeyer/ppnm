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
        var (I, err) = integ.integral(f, 0, 1);
        double expected = 2.0/3;
        WriteLine($"∫₀¹ dx √(x) = {I : 0.0000}     Analytic: 2/3   Within the given accuracy goals?: {funs.approx(I, expected, 1e-3, 1e-3)}");

        f = delegate(double x){return 1.0/Sqrt(x);};
        (I, err) = integ.integral(f, 0, 1);
        expected = 2.0;
        WriteLine($"∫₀¹ dx 1/√(x) = {I : 0.0000}     Analytic: 2    Within the given accuracy goals?: {funs.approx(I, expected, 1e-3, 1e-3)}");

        f = delegate(double x){return 4*Sqrt(1-x*x);};
        (I, err) = integ.integral(f, 0, 1);
        expected = PI;
        WriteLine($"∫₀¹ dx 4√(1-x²) = {I : 0.0000}     Analytic: π     Within the given accuracy goals?: {funs.approx(I, expected, 1e-3, 1e-3)}");

        f = delegate(double x){return Log(x)/Sqrt(x);};
        (I, err) = integ.integral(f, 0, 1);
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
        double[] I_taskC = new double[5];
        int[] ncalls_taskC = new int[5];
        for(int lineCount = 0; lineCount<=33;lineCount++){
            line = instreamPy.ReadLine();
            if(lineCount == 1) I_py_a = double.Parse(line);
            else if (lineCount == 3) ncalls_py_a = int.Parse(line);
            else if(lineCount == 6) I_py_b = double.Parse(line);
            else if (lineCount == 8) ncalls_py_b = int.Parse(line);     
            else if(lineCount == 11) I_taskC[0] = double.Parse(line);
            else if (lineCount == 13) ncalls_taskC[0] = int.Parse(line);
            else if(lineCount == 16) I_taskC[1] = double.Parse(line);
            else if (lineCount == 18) ncalls_taskC[1] = int.Parse(line);
            else if(lineCount == 21) I_taskC[2] = double.Parse(line);
            else if (lineCount == 23) ncalls_taskC[2] = int.Parse(line);     
            else if(lineCount == 26) I_taskC[3] = double.Parse(line);
            else if (lineCount == 28) ncalls_taskC[3] = int.Parse(line);
            else if(lineCount == 31) I_taskC[4] = double.Parse(line);
            else if (lineCount == 33) ncalls_taskC[4] = int.Parse(line);
        }

        WriteLine("Calculate ∫₀¹ dx 1/√(x)");
        int ncalls = 0;
        f = delegate(double x){ncalls++; return 1.0/Sqrt(x);};
        var (I_Clenshaw, err_Clenshaw) = integ.clenshawIntegral(f, 0, 1);
        WriteLine($"With Clenshaw–Curtis variable transformation: {I_Clenshaw } ,  number of calls: {ncalls} , err: {err_Clenshaw}");
        ncalls = 0;
        (I, err) = integ.integral(f, 0, 1);
        WriteLine($"Without variable transformation: {I} ,  number of calls: {ncalls} , err: {err}");
        WriteLine($"Python/numpy's integration routines: {I_py_a} ,  number of calls: {ncalls_py_a}");
        WriteLine( "Analytic: 2");
        WriteLine();

        WriteLine("Calculate ∫₀¹ dx ln(x)/√(x)");
        ncalls = 0;
        f = delegate(double x){ncalls++; return Log(x)/Sqrt(x);};
        (I_Clenshaw, err_Clenshaw) = integ.clenshawIntegral(f, 0, 1);
        WriteLine($"With Clenshaw–Curtis variable transformation: {I_Clenshaw} ,  number of calls: {ncalls} , err: {err_Clenshaw}");
        ncalls = 0;
        (I, err) = integ.integral(f, 0, 1);
        WriteLine($"Without variable transformation: {I} ,  number of calls: {ncalls} , err: {err}");
        WriteLine($"Python/numpy's integration routines: {I_py_b} ,  number of calls: {ncalls_py_b}");
        WriteLine( "Analytic: -4");
        WriteLine();

        instreamPy.Close();

        WriteLine("---Task C-------");
        WriteLine(@"My implementation of the integrator is tested on some (converging) infitine limit integrals and the number of integrand evaluations (calls) is noted
and compared to python's scipy.integrate.quad routine.");
        WriteLine();

        ncalls = 0;
        f = delegate(double x){ncalls++; return 1.0/(x*x);};
        (I, err) = integ.integral(f, 1, double.PositiveInfinity);
        WriteLine($"∫1,∞ dx 1/(x^2) = {I}     Analytic:  1 , err: {err}, approx?: {funs.approx(I, 1, 1e-3, 1e-3)}, number of calls: {ncalls} , python number of calls: {ncalls_taskC[0]}");
        
        ncalls = 0;
        f = delegate(double x){ncalls++; return 1.0/(x*x*x);};
        (I, err) = integ.integral(f, 1, double.PositiveInfinity);
        WriteLine($"∫1,∞ dx 1/(x^3) = {I}     Analytic:  0.5 , err: {err}, approx?: {funs.approx(I, 0.5, 1e-3, 1e-3)} , number of calls: {ncalls} , python number of calls: {ncalls_taskC[1]}");
          
        ncalls = 0;
        f = delegate(double x){ncalls++; return 1.0/((1+x)*Sqrt(x));};
        (I, err) = integ.integral(f, 0, double.PositiveInfinity);
        WriteLine($"∫₀∞ dx 1/((1+x)√(x)) = {I}     Analytic:  π , err: {err}, approx?: {funs.approx(I, PI, 1e-3, 1e-3)} , number of calls: {ncalls} , python number of calls: {ncalls_taskC[2]}");
        
        ncalls = 0;
        f = delegate(double x){ncalls++; return Exp(-x*x);};
        (I, err) = integ.integral(f, double.NegativeInfinity, double.PositiveInfinity);
        WriteLine($"∫-∞,∞ dx exp(-x^2) = {I}     Analytic:  Sqrt(π) ,  err: {err}, approx?: {funs.approx(I, Sqrt(PI), 1e-3, 1e-3)} , number of calls: {ncalls} , python number of calls: {ncalls_taskC[3]}");
        
        ncalls = 0;
        f = delegate(double x){ncalls++; return Exp(x);};
        (I, err) = integ.integral(f, double.NegativeInfinity, 0);
        WriteLine($"∫-∞,0 dx exp(x) = {I}     Analytic:  1 ,  err: {err}, approx?: {funs.approx(I, 1, 1e-3, 1e-3)} , number of calls: {ncalls} , python number of calls: {ncalls_taskC[4]}");
        
        return 0;
    }
}