using System;
using static System.Math;
public class ann{
    public int n; /* number of hidden neurons */
    private Func<double,double> f; /* activation function */
    private vector p; /* network parameters ( shifts ai and scales bi and all weight-factors wi) */

    public ann(int n){
        this.n = n;
        p = new vector(3*n);
        f = x => x*Exp(-x*x); //gaussian wavelet
    }

    public ann(int n, Func<double,double> f){
        this.n = n;
        p = new vector(3*n);
        this.f = f;
    }

    public double response(double x, vector q){
      vector outv = outputsignal(x, q);
      double resp = 0.0;
      for( int i = 0; i<n; i++) resp += outv[i];
      return resp;
    }

    public double response(double x){/* returns the response of the network to the input signal x */
        return response(x, p);
    }

    vector outputsignal(double x, vector q){ //the neuron number i transforms its input signal, x, into the its output signal, out[i-1]
        vector outv = new vector(n);
        for(int i = 0; i<n; i++){
            outv[i] = f((x-q[i])/q[n+i])*q[2*n+i];
        }
        return outv;
    }    

    public void train(vector x,vector y){
        /* train the network to interpolate the given table {x,y} */
        vector p_start = p.copy();
	    vector dp=p.copy();
	    for(int i=0;i<n;i++)p_start[i]=x[0]+i*(x[x.size-1]-x[0])/(n-1);
	    for(int i=0;i<n;i++)p_start[n+i]= 0.1+i*20/(n-1);
	    for(int i=0;i<n;i++)p_start[2*n+i]=-10+i*20/(n-1);
	    for(int i=0;i<n;i++)dp[i]=x[x.size-1]-x[0];
	    for(int i=0;i<n;i++)dp[n+i]=0.9;
	    for(int i=0;i<n;i++)dp[2*n+i]=5;

        //Cost function
        Func<vector, double> Cp = delegate(vector q){
            double sum = 0;
            for(int i = 0; i<x.size; i++) sum += Pow(response(x[i], q)-y[i], 2);
            return sum/x.size;
        };

        //minimize the cost fuction        
        vector a = p.copy()-dp;
        vector b = p.copy()+dp;
        var (p_opt, step, exceeded_step_max) = minimization.newton_prernd(Cp, p_start, a, b, max_time : 5, max_pren : 100000);
        p=p_opt;

        //prevents that there will be a warning due to unused variables
        #pragma warning disable CS0219
        int s = step;
        bool ex = exceeded_step_max;
        #pragma warning restore CS0219
    }

    public double deriv_gausswv(double z){//should only be used if the activation function is gauassian wawvelet
        double sum = 0;
        Func<double, double> deriv_gausswv_f = x => Exp(-x*x)+x*(-2*x)*Exp(-x*x);
        for(int i = 0; i<n; i++) sum += p[2*n+i]*deriv_gausswv_f((z-p[i])/p[n+i])/p[n+i];
        return sum;
    }

    public string parameters(string format="{0,4:g3}"){
        return p.getString(format);
    }

}
