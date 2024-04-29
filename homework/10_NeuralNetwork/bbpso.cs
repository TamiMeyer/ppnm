using System;
using System.Diagnostics;
using static System.Console;
using static System.Math;

public class normal_random{
	public Random rnd;
	public normal_random(int seed=1){
		rnd=new Random(seed);
	}
	public double next(double mean=0,double sigma=1){
		double sum=0;
		for(int i=0;i<12;i++)sum+=rnd.NextDouble();
		return mean+(sum-6)*sigma;
	}
}

public static class bbpso{

public static vector randomvec(vector a,vector b,Random rnd){
	var x=new vector(a.size);
	for(int i=0;i<a.size;i++)x[i]=a[i]+(b[i]-a[i])*rnd.NextDouble();
	return x;
	}

public static vector run
(Func<vector,double>f,vector a,vector b,int seconds=1)
{
int dim=a.size, N=8*dim;
vector[] x=new vector[N], p=new vector[N];
var fp=new double[N];
Random rnd=new Random();
var g=(a+b)/2;
double fg=f(g);
for(int i=0;i<N;i++){
	x[i]=randomvec(a,b,rnd);
	p[i]=x[i].copy();
	fp[i]=f(p[i]);
	if(fp[i]<fg){g=p[i].copy();fg=fp[i];}
	}
var start_time=DateTime.Now;
normal_random nrnd = new normal_random();
do{
	for(int i=0;i<N;i++){
		vector mean=(p[i]+g)/2;
		//double sigma=(p[i]-g).norm();
		vector sigma=(p[i]-g);
		for(int k=0;k<x[i].size;k++){
			x[i][k]=nrnd.next(mean[k],sigma[k]);
			}
		var fxi=f(x[i]);
		if(fxi<fp[i]){fp[i]=fxi;p[i]=x[i].copy();}
		if(fxi<fg)   {fg=fxi;   g=x[i].copy();}
	}
}while((DateTime.Now-start_time).TotalSeconds < seconds);
return g;	
}

}//class
