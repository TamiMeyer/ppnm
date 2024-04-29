using System;
using static System.Math;
public static class minimization{
public static double LAMBDA_MIN = Pow(2,-13);

public static (vector, int, bool) newton(
	Func<vector,double> f, /* objective function */
	vector x,              /* starting point */
	double acc=1e-3,       /* accuracy goal, on exit |∇φ| should be < acc */
    double λmin = Double.NaN,
	int step_max = 1000,    /* limits the number of Newton's steps*/
    bool central_derivative = false /*false if forward difference approximations are used; true if central app. is used (task C)*/
){
	if( Double.IsNaN(λmin)) λmin = LAMBDA_MIN;
    double λ=1.0;
    double fx = f(x), fx_plus;
    vector gradf, dx;
	matrix H;
    int step = 0;
	bool exceeded_step_max = false; /*Has the maximum number of steps been exceeded?*/
	do{ /* Newton's iterations */
        step++;
		if(central_derivative){
            gradf = gradient_cent(f,x, fx);
        }
        else{
            gradf = gradient_fwd(f,x, fx);
        }
		if(gradf.norm() < acc) break; /* job done */
		H = hessian(f,x, central_derivative, gradf);
		dx = QRGS.solve(H, -gradf); /* Newton's step */
        λ=1.0;
		do{ /* linesearch */
			fx_plus = f(x+λ*dx);
			if( fx_plus < fx ) break; /* good step: accept */
			if( λ < λmin ) break; /* accept anyway */
			λ/=2;
		}while(true);
		x+=λ*dx;
		fx = fx_plus; /*saves a f(x+λ*dx) evaluation*/
		if(step >= step_max){
			exceeded_step_max = true; /*maybe interesting for later exercise?*/
			break; /*quit if maximum number of steps is exceeded, to prevent infinite loop*/
		} 
	}while(true);
	return (x, step, exceeded_step_max);
}//newton

public static vector gradient_fwd(Func<vector,double> f,vector x, double fx = Double.NaN){
	if(Double.IsNaN(fx)) fx = f(x);
	vector dfdx = new vector(x.size);
	for(int i=0;i<x.size;i++){
		double dx=Max(Abs(x[i]),1)*Pow(2,-26);
		x[i]+=dx;
		dfdx[i]=(f(x)-fx)/dx;
		x[i]-=dx;
	}
	return dfdx;
}

public static vector gradient_cent(Func<vector,double> f,vector x, double fx =Double.NaN){
	if(Double.IsNaN(fx)) fx = f(x);
	vector dfdx = new vector(x.size);
	/*for(int i=0;i<x.size;i++){
		double dx=Abs(x[i])*Pow(2,-26);
		x[i]+=dx;
		dfdx[i]=(f(x)-fx)/dx;
		x[i]-=dx;
	}*/
	return dfdx;
}

public static matrix hessian(Func<vector,double> f,vector x, bool central_derivative, vector gradf = null){
	if(gradf == null && central_derivative) gradf = gradient_cent(f,x);
	else if(gradf == null && !central_derivative) gradf= gradient_fwd(f,x);
	matrix H = new matrix(x.size);
	vector gradf_plus;
	for(int j=0;j<x.size;j++){
		double dx=Max(Abs(x[j]),1)*Pow(2,-13); /* for numerical gradient */
		x[j]+=dx;
		if(central_derivative) gradf_plus = gradient_cent(f,x);
		else gradf_plus = gradient_fwd(f,x);
		vector dgradf=gradf_plus-gradf;
		for(int i=0;i<x.size;i++) H[i,j]=dgradf[i]/dx;
		x[j]-=dx;
	}
	return 0.5*(H+H.transpose());
}
 

}
