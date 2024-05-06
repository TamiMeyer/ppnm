using static System.Math;
public class hydrogen{

    public static matrix hamiltonian(double rmax, double dr){
        int npoints = (int)(rmax/dr)-1;
        vector r = new vector(npoints);
        for(int i=0;i<npoints;i++)r[i]=dr*(i+1);
        matrix H = new matrix(npoints,npoints);
        for(int i=0;i<npoints-1;i++){
            H[i,i]  =-2;
            H[i,i+1]= 1;
            H[i+1,i]= 1;
        }
        H[npoints-1,npoints-1]=-2;
        matrix.scale(H, -0.5/dr/dr);
        for(int i=0;i<npoints;i++)H[i,i]+=-1/r[i];
        return H;
    }

    public static (vector,matrix) diagonalize_hamiltonian(matrix H){ //returns the vector of eigenvalues and the Matrix composed of eigenvectors
        return jacobi.cyclic(H);
    }

    public static (double, vector) lowest_eigenvalue_eigenvector(matrix H){ // returns the lowest eigenvalues and eigenfunctions of the s-wave states in the hydrogen atom
        var (w, V) = diagonalize_hamiltonian(H);
        double E0 = w[0];  //first guess for the lowest eigenvalue
        vector v0 = V[0]; // eigenvector corresponding to E0

        for(int i = 1; i<w.size; i++){
            if(w[i]<E0){
                E0 = w[i];
                v0 = V[i];
            }
        }
        return (E0, v0);
    }

    public static double[,] wavefunction(int k, double rmax, double dr){// returns the kth wavefunction
        matrix H = hamiltonian(rmax, dr);
        var (w,V) = diagonalize_hamiltonian(H);
        int npoints = (int)(rmax/dr)-1;
        double[,] wfct= new double[2,npoints];
        for(int i = 0; i<npoints; i++){
            wfct[0,i] = (i+1)*dr;
            wfct[1,i] =  V[i,k] * 1/Sqrt(dr);
        }
        return wfct;
    }
}