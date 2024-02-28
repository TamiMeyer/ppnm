using System;
using static System.Console;

public class main{
    public static void Main(){
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


    }
}
