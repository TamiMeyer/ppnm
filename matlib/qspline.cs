using System;
public class qspline {
	vector x,y,b,c;
	public qspline(vector xs,vector ys){
		x=xs.copy();
		y=ys.copy();
		int n = x.size;
		b = new vector(n-1);
		c = new vector(n-1);
		c[0]=0; //choose one coefficient arbitrarily
		double dx, dy, dx1, dy1;
		for(int i=-1; i<n-2; i++){
			dx1=x[i+2]-x[i+1]; if(!(dx1>0)) throw new Exception("error dx1<=0");//dx_(i+1)
			dy1=y[i+2]-y[i+1]; //dy_(i+1)
			if(i>=0){
			    dx=x[i+1]-x[i]; if(i>=0 && !(dx>0)) throw new Exception("error dx<=0");//dx_i
        		dy=y[i+1]-y[i]; //dy_i
				c[i+1]=1/dx1*(dy1/dx1-dy/dx-c[i]*dx);	
			}
			b[i+1]=dy1/dx1-c[i+1]*dx1;
		}

	}

    public static int binsearch(double[] d, double z){/* locates the interval for z by bisection */ 
	    if( z<d[0] || z>d[d.Length-1] ) throw new Exception("binsearch: bad z");
	    int i=0, j=d.Length-1;
	    while(j-i>1){
		int mid=(i+j)/2;
		if(z>d[mid]) i=mid; else j=mid;
		}
	return i;
	}

	public double evaluate(double z){
		double[] x_arr =x.toArray();
        int i=binsearch(x_arr,z);
        return y[i]+b[i]*(z-x[i])+c[i]*(z-x[i])*(z-x[i]);

	}

	public double integral(double z){
        int i=binsearch(x,z);
        double integral = 0;
		double dx=0;
        for(int j=0; j<i; j++){ //integral of the interpolation up to the last datapoint before z
            dx = x[j+1]-x[j]; //dx_j
			integral += y[j]*dx+1.0/2*b[j]*Math.Pow(dx, 2)+1.0/3*c[j]*Math.Pow(dx, 3);

        }
		double lastpart = y[i]*(z-x[i])+1.0/2*b[i]*Math.Pow((z-x[i]), 2)+1.0/3*c[i]*Math.Pow((z-x[i]), 3);
        integral += lastpart;
        return integral; 
	}

	public double derivative(double z){
		double[] x_arr =x.toArray();
        int i=binsearch(x_arr,z);
        return b[i]+2*c[i]*(z-x[i]);
	}

	public void print_coefficients(){
		c.print("c = ");
		b.print("b = ");
	}
	}