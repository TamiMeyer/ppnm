using System;
using static System.Math;
public static class root{

public static vector newton(
	Func<vector,vector>f /* the function to find the root of */
	,vector x            /* the start point */
	,double acc=1e-3     /* accuracy goal: on exit ‖f(x)‖ should be <acc */
	,vector δx=null      /* optional δx-vector for calculation of jacobian */
	,double λmin=1.0/64  /* */
	){
    vector fx=f(x),z,fz, Dx;
	matrix J;
    do{ /* Newton's iterations */
	    if(fx.norm() < acc) break; /* job done */
	    J=jacobian(f,x,fx,δx);
	    Dx = QRGS.solve(J, -fx); /* Newton's step (i.e. solve the equation: J Dx = -f(x) */
	    double λ=1.0;
	    do{ /* linesearch */
		    z=x+λ*Dx;
		    fz=f(z);
		    if( fz.norm() < (1-λ/2)*fx.norm() ) break;
		    if( λ < λmin ) break;
		    λ/=2.0;
	    }while(true);
	    x=z; fx=fz;
	}while(true);
	return x;
}

public static matrix jacobian(Func<vector,vector>f,vector x,vector fx, vector δx=null){
	if(δx==null) δx = x.map(xi => Abs(xi) * Pow(2, -26)); //set the vector δx (used in the finite-difference numerical evaluation of the Jacobian) if the user does not supply it
	int n = x.size;
	matrix J = new matrix(n);
	vector df, x_plus_xj = x.copy();
	for(int j=0; j<n; j++){
		x_plus_xj[j] += δx[j];
		df = f(x_plus_xj) - fx;
		for(int i = 0; i<n; i++){
			/////Console.WriteLine($" xsoze = {x.size}, df.size = {df.size}, i = {i}, j = {j}");
			J[i, j] = df[i] / δx[j];
		}
		x_plus_xj[j] -= δx[j];
	}
	return J;
}


}