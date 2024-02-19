using static System.Console;
using static System.Math;
using System.Numerics;
using static System.Numerics.Complex;

class main{
    public static int Main(){
        for(double a = -5; a<=5; a+=1.0/69){ //when looping over double, be careful that they can be represented as binarys 
            for(double b = -5; b<=5; b+=1.0/69){
                WriteLine($"{a} {b} {sfuns.G(new Complex(a,b)).Real} {sfuns.G(new Complex(a,b)).Imaginary}");
            }
            WriteLine("");
        } 
        return 0;       
    }//Main
}//main