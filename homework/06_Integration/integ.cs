using System;
using static System.Math;
public static class integ{
public static (double,double) finiteIntegral(Func<double,double> f, double a, double b, double δ=0.001, double ε=0.001, double f2=double.NaN, double f3=double.NaN){ //a and b cannot be infinite
    double h=b-a;
    if(double.IsNaN(f2)){ f2=f(a+2*h/6); f3=f(a+4*h/6); } // first call, no points to reuse
    double f1=f(a+h/6), f4=f(a+5*h/6);
    double Q = (2*f1+f2+f3+2*f4)/6*(b-a); // higher order rule
    double q = (  f1+f2+f3+  f4)/4*(b-a); // lower order rule
    double err = Abs(Q-q);
    if (err <= δ+ε*Abs(Q)) return (Q, err);
    else{
        var (Q1, err1) = integral(f,a,(a+b)/2,δ/Sqrt(2),ε,f1,f2);
        var (Q2, err2) = integral(f,(a+b)/2,b,δ/Sqrt(2),ε,f3,f4);
        return (Q1+Q2, Sqrt(err1*err1 + err2 * err2));
    }
}

public static double erf(double z){
    if(z<0) return -erf(-z);
    if(z<=1 && z>=0){
        Func<double,double> f = delegate(double x){return Exp(-x*x);};
        var (I1, err1) = integral(f, 0, z);
        return 2.0/Sqrt(PI)*I1;
    }
    Func<double,double> g = delegate(double x){return Exp(-Pow(z+(1-x)/x,2))/x/x;};
    var (I2, err2) = integral(g, 0, 1);
    return 1.0 - 2.0/Sqrt(PI)*I2;
}

public static (double, double) clenshawIntegral(Func<double,double> f, double a, double b, double δ=0.001, double ε=0.001, double f2=double.NaN, double f3=double.NaN){
    Func<double, double> g = (theta) =>  f((a+b)/2 + (b-a)/2*Cos(theta)) * Sin(theta) *(b-a)/2;
    return integral(g, 0, PI, δ, ε, f2, f3);
}

public static (double, double) integral(Func<double,double> f, double a, double b, double δ=0.001, double ε=0.001, double f2=double.NaN, double f3=double.NaN){ // accepts finite and infinite boundaries
    if(double.IsNegativeInfinity(a) && double.IsPositiveInfinity(b)){
        Func<double, double> g = delegate(double t){return f(t/(1-t*t)) * (1+t*t) / Pow(1-t*t, 2);};
        return finiteIntegral(g, -1, 1, δ, ε, f2, f3);
    }
    if(double.IsPositiveInfinity(b)){
        Func<double, double> g = delegate(double t){return f(a+t/(1-t)) / Pow(1-t, 2);};
        return finiteIntegral(g, 0, 1, δ, ε, f2, f3);
    }
    if(double.IsNegativeInfinity(a)){
        Func<double, double> g = delegate(double t){return f(b-(1-t)/t) / (t*t);};
        return integral(g, 0, 1, δ, ε, f2, f3);
    }
    return finiteIntegral(f, a, b, δ, ε, f2, f3);
}
}