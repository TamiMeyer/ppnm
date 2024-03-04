using System;
public static class fit{
    public static (vector,matrix) lsfit(Func<double,double>[] fs, vector x, vector y, vector dy){ //Fit given dataset with a linear combination c_k*f_k(x); returns the vector of the best fit coefficients c_k and th covariance matrix
        int n = x.size;
        int m = fs.Length;
        matrix A = new matrix(n,m);
        vector b = new vector(n);
        for(int i=0;i<n;i++){
            b[i]=y[i]/dy[i];
            for(int k=0;k<m;k++){
                A[i,k] = fs[k](x[i])/dy[i];
            }
        }
        vector c = QRGS.solve(A,b);
        //matrix A_inv = QRGS.inverse(A);
        matrix S = null;//A_inv * A_inv.transpose(); //covariance matrix S=A^-1*A^-T
        return (c,S);
    }
}