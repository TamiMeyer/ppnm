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
    bool central_derivative = false, /*false if forward difference approximations are used; true if central app. is used (task C)*/
	double epshess = Double.NaN
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
		H = hessian(f,x, central_derivative, gradf, eps:epshess);
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

public static matrix hessian(Func<vector,double> f,vector x, bool central_derivative, vector gradf = null, double eps = Double.NaN){
	if(gradf == null && central_derivative) gradf = gradient_cent(f,x);
	else if(gradf == null && !central_derivative) gradf= gradient_fwd(f,x);
	if(!central_derivative && Double.IsNaN(eps)) eps = Pow(2,-26); //
	if(central_derivative && Double.IsNaN(eps)) eps = Pow(2,-26);
	matrix H = new matrix(x.size);
	vector gradf_plus;
	for(int j=0;j<x.size;j++){
		double dx=Max(Abs(x[j]),1)*eps; /* for numerical gradient */ 
		x[j]+=dx;
		if(central_derivative) gradf_plus = gradient_cent(f,x);
		else gradf_plus = gradient_fwd(f,x);
		vector dgradf=gradf_plus-gradf;
		for(int i=0;i<x.size;i++) H[i,j]=dgradf[i]/dx;
		x[j]-=dx;
	}
	return 0.5*(H+H.transpose());
}

/* needed in homework 10_ANN */
public static (vector, int, bool) newton_prernd(
	Func<vector,double> f, /* objective function */
	vector first,		   /* first starting point to try*/
	vector a,              /* 'lowest' possible starting point */
	vector b,			   /* 'highest' possible starting point */
	int max_time = 1,	   /* maximum time to check f at random points */
	int max_pren = 3000,   /* maximum number of random points that are checked*/
	double acc=1e-3,       /* accuracy goal, on exit |∇φ| should be < acc */
    double λmin = Double.NaN,
	int step_max = 1000,    /* limits the number of Newton's steps*/
    bool central_derivative = false /*false if forward difference approximations are used; true if central app. is used (task C)*/
){
	var rnd = new Random();
	vector best=first;
	double fbest=f(best);
	var start_time=DateTime.Now;
	do{
		for(int i=0;i<max_pren;i++){
			vector x=vector.random_vector(a,b,rnd);
			double fx=f(x);
			if(fx<fbest){
				best=x;
				fbest=fx;
			}
		}
	}while((DateTime.Now-start_time).TotalSeconds < max_time);
	var (x_opt, step, exceeded_step_max) = newton(f, best, acc,λmin,step_max,central_derivative);
	return (x_opt, step, exceeded_step_max);	
}

}
