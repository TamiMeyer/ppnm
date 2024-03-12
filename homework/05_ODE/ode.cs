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
    }

    public static (genlist<double>,genlist<vector>) driver(
	Func<double,vector,vector> F,/* the f from dy/dx=f(x,y) */
	(double,double) interval,    /* (start-point,end-point) */
	vector ystart,               /* y(start-point) */
	double h=0.125,              /* initial step-size */
	double acc=0.01,             /* absolute accuracy goal */
	double eps=0.01              /* relative accuracy goal */
    ){
        var (a,b)=interval; double x=a; vector y=ystart.copy();
        var xlist=new genlist<double>(); xlist.add(x);
        var ylist=new genlist<vector>(); ylist.add(y);
        do{
            if(x>=b) return (xlist,ylist); /* job done */
            if(x+h>b) h=b-x;               /* last step should end at b */
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


}