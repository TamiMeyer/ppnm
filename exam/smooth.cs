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
}