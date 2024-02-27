
public static class QRGS{
    public static (matrix,matrix) decomp(matrix A){
        matrix Q=A.copy();
        var m = A.size2;
        matrix R=new matrix(m, m);
        for(int i = 0; i<m; i++){
            R[i,i] = Q[i].norm(); //Rii = sqrt(ai*ai)
            Q[i] /= R[i,i]; //qi = ai/Rii
            for(int j = i+1; j<m; j++){
                R[i,j]=Q[i].dot(Q[j]); //Rij = qi*aj
                Q[j]-=Q[i]*R[i,j]; 
            }
        }
        return (Q, R);
    }

    public static vector solve(matrix Q, matrix R, vector b){
        vector c = Q.T*b; 
        backsub(R,c); //solves the equation R*x=Q^T*b by backsubstitution
        return c;
    }

    public static double det(matrix R){ /*determinant of R is + or - the determinant of A*/
        double det = 1;
        for(int i = 0; i<R.size2; i++){
            det *= R[i,i];
        }
        return det;
    }

    public static matrix inverse(matrix Q,matrix R){ 
        matrix I = new matrix(Q.size1);
        I.set_unity();
        for(int i = 0; i<Q.size1; i++){
            I[i] = solve(Q, R, I[i]);
        }
        return I;

    }

    static void backsub(matrix U, vector c){
        for(int i = c.size-1; i >= 0; i--){
            double sum=0;
            for(int k = i+1; k<c.size; k++) sum+=U[i,k]* c[k];
            c[i] = (c[i]-sum)/U[i,i];
        }
    }

}
