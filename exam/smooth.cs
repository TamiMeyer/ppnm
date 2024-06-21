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
public static vector smoothLU(
    vector y, //signal vector
    double lambda //  smoothing parameter between 0 (-> no smoothing) and infinity (-> x converges to a linear fit to the data)
){
    matrix A = lineqMatrix(lambda, y.size);
    var (L,U) = LUdecomp(A);
    vector v = y.copy();
    fwdsub(L, v);//solve Lz=y
    backsub(U, v);//solve Ux=z
    return v;
}

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

static void fwdsub(matrix L, vector b){
    for(int i = 0; i <b.size; i++){
        double sum=0;
        for(int k = 0; k<i; k++) sum+=L[i,k]* b[k];
        b[i] = (b[i]-sum)/L[i,i];
    }
}

static void backsub(matrix U, vector c){
    for(int i = c.size-1; i >= 0; i--){
        double sum=0;
        for(int k = i+1; k<c.size; k++) sum+=U[i,k]* c[k];
        c[i] = (c[i]-sum)/U[i,i];
    }
}

/////more efficient methods

/*the matrix A=I + λD^TD is symmetric pentadiagonal (D is defined as described in the method 'secondDerivative').
This method returns the values of the main diagonal (a), the values of the super- and subdiagonal (b), the values of the supersuper- and subsubdiagonal (c).
The nxn A matrix has the following structure:
main diagonal a: a_1=a_n, a_2=a_{n-1}, a_3=a_{n-2}, a_i=const for i=[4,n-3]
superdiagonal b: b_1=b_{n-1}, b_2=b_{n-2}, b_i=const for i=[3,n-3]
supersuperdiagonal c: c_1=c_{n-2}, c_i=const for i=[2,n-3]
subdiagonal d: d_{i+1}=b_i
subsubdiagonal e: e_{i+2}=c_i
*/
public static (vector, vector, vector) lineqMatrix_eff(double lambda){ //m is the number of signal points
   vector a = new vector(4);//main diagonal elements a_1,a_2,a_3 and a_4
   vector b = new vector(3);//superdiagonal elements b_1,b_2,b_3
   vector c = new vector(2);//supersuperdiagonal elements c_1,c_2

    a[0]= 1 + lambda* (1+1);//a_1
    a[1]= 1 + lambda* (4+4+1);
    a[2]= 1 + lambda*(1+1+4+1);
    a[3]= 1 + lambda* (1+4+1);
    b[0]= lambda* (-2-2);
    b[1]= lambda* (-2-2-2);
    b[2]= lambda* (-2-2);
    c[0]= lambda* (1+1);
    c[1]= lambda;

    return (a,b,c);
}

/* more efficient doolittlde LU decomposition which makes use of the special structure of the A matrix.
The special structure of the A matrix causes the L matrix to have only 3 non-zero diagonals (main, sub, subsub)
and the U matrix to have only 3 non-zero diagonals (main, super, supersuper).

Input parameters:
    int n is the number of datapoints which A=LU will be applied to
    vectors a,b and c are the 9 values which fully determine the A matrix
*/
public static (vector, vector, vector, vector, vector, vector) LUdecomp_eff(int n, vector a, vector b, vector c){
    //structure of matrix L
    vector l = new vector(1);//main diagonal
    l[0]=1.0;//main diagonal is initialized to 1 (doolittle)
    vector ll = new vector(n-1); //subdiagonal
    vector lll = new vector(n-2);//subsubdiagonal

    //structure of matrix U
    vector u = new vector(n);//main diagonal
    vector uu = new vector(n-1); //superdiagonal
    vector uuu = new vector(2);//supersuperdiagonal (it turns out, that uuu is equal to the supersuperdiagonal c of A)

    //first row of U
    u[0]=a[0];
    uu[0]=b[0];
    uuu[0]=c[0];
    //first coloumn of L
    ll[0]=b[0]/u[0];
    lll[0]=c[0]/u[0];

    //second row of U
    u[1]=a[1]-ll[0]*uu[0];
    uu[1]=b[1]-ll[0]*uuu[0];
    uuu[1]=c[1];
    //second coloumn of L
    ll[1]=(b[1]-lll[0]*uu[0])/u[1];
    lll[1]=c[1]/u[1];

    //third row of U
    u[2]=a[2]-ll[1]*uu[1]-lll[0]*c[0];
    uu[2]=b[2]-ll[1]*uuu[1];
    //third coloumn of L
    ll[2]=(b[2]-lll[1]*uu[1])/u[2];
    lll[2]=c[1]/u[2];

    //fourth and higher
    for(int i=3; i<n-3;i++){
        u[i]=a[3]-ll[i-1]*uu[i-1]-lll[i-2]*c[1];
        uu[i]=b[2]-ll[i-1]*uuu[1];
        ll[i]=(b[2]-lll[i-1]*uu[i-1])/u[i];
        lll[i]=c[1]/u[i];
    }

    //last three rows and coloumns of U and L
    u[n-3]=a[2]-ll[n-3-1]*uu[n-3-1]-lll[n-3-2]*c[1];
    uu[n-3]=b[1]-ll[n-3-1]*uuu[1];
    ll[n-3]=(b[1]-lll[n-3-1]*uu[n-3-1])/u[n-3];
    lll[n-3]=c[0]/u[n-3];
    //
    u[n-2]=a[1]-ll[n-2-1]*uu[n-2-1]-lll[n-2-2]*c[1];
    uu[n-2]=b[0]-ll[n-2-1]*uuu[0];
    ll[n-2]=(b[0]-lll[n-2-1]*uu[n-2-1])/u[n-2];
    //
    u[n-1]=a[0]-ll[n-1-1]*uu[n-1-1]-lll[n-1-2]*c[0];

    return (l,ll,lll,u,uu,uuu);
}

/* Returns the smooth signal x, obtained as a solution to the leastsquares problem,
x : min_x (∥x − y∥^2 + λ∥Dx∥^2), by solving the linear equation Ax=(I + λD^TD)x = y,
using LU decomposition and making use of the special structure of A*/ 
public static vector smoothLU_eff(
    vector y, //signal vector
    double lambda //  smoothing parameter between 0 (-> no smoothing) and infinity (-> x converges to a linear fit to the data)
){
    int n = y.size;
    var (a,b,c) = lineqMatrix_eff(lambda);
    var (l,ll,lll,u,uu,uuu) = LUdecomp_eff(n, a, b, c);
    vector v = y.copy();
    fwdsub_eff(l,ll,lll, v);//solve Lz=y
    backsub_eff(u,uu,uuu, v);//solve Ux=z
    return v;
}

static void fwdsub_eff(vector l, vector ll, vector lll, vector b){
    int n = b.size;
    ///b[0]= b[0]/l[0] (remember that l[0]=0)
    b[1] = b[1]-ll[0]*b[0];
    for(int i = 2; i<n; i++){
        b[i]=b[i]-lll[i-2]*b[i-2]-ll[i-1]*b[i-1];
    }
}

static void backsub_eff(vector u, vector uu, vector uuu, vector c){
    int n = c.size;
    c[n-1] = c[n-1]/u[n-1];
    c[n-2] = (c[n-2]-uu[n-2]*c[n-1])/u[n-2];
    c[n-3] = (c[n-3]-uu[n-3]*c[n-2]-uuu[0]*c[n-1])/u[n-3];
    for(int i = n-4; i>=1; i--){
        c[i] = (c[i]-uu[i]*c[i+1]-uuu[1]*c[i+2])/u[i];
    }
    c[0] = (c[0]-uu[0]*c[1]-uuu[0]*c[2])/u[0];
}
    

}