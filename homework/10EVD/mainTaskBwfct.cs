using static System.Console;
public class wavefunctions{
    public static void Main(string[] args){
        double rmax = 10;
        double dr = 0.3;
        foreach(var arg in args){
	        var words = arg.Split(':');
            if(words[0]=="-rmax"){rmax= double.Parse(words[1]);}
	        if(words[0]=="-dr"){dr= double.Parse(words[1]);}
        }

        double[,] wfct0 = hydrogen.wavefunction(0, rmax, dr);
        double[,] wfct1 = hydrogen.wavefunction(1, rmax, dr);
        double[,] wfct2 = hydrogen.wavefunction(2, rmax, dr);
        double[,] wfct3 = hydrogen.wavefunction(3, rmax, dr);
        //WriteLine($"rmax={rmax} and dr={dr}");
        for(int j = 0; j<wfct0.GetLength(1); j++)
            WriteLine($"{wfct0[0,j]} {wfct0[1,j]} {wfct1[1,j]} {wfct2[1,j]} {wfct3[1,j]}");
        WriteLine();
    } 
}   