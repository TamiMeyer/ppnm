using System;
using static System.Math;
public static class ode{
    public static (vector,vector) rkstep12(
	Func<double,vector,vector> f,/* the f from dy/dx=f(x,y) */
	double x,                    /* the current value of the variable */
	vector y,                    /* the current value y(x) of the sought function */
	double h                     /* the step to be taken */
	){
	    vector k0 = f(x,y);              /* embedded lower order formula (Euler) */
	    vector k1 = f(x+h/2,y+k0*(h/2)); /* higher order formula (midpoint) */
	    vector yh = y+k1*h;              /* y(x+h) estimate */
	    vector δy = (k1-k0)*h;           /* error estimate */
	    return (yh,δy);
    }//rkstep12

    public static (genlist<double>,genlist<vector>) driver(
	Func<double,vector,vector> F,/* the f from dy/dx=f(x,y) */
	(double,double) interval,    /* (start-point,end-point) */
	vector ystart,               /* y(start-point) */
	double h=0.125,              /* initial step-size */
	double acc=0.01,             /* absolute accuracy goal */
	double eps=0.01,              /* relative accuracy goal */
    int nmax = 999
	){
        var (a,b)=interval; double x=a; vector y=ystart.copy();
        var xlist=new genlist<double>(); xlist.add(x);
        var ylist=new genlist<vector>(); ylist.add(y);
		int nsteps = 0;
        do{
            if(x>=b || nsteps >= nmax) return (xlist,ylist); /* job done */
            if(x+h>b) h = b-x;               /* last step should end at b */
            var (yh,δy) = rkstep12(F,x,y,h);
            double tol = (acc+eps*yh.norm()) * Sqrt(h/(b-a));
            double err = δy.norm();
            if(err<=tol){ // accept step
		        x+=h; y=yh;
		        xlist.add(x);
		        ylist.add(y);
		    }
	        h *= Min( Pow(tol/err,0.25)*0.95 , 2); // readjust stepsize
        }while(true);
	}//driver

public static Func<double,vector> make_linear_interpolant(genlist<double> x,genlist<vector> y)
{
	Func<double,vector> interpolant = delegate(double z){
		int i=linspline.binsearch(x.toArray(),z);
		double Δx=x[i+1]-x[i];
		vector Δy=y[i+1]-y[i];
		return y[i]+Δy/Δx*(z-x[i]);
	};
	return interpolant;
}

public static Func<double,vector> make_ode_ivp_interpolant
(Func<double,vector,vector> f,(double,double)interval,vector y,double acc=0.01,double eps=0.01,double hstart=0.01, int nmax=999 )
{
	(var xlist,var ylist) = driver(f, interval, y, hstart, acc, eps, nmax);
	return make_linear_interpolant(xlist,ylist);
}

public static vector threebody_eqs(double t, vector z){
	double G = 1;//gravitational constant
	double[] masses = {1,1,1};//masses of the three bodys

	vector[] positions = new vector[3];
    vector[] velocities = new vector[3];
	for(int i = 0; i < 3; i++){
        positions[i] = new vector(z[2*i+6], z[2*i+7]);
        velocities[i] = new vector(z[2*i], z[2*i+1]);
    }

	vector[] accelerations = new vector[3];
	for(int i = 0; i < 3; i++){
        accelerations[i] = new vector(0, 0);
        for(int j = 0; j < 3; j++){
            if (i != j){
                vector r = positions[j] - positions[i];
                double r3 = Pow(r.norm(), 3);
                accelerations[i] += G * masses[j] * r / r3;//vi' = ∑j≠i G*mj*(rj-ri) /|rj-ri|3
            }
        }
    }

    vector result = new vector(12);
    for(int i = 0; i < 3; i++){
        result[2*i] = accelerations[i][0];
        result[2*i+ 1] = accelerations[i][1];
        result[2*i+ 6] = velocities[i][0];
        result[2*i+ 7] = velocities[i][1];
    }
    return result;	
}


}