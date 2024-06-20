using System;
public static class smooth{

/* Returns the smooth signal x, obtained as a solution to the leastsquares problem,
x : min_x (∥x − y∥^2 + λ∥Dx∥^2), by solving the linear equation (I + λD^TD)x = y,
using QR-GramSchmidt-decomposition*/ 
public static vector smoothQR(
    vector y, //signal vector
    double lambda //  smoothing parameter between 0 (-> no smoothing) and infinity (-> x converges to a linear fit to the data)
){
    matrix A = lineqMatrix(lambda, y.size);
    var (Q,R) = QRGS.decomp(A);
    vector x = QRGS.solve(Q,R,y);
    return x;
}

public static matrix secondDerivative(int m){ //m is the number of signal points
    matrix D = new matrix(m, m);
    for(int i=1; i<m-1; i++){
        D[i,i-1] = 1;
        D[i,i+1] = 1;
        D[i,i] = -2;   
    }
    D[0,0] = 1;
    D[0,2] = 1;
    D[0,1] = -2;   
    D[m-1,m-3] = 1;
    D[m-1,m-1] = 1;
    D[m-1,m-2] = -2; 
    return D;
}

public static matrix lineqMatrix(double lambda, int m){ //m is the number of signal points
    matrix D = secondDerivative(m);
    matrix A = matrix.id(m);
    A += lambda * D.transpose()*D;
    return A;
}

//generates a clean signal in the range [a,b] for a give function and adds random noise
public static (vector, vector, vector) generateCleanAndNoisySignal(int points, Func<double, double> function, double a, double b, double noiseLevel){
    vector x = new vector(points);
    vector clean = new vector(points);
    vector noisy = new vector(points);
    Random random = new Random();
    for(int i =0; i<points; i++){
        x[i] = a + 1.0*i/(points-1) * (b-a);
        clean[i] = function(x[i]);
        noisy[i] = clean[i] + noiseLevel * (random.NextDouble() - 0.5);
    }
    return (x, clean, noisy);
}

/* Returns the smooth signal x, obtained as a solution to the leastsquares problem,
x : min_x (∥x − y∥^2 + λ∥Dx∥^2), by solving the linear equation (I + λD^TD)x = y,
using LU decomposition*/ 
/*public static vector smoothLU(
    vector y, //signal vector
    double lambda //  smoothing parameter between 0 (-> no smoothing) and infinity (-> x converges to a linear fit to the data)
){
    matrix A = lineqMatrix(lambda, y.size);
    var (L,U) = LUdecomp(A);
    vector v = y.copy();
    fwdsub(L, v);//solve Lz=y
    backsub(U, v);//solve Ux=z
    return v;
}*/

public static (matrix, matrix) LUdecomp(matrix A){ //doolittle LU
    int n = A.size1;
    matrix L = new matrix(n);
    matrix U = new matrix(n);
    double sum = 0;
    for(int i = 0; i<n;i++){
        L[i,i] = 1;// initialize the diagonal of L to 1
        for(int j=i; j<n; j++){
            sum = 0;
            for(int k = 0; k<i; k++) sum+=L[i,k]*U[k,j];
            U[i,j] = A[i,j] - sum;
        }
        for(int j=i+1; j<n; j++){
            sum = 0;
            for(int k = 0; k<j; k++) sum+=L[j,k]*U[k,i];
            L[j,i]=1.0/U[i,i]*(A[j,i]-sum);
        }
    }
    return (L,U);
}

/*static void fwdsub(matrix L, vector b){

}


static void backsub(matrix U, vector c){
        for(int i = c.size-1; i >= 0; i--){
            double sum=0;
            for(int k = i+1; k<c.size; k++) sum+=U[i,k]* c[k];
            c[i] = (c[i]-sum)/U[i,i];
        }
}*/

}