using System;
public class cspline {
	vector x,y,b,c,d;
	public cspline(vector xs,vector ys){
        int n = xs.size;
		x=xs.copy();
        y=ys.copy();
        b = new vector(n);
        c = new vector(n-1);
        d = new vector(n-1);
        vector h = new vector(n-1);
        vector p = new vector(n-1);

        for (int i=0; i<n-1; i++){
            h[i] = x[i+1] - x[i];
            if (h[i] <= 0) throw new ArgumentException("Input x values must be strictly increasing.");
        }

        for (int i=0; i<n-1; i++) p[i] = (y[i+1] - y[i]) / h[i];

        vector D = new vector(n); //diagonal matrix elements
        vector Q = new vector(n-1); //above-main diagonal
        vector B = new vector(n); // right-hand side terms 

        D[0] = 2;
        for (int i=0; i<n-2; i++) D[i+1] = 2 * h[i] / h[i+1] + 2;
        D[n-1] = 2;

        Q[0] = 1;
        for (int i=0; i<n-2; i++) Q[i+1] = h[i] / h[i+1];
        
        B[0] = 3 * p[0];
        for (int i=0; i<n-2; i++) B[i+1] = 3*(p[i] + p[i+1] * h[i] / h[i+1]);
        B[n - 1] = 3 * p[n - 2];

        /*Gauss elimination*/
        for (int i=1; i<n; i++){
            D[i] -= Q[i-1] / D[i-1];
            B[i] -= B[i-1] / D[i-1];
        }

        /*back-substitution gives solution b to matrix*b=B, where the matrix-diagonal is given by D and the above-main diagonal by Q */
        b[n-1] = B[n-1] / D[n-1];
        for (int i = n-2; i>=0; i--)
            b[i] = (B[i] - Q[i] * b[i+1]) / D[i];

        for (int i=0; i<n-1; i++)
        {
            c[i] = (-2*b[i] - b[i+1] + 3*p[i]) / h[i]; //eq.20
            d[i] = (b[i] + b[i+1] - 2*p[i]) / (h[i]*h[i]); //eq.20
        }
	}

	public double evaluate(double z){
        int i = binsearch(x.toArray(), z);
        double dz = z-x[i];
        return y[i]+ dz*(b[i]+dz*(c[i]+dz*d[i]));        
    }

	public double derivative(double z){
        int i = binsearch(x.toArray(), z);
        double dz = z-x[i];
        return b[i] + 2*c[i]*dz + 3*d[i]*dz*dz;        
    }

	public double integral(double z){ //integral of the cubic spline from the point x[0] to the given point z
        int i = binsearch(x.toArray(), z);
        double integral = 0;
        double dx;
        for (int j = 0; j < i; j++)
        {
            dx = x[j+1]-x[j];
            integral += y[j]*dx + 1.0/2*b[j]*Math.Pow(dx,2) + 1.0/3*c[j]*Math.Pow(dx,3) + 1.0/4*d[j]*Math.Pow(dx,4);
        }
        dx = z-x[i];
        integral += y[i]*dx + 1.0/2*b[i]*Math.Pow(dx,2) + 1.0/3*c[i]*Math.Pow(dx,3) + 1.0/4*d[i]*Math.Pow(dx,4);
        return integral;
    }

    public double integral(double z1, double z2){//integral of the cubic spline from the given point z1 to the given point z2
        return integral(z2) - integral(z1);
    }

    public static int binsearch(double[] e, double z){/* locates the interval for z by bisection */ 
	    if( z<e[0] || z>e[e.Length-1] ) throw new Exception("binsearch: bad z");
	    int i=0, j=e.Length-1;
	    while(j-i>1){
		    int mid=(i+j)/2;
		    if(z>e[mid]) i=mid; else j=mid;
		}
	    return i;
	}

	}