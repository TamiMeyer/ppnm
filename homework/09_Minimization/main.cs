using System;
using static System.Console;
using static System.Math;

public class main{
    public static void Main(){
        WriteLine("---Task A-------");
        vector x_min; int step; bool exceeded_step_max;

        WriteLine("- Rosenbrock: Find a minimum of the Rosenbrock's valley function f(x,y) = (1-x)^2+100(y-x^2)^2 using Newton's method with forward gradient");
        Func<vector,double> f_rosenbrock = delegate(vector x){return Pow((1-x[0]), 2) + 100 * Pow((x[1] - x[0]*x[0]), 2);};
        
        vector[] x_start = {new vector(1,2), new vector(2, -2), new vector(3,-3), new vector(-2,2)};
        for(int i = 0; i<x_start.Length; i++){
            (x_min, step, exceeded_step_max) = minimization.newton(f_rosenbrock, x_start[i]);
            double f_min = f_rosenbrock(x_min);
            WriteLine($"Startpoint: {x_start[i].getString()} Minimum: xmin = {x_min.getString("{0,4:g7}")} ; f(xmin) = {f_min}; Steps: {step}; Exceeded max number of steps?: {exceeded_step_max}");
        }
        WriteLine("Exact minimum: xmin = (1, 1) ;  f(xmin) = 0  \n");

        WriteLine("- Himmelblau: Find a minimum of the Himmelblau's function f(x,y)=(x^2+y-11)^2+(x+y^2-7)^2 using Newton's method with forward gradient");
        Func<vector,double> f_himmel = delegate(vector x){return Pow(x[0]*x[0] + x[1] - 11, 2) + Pow(x[0] + x[1]*x[1]-7 ,2);};
        vector[] x_start2 = {new vector(5,5), new vector(-5, 5), new vector(-5,-5), new vector(5,-5)};
        for(int i = 0; i<x_start2.Length; i++){
            (x_min, step, exceeded_step_max) = minimization.newton(f_himmel, x_start2[i]);
            double f_min = f_himmel(x_min);
            WriteLine($"Startpoint: {x_start2[i].getString()} Minimum: xmin = {x_min.getString("{0,4:g7}")} ; f(xmin) = {f_min}; Steps: {step}; Exceeded max number of steps?: {exceeded_step_max}");
        }
        WriteLine("Exact minima: xmin = (3,2) ;  f(xmin) = 0"); 
        WriteLine("              xmin = (-2.805118,3.131312) ;  f(xmin) = 0");    
        WriteLine("              xmin = (-3.779310,-3.283186) ;  f(xmin) = 0");    
        WriteLine("              xmin = (3.584428, -1.848126) ;  f(xmin) = 0 \n");    

        WriteLine("---Task B-------");
        WriteLine(@"The task is to fit the Breit-Wigner function F(E|m,Γ,A) = A/[(E-m)²+Γ²/4] (where A is the scale-factor, m is the mass and Γ is the widths of the resonance) to the given experimental data in the file 'experiment_Higgs.data'.
        
First, the fitting parameters m,Γ,A are determined by minimizing the deviation function using Newton's method with forward gradient:
D(m,Γ,A)=Σi[(F(Ei|m,Γ,A)-σi)/Δσi]^2 .");
        //read the higgs data table into generic lists
        var energy = new genlist<double>(); //Ei
        var signal = new genlist<double>(); //σi
        var error  = new genlist<double>(); //Δσi
        var separators = new char[] {' ','\t'};
        var options = StringSplitOptions.RemoveEmptyEntries;
        do{
            string line=Console.In.ReadLine();
            if(line==null)break;
            string[] words=line.Split(separators,options);
            energy.add(double.Parse(words[0]));
            signal.add(double.Parse(words[1]));
            error.add(double.Parse(words[2]));
        }while(true);

        Func<vector, double> f_BW = delegate(vector x){return x[3]/(Pow(x[0]-x[1],2)+x[2]*x[2]/4);}; //Breit-Wigner function (where A is the scale-factor, m is the mass and Γ is the widths of the resonance)
        Func<vector,double> f_fit_deviation = delegate(vector x){ //x[0]=m, x[1]=Gamma, x[2]=A
            double sum = 0.0;
            for(int i = 0;i<energy.size;i++){
                sum += Pow((f_BW(new vector(energy[i],x[0], x[1], x[2])) - signal[i])/error[i], 2);
            }
            return sum ;};
        //starting points for debugging
        ///vector[] x_start3 = {new vector(125.3, 0.05, 5), new vector(125.3, 0.004, 15), new vector(125.3, 0.004, 10), new vector(125.3, 0.004, 5), new vector(125.3, 4, 5), new vector(125.3, 4, 10), new vector(125.3, 0.004, 5), new vector(125.3, 0.003, 5), new vector(125.3, 0.05, 5)};
        vector[] x_start3 = {new vector(125.3, 0.004, 10)};
        for(int i = 0; i<x_start3.Length; i++){
            (x_min, step, exceeded_step_max) = minimization.newton(f_fit_deviation, x_start3[i], epshess:Pow(2,-26));
            double f_min = f_fit_deviation(x_min);
            WriteLine($"Startpoint: m={x_start3[i][0]},Γ={x_start3[i][1]},A={x_start3[i][2]} Minimum: m_min={x_min[0]},Γ_min={x_min[1]},A_min={x_min[2]} ; f(xmin) = {f_min}; Steps: {step}; Exceeded max number of steps?: {exceeded_step_max}");
        }

        ///WriteLine("\nThe fitting parameters were determined to m={}, Γ={}, A={}.\n");
        
        WriteLine("Second, see 'Out.higgs_fit.svg': The Breit-Wigner function with the previously determined fitting parameters and the experimental data are plot.");
        



        /*WriteLine(@"See 'xxxx.svg': 
        xxxx");*/

        
    }
}