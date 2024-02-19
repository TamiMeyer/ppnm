using static System.Console;

class main{
    public static void Main(){
        for(double x = -3; x<=3; x+=0.005){
            WriteLine($"{x} {sfuns.erf(x)}");
        } 

        var outstream=new System.IO.StreamWriter("gamma.data", append:false);
        for(double x = -5; x<=5; x+=0.005){  
            outstream.WriteLine($"{x} {sfuns.gamma(x)}");
        }
        outstream.Close();

        var outstream2=new System.IO.StreamWriter("lngamma.data", append:false);
        for(double x = 0.005; x<=5; x+=0.005){  
            outstream2.WriteLine($"{x} {sfuns.lngamma(x)}");
        }
        outstream.Close();        
    }//Main
}//main