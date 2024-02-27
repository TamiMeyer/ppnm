using System; 
using static System.Console;

public class main{
    public static void Main(string[] args){
        int N = 0;
        foreach(var arg in args){
	        var words = arg.Split(':');
    	    if(words[0]=="-size"){N= (int)float.Parse(words[1]);}
        }
        matrix A = matrix.random_matrix(N, N);
        QRGS.decomp(A);

    }
}