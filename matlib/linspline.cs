using System;
public static class linspline{
    
    public static double linterp(double[] x, double[] y, double z){
        int i=binsearch(x,z);
        double dx=x[i+1]-x[i]; if(!(dx>0)) throw new Exception("error dx<=0");
        double dy=y[i+1]-y[i];
        return y[i]+dy/dx*(z-x[i]);
    }

    public static int binsearch(double[] x, double z){/* locates the interval for z by bisection */ 
	    if( z<x[0] || z>x[x.Length-1] ) throw new Exception("binsearch: bad z");
	    int i=0, j=x.Length-1;
	    while(j-i>1){
		int mid=(i+j)/2;
		if(z>x[mid]) i=mid; else j=mid;
		}
	return i;
	}

    public static double linterpInteg(double[] x, double[] y, double z){/*calculates the integral of the linear spline from the point x[0] to the given point z*/
        int i=binsearch(x,z);
        double integral = 0;
        double dx=x[1]-x[0]; if(!(dx>0)) throw new Exception("error dx<=0");
        double dy=y[1]-y[0];
        for(int j=0; j<i; j++){ //integral of the interpolation up to the last datapoint before z
            integral += dx*y[j];//rectangular area
            integral += dx*dy/2; //triangualr area
            dx=x[j+2]-x[j+1]; if(!(dx>0)) throw new Exception("error dx<=0");
            dy=y[j+2]-y[j+1];
        }
        integral += y[i]*(z-x[i])+dy/dx*Math.Pow((z-x[i]),2)/2;
        return integral; 
    }
}