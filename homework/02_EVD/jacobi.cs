using System;
using static System.Console;
using static System.Math;

public static class jacobi{
public static void timesJ(matrix A, int p, int q, double theta){/*multiplies the given Matrix A with Jacobi matrix from the right (in-place)*/
    double c=Cos(theta);
	double s=Sin(theta);
    for(int i=0;i<A.size1;i++){//change only the required matrix elements (e.q. 9.8 in notes)
		double aip=A[i,p];
		double aiq=A[i,q];
		A[i,p]=c*aip-s*aiq;
		A[i,q]=s*aip+c*aiq;
	}
}

public static void Jtimes(matrix A, int p, int q, double theta){/*multiplies the given Matrix A with Jacobi matrix from the left (in-place)*/ 
	double c=Cos(theta);
	double s=Sin(theta);
	for(int j=0;j<A.size1;j++){//change only the required matrix elements (e.q. 9.8 in notes)
		double apj=A[p,j];
		double aqj=A[q,j];
		A[p,j]= c*apj+s*aqj;
		A[q,j]=-s*apj+c*aqj;
		}
}

public static (vector,matrix) cyclic(matrix M){
	int n=M.size1;
	matrix A=M.copy();
	matrix V=matrix.id(n);
	vector w=new vector(n);
	bool changed;
	do{
		changed=false;
		for(int p=0;p<n-1;p++)
		for(int q=p+1;q<n;q++){
			double apq=A[p,q], app=A[p,p], aqq=A[q,q];
			double theta=0.5*Atan2(2*apq,aqq-app);
			double c=Cos(theta),s=Sin(theta);
			double new_app=c*c*app-2*s*c*apq+s*s*aqq;
			double new_aqq=s*s*app+2*s*c*apq+c*c*aqq;
			if(new_app!=app || new_aqq!=aqq) // do rotation on A and update V
			{
				changed=true;
				timesJ(A,p,q, theta); // A←A*J 
				Jtimes(A,p,q,-theta); // A←JT*A 
				timesJ(V,p,q, theta); // V←V*J
			}
		}
	}while(changed);
	for(int i=0; i<n; i++){ /*copy diagonal elements into w */
		w[i]=A[i,i];
	}
	return (w,V); //returns the diagonal elements w (i.e. eigenvalues) and the orthogonal matrix of eigenvectors V
}
}