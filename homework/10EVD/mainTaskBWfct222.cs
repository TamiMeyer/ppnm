using static System.Console;
public class wavefunctions{
    public static void Main(string[] args){
        double[] rmaxValues = new double[3];
        double[] drValues = new double[3];
        foreach(var arg in args){
         var words = arg.Split(':');
	        if(words[0]=="-rmaxValues"){
                for(int i=0; i<words.Length-1; i++){
                    rmaxValues[i]= double.Parse(words[i]);
                }
            }
	        if(words[0]=="-drValues"){
                for(int i=0; i<words.Length-1; i++){
                    drValues[i]= double.Parse(words[i]);
                }   
            }
        }
        double[,] wfct;
        foreach(double rmax in rmaxValues){
            foreach(double dr in drValues){
                wfct = hydrogen.wavefunction(0, rmax, dr);
                WriteLine($"rmax={rmax} and dr={dr}");
                for(int j = 0; j<wfct.GetLength(1); j++)
                    WriteLine($"{wfct[0,j]} {wfct[1,j]}");
                WriteLine();
            }
        }  
    }
}   