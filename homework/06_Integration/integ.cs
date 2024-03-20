using System;
using static System.Math;
public static class integ{
public static double integral(Func<double,double> f, double a, double b, double δ=0.001, double ε=0.001, double f2=double.NaN, double f3=double.NaN){
    double h=b-a;
    if(double.IsNaN(f2)){ f2=f(a+2*h/6); f3=f(a+4*h/6); } // first call, no points to reuse
    double f1=f(a+h/6), f4=f(a+5*h/6);
    double Q = (2*f1+f2+f3+2*f4)/6*(b-a); // higher order rule
    double q = (  f1+f2+f3+  f4)/4*(b-a); // lower order rule
    double err = Abs(Q-q);
    if (err <= δ+ε*Abs(Q)) return Q;
    else return integral(f,a,(a+b)/2,δ/Sqrt(2),ε,f1,f2) + integral(f,(a+b)/2,b,δ/Sqrt(2),ε,f3,f4);
}

public static double erf(double z){
    if(z<0) return -erf(-z);
    else if(z<=1 && z>=0){
        Func<double,double> f = delegate(double x){return Exp(-x*x);};
        return 2/Sqrt(PI)*integral(f, 0, z);
    } else {
        Func<double,double> g = delegate(double x){return Exp(-Pow(z+(1-x)/x,2)/x/x);};
        return 1 - 2/PI*integral(g, 0, 1)
    }
}

}