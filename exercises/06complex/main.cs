using System;
using static System.Console;
using static System.Math;
using System.Numerics;
using static System.Numerics.Complex;
class main{

static bool approxReal(double a,double b,double acc=1e-9,double eps=1e-9){
	if(Abs(a-b)<acc)return true;
	if(Abs(a-b)<(Abs(a)+Abs(b))*eps)return true;
	return false;
}

static bool approxC(Complex c1, Complex c2){
    if(!approxReal(c1.Real,c2.Real))return false;
    if(!approxReal(c1.Imaginary, c2.Imaginary))return false;
    return true;
}

public static int Main(){
    Complex c1 = Complex.Sqrt(new Complex(-1,0));
    WriteLine($"sqrt(-1)={c1},                                == (0,1)? --> {approxC(c1, new Complex(0,1))}");
    c1 = Complex.Sqrt(new Complex(0,1));
    WriteLine($"sqrt(i)={c1}, ==(1/Sqrt(2),1/Sqrt(2))? --> {approxC(c1, new Complex(1/Sqrt(2),1/Sqrt(2)))}");
    c1 = Complex.Log(new Complex(0,1));
    WriteLine($"ln(i)={c1},                     ==(0,PI/2)? --> {approxC(c1, new Complex(0,PI/2))}");
    c1 = Complex.Pow(new Complex(0,1), new Complex(0, 1));
    WriteLine($"i**i={c1},                    ==(Exp(-PI/2),0)? --> {approxC(c1, new Complex(Exp(-PI/2),0))}");
    c1 = Complex.Sin(new Complex(0,PI));
    WriteLine($"sin(i*Pi)={c1},                ==(0, Sinh(PI))? --> {approxC(c1, new Complex(0, Sinh(PI)))}");

    return 0;
}

}