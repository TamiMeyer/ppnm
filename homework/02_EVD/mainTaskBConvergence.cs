using static System.Console;
public class convergence{
        public static void Main(string[] args){
            double rmax = 10;
            double dr = 0.3;
            foreach(var arg in args){
	            var words = arg.Split(':');
    	        if(words[0]=="-rmax"){rmax= double.Parse(words[1]);}
    	        if(words[0]=="-dr"){dr= double.Parse(words[1]);}
            }

        matrix H = hydrogen.hamiltonian(rmax, dr);
        var (E0, v0) = hydrogen.lowest_eigenvalue_eigenvector(H);
        WriteLine($"{rmax} {dr} {E0}");

    }
    
}