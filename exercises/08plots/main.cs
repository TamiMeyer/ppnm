using static System.Console;

class main{
    public static void Main(){
        for(double x = -3; x<=3; x+=0.005){
            WriteLine($"{x} {sfuns.erf(x)}");
        } 

        var outstream=new System.IO.StreamWriter("gamma.data", append:false);
        for(double x = -5; x<=5; x+=0.005){  
            outstream.WriteLine($"{x} {sfuns.fgamma(x)}");
        }
        outstream.Close();
    }//Main
}//main