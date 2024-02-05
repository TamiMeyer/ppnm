using System;
using static System.Console;
using static System.Math;
//using static sfuns;

static class main{
        static double x=1.0;
        static int i;
        static string hello = $"hello, x={x}\n";
        static double times2(double y){
                WriteLine(x);
                return y*2;
        }
        static int Main(){
		double sqrt2=Math.Sqrt(2.0);
		Write($"The sqrt of 2 is {sqrt2}\n");
		Write($"sqrt2^2 = {sqrt2*sqrt2} (should equal 2)\n");
		WriteLine($"x={x} Sin(x)={Sin(x)}");
                double prod = 1;
                for(int x=1; x<10; x+=1)
                {
                        Write($"fgamma({x})={sfuns.fgamma(x)} {x-1}!={prod}\n");
                        WriteLine($"exp(lngamma({x})={Exp(sfuns.lngamma(x))}");
                        prod *=x;
                }
                return 0;
        }
}

