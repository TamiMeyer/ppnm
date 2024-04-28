using System;
using static System.Math;
public class ann{
    public int n; /* number of hidden neurons */
    Func<double,double> f; /* activation function */
    vector p; /* network parameters ( shifts ai and scales bi and all weight-factors wi) */

    public ann(int n){
        this.n = n;
        p = new vector(3*n);
        for(int i = 0; i<n; i++){
            p[i]=1;//ai
            p[n+i]=1;//bi
            p[2*n+i]=1;//wi
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

        //Cost function
        Func<vector, double> Cp = delegate(vector q){
            double sum = 0;
            for(int i = 0; i<x.size; i++) sum += Pow(response(x[i], q)-y[i], 2);
            return sum/x.size;
        };

        //minimize the cost fuction
        var (p_opt, step, exceeded_step_max) = minimization.newton(Cp, p_start);
        p=p_opt;
    }

    public string parameters(string format="{0,4:g3}"){
        return p.getString(format);
    }

}