using System;
using static System.Math;
public class ann{
    public int n; /* number of hidden neurons */
    Func<double,double> f; /* activation function */
    vector p; /* network parameters ( shifts ai and scales bi and all weight-factors wi) */

    public ann(int n){
        this.n = n;
        p = new vector(3*n);
        var random = new Random();
        var rDouble = random.NextDouble();
        double upperBound = -1.0;
        double lowerBound = 1.0;
        var rRangeDouble = rDouble * (upperBound - lowerBound) + lowerBound;
        for(int i = 0; i<n; i++){
            p[i]=1;//ai
            p[n+i]=1;//bi
            p[2*n+i]=rRangeDouble;//wi 
        }
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
        //var (p_opt, step, exceeded_step_max) = minimization.newton(Cp, p_start);
        
        vector a = p.copy()+dp;
        vector b = p.copy()+dp;

        /*vector a = p.copy();
        vector b = p.copy();
        for(int i = 0; i<3*n;i++){
            a[i]-=0.7;
            b[i]+=0.7;
        }*/

        /*vector a = new vector(3*n);
        vector b = new vector(3*n);
        for(int i = 0; i<3*n; i++){
            a[i]= Double.NegativeInfinity;
            b[i]=Double.PositiveInfinity;
        }*/
        
        var (p_opt, step, exceeded_step_max) = minimization.newton_prernd(Cp, p_start, a, b, max_time : 5, max_pren : 10000);
        /////simplex.downhill(Cp, ref p_start);
        /////vector p_opt = p_start;

    ////p=bbpso.run(Cp,p-dp,p+dp);
        //p=p_opt;
        p=p_opt;
    }

    public string parameters(string format="{0,4:g3}"){
        return p.getString(format);
    }

}
