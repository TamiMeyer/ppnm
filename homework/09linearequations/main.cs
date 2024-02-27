using System;
using static System.Console;
using static System.Math;

public class main{
    public static void Main(){
        WriteLine("---Task A--------");
        WriteLine("A1: Test the function decomp:");
        test_decomp();
        WriteLine("A2: Test the function solve:");
        test_solve();
        WriteLine("---Task B--------");


    }

    public static void test_decomp(){
        WriteLine("Generate a random tall (n>m) matrix A (of a modest size):");
        matrix A = matrix.random_tall_matrix();
        A.print("A = ");
        WriteLine();

        WriteLine("Factorize A into QR:");
        var (Q, R) = QRGS.decomp(A);
        Q.print("Q = ");
        R.print("R = ");
        WriteLine();

        WriteLine($"Is R upper triangular ? => {R.isUpperTriangular()}");
        WriteLine();

        matrix QTQ = Q.transpose()*Q;
        matrix unity = new matrix(QTQ.size1);
        unity.set_unity();
        WriteLine($"Q^T*Q=1 ? => {matrix.approx(QTQ, unity)}");
        QTQ.print("Q^TQ = ");
        WriteLine();

        matrix QR = Q*R;
        WriteLine($"QR=A ? => {matrix.approx(Q*R, A)}");
        QR.print("QR = ");
        WriteLine("");
    }

    public static void test_solve(){
        WriteLine("Generate a random square matrix A (of a modest size):");
        matrix A = matrix.random_square_matrix();
        A.print("A = ");
        WriteLine();

        WriteLine("Generate a random vector b (of the same size):");
        vector b = vector.random_vector(A.size1);
        b.print("b = ");
        WriteLine();

        WriteLine("Factorize A into QR:");
        var (Q, R) = QRGS.decomp(A);
        Q.print("Q = ");
        R.print("R = ");
        WriteLine();

        WriteLine("Solve QRx=b:");
        vector x = QRGS.solve(Q, R, b);
        x.print("solution x = ");
        WriteLine();

        WriteLine($"Ax=b ? => {b.approx(A*x)}");
        vector Ax = A*x;
        Ax.print("Ax = ");
        WriteLine();
    }




}
