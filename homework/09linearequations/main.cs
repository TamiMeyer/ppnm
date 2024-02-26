using System;
using static System.Console;
using static System.Math;

public class main{
    public static void Main(){
        WriteLine("Task A:");
        test_decomp();
        //test_solve();

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
        unity.print();
    }




}
