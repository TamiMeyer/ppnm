using System;
public static class smooth{

/* Returns the smooth signal x, obtained as a solution to the leastsquares problem,
x : min_x (∥x − y∥^2 + λ∥Dx∥^2), by solving the linear equation (I + λD^TD)x = y.*/ 
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
    matrix D = new matrix(m-2, m);
    for(int i=0; i<m-2; i++){
        D[i,i] = 1;
        D[i,i+2] = 1;
        D[i,i+1] = -2;   
    }
    return D;
}



public static matrix lineqMatrix(double lambda, int m){ //m is the number of signal points
    matrix D = secondDerivative(m);
    matrix A = matrix.id(m);
    A += lambda * D.transpose()*D;
    return A;
}
}