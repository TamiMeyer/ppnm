using System;
using static System.Console;

public class main{
    public static void Main(string[] args){
        WriteLine("---Task A--------");
        WriteLine("Generate a random symmetric matrix A:");
        matrix A = matrix.random_symmetric_matrix();
        A.print("A = ");
        WriteLine();

        WriteLine("Perform the eigenvalue-decomposition (A=VDV^T):");
        var (w,V) = jacobi.cyclic(A);
        w.print("eigenvalues w = ");
        V.print("orthogonal matrix of eigenvectors V = ");
        WriteLine();

        matrix D = matrix.new_matrix_diagonal(w);
        matrix VTAT = V.transpose()*A*V;
        WriteLine($"V^TAV=D ? => {D.approx(VTAT)} (where D is the diagonal matrix with the corresponding eigenvalues)");
        //VTAT.print("V^TAV = ");
        WriteLine($"VDV^T=A ? => {A.approx(V*D*V.transpose())}");
        matrix I = matrix.id(V.size1);       
        WriteLine($"V^TV=1 ? => {I.approx(V.transpose()*V)}");
        WriteLine($"VV^T=1 ? => {I.approx(V*V.transpose())}");
        WriteLine("");

        WriteLine("---Task B--------");
        double rmax = 10;
        double dr = 0.3;
        foreach(var arg in args){
	        var words = arg.Split(':');
    	    if(words[0]=="-rmax"){rmax= double.Parse(words[1]);}
    	    if(words[0]=="-dr"){dr= double.Parse(words[1]);}
        }
        WriteLine($"Hamiltonian matrix for rmax = {rmax} and dr = {dr}:");
        matrix H = hydrogen.hamiltonian(rmax, dr);
        H.print("H = ");
        WriteLine();
        WriteLine("Diagonalize the Hamiltonian matrix using my Jacobi routine and obtain the eigenvalues and eigenvectors:");
        (w,V) = hydrogen.diagonalize_hamiltonian(H);
        w.print("eigenvalues w = ");
        V.print("orthogonal matrix of eigenvectors V = ");
        WriteLine();

        WriteLine("See E0_dr.svg: Fix rmax to a reasonable value and calculate E0 for several different values of dr ; plot the resulting curve.");
        WriteLine("See E0_rmax.svg: Fix dr to a reasonable value and calculate E0 for several different values of rmax ; plot the resulting curve.");


    }
}
