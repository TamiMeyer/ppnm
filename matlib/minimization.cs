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

//more stable but slower than Newton minimization
public static (vector, int, bool) downhillSimplex(Func<vector,double> f, vector v,double stepsize=1.0/64,double sizegoal=1.0/1024){
	vector[] ps = new vector[v.size +1];
	double[] f_ps = new double[v.size +1];
	vector p_high; //highest point
	vector p_low; //lowest point
	vector p_centr;
	int step_max = 1000;
	int steps = 0;
	int high, low;

	ps[v.size] = v.copy();
	f_ps[v.size] = f(ps[v.size]);
	for(int i = 0; i<v.size; i++){
		v[i]+=stepsize;
		ps[i] = v.copy();
		f_ps[i] = f(ps[i]);
		v[i] -= stepsize;
	}

	while(size(ps)>sizegoal && ++steps<step_max){
		//find highest , lowest , and centroid points of the simplex
		high = 0; // index of the vertex with the highest value of the function
		low = 0; // index of the vertex with the lowest value of the function
		double f_high = f_ps[high];
		double f_low = f_ps[low];
		for(int i = 1; i<ps.Length; i++){
			if(f_ps[i] > f_high) {f_high = f_ps[high]; high = i;}
			if(f_ps[i] < f_low) {f_low = f_ps[low]; low = i;}
		}
		p_high = ps[high];
		p_low = ps[low];
		p_centr = new vector(v.size);//center of gravity of all points, except for the highest
		for( int i = 0; i<ps.Length; i++) if(i != high) p_centr += ps[i]; 
		p_centr/=p_centr.size;
		
		vector p_ref = new vector(v.size); //reflection
		vector p_exp = new vector(v.size); //expansion
		vector p_con = new vector(v.size); //contraction
		
		p_ref = p_centr + (p_centr - p_high); //try reflection
		if(f(p_ref) < f_ps[low]){
			p_exp = p_centr + 2*(p_centr-p_high);//try expansion
			if(f(p_exp) <f(p_ref)){
				p_high = p_exp;
				
			}else{
				p_high = p_ref;
			}
		} else {
			if(f(p_ref) < f_ps[high]){
				p_high = p_ref;
			} else{
				p_con = p_centr + 0.5*(p_high - p_centr); //try contraction
				if(f(p_con) < f_ps[high]){
					p_high = p_con;
				} else {
					//do reduction
					for(int k = 0; k<ps.Length; k++){
						if(k != low) ps[k] = 0.5*(ps[k]+ps[low]);
					}
				}
			}
		}
		ps[high] = p_high;
		f_ps[high] = f(p_high);

		
	}//while
	bool exceeded_step_max = false;
	if (steps>=step_max) exceeded_step_max = true;
	return (ps[low], steps, exceeded_step_max);  
}

//size of simplex ps
static double size(vector[] ps){
	double max = 0.0;
	for( int i = 1; i<ps.Length; i++) max = Math.Max(max, (ps[0]-ps[1]).norm());
	return max;
}
 

}
