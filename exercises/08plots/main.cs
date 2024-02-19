using static System.Console;

class main{
    public static int Main(){
        for(double x = -3; x<=3; x+=1.0/8){ //when looping over double, be careful that they can be represented as binarys 
            WriteLine($"{x} {sfuns.erf(x)}");
        } 

        var outstream=new System.IO.StreamWriter("gamma.data", append:false);
        for(double x = -5; x<=5; x+=1.0/64){  
            outstream.WriteLine($"{x} {sfuns.gamma(x)}");
        }
        outstream.Close();

        var outstream2=new System.IO.StreamWriter("lngamma.data", append:false);
        for(double x = 1.0/64; x<=5; x+=1.0/64){  
            outstream2.WriteLine($"{x} {sfuns.lngamma(x)}");
        }
        outstream.Close();

        return 0;       
    }//Main
}//main