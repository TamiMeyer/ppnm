using System;
using static System.Math;
public static class montecarlo{

public static (double,double) pseudo_plainmc(Func<vector,double> f,vector a,vector b,int N){
    int dim=a.size;
    double V=1;
    for(int i=0;i<dim;i++)V*=b[i]-a[i];
    double sum=0,sum2=0;
	var x=new vector(dim);
	var rnd=new Random();
    for(int i=0;i<N;i++){
        for(int k=0;k<dim;k++)x[k]=a[k]+rnd.NextDouble()*(b[k]-a[k]);
        double fx=f(x);
        sum+=fx;
        sum2+=fx*fx;
    }
    double mean=sum/N, sigma=Sqrt(sum2/N-mean*mean);
    var result=(mean*V,sigma*V/Sqrt(N));
    return result;
}

public static (double, double) quasi_mc(Func<vector,double> f,vector a,vector b,int N){
    int dim=a.size;
    double V=1;
    for(int i=0;i<dim;i++)V*=b[i]-a[i];
    double sum_a=0,sum_b=0;
	vector x=new vector(dim);
    vector qrnd;
    for(int j = 0; j<N; j++){ //calculation with a halton sequence
        qrnd = halton(j, dim); 
        for(int k=0;k<dim;k++)x[k]=a[k]+qrnd[k]*(b[k]-a[k]);
        double fx=f(x);
        sum_a+=fx;
    }

    for(int j = 0; j<N; j++){ //calculation with ANOTHER halton sequence
        qrnd = halton(j, dim, 1); //produces another halton series by shifting the base by 1 
        for(int k=0;k<dim;k++)x[k]=a[k]+qrnd[k]*(b[k]-a[k]);
        double fx=f(x);
        sum_b+=fx;
    }

    double mean_a=sum_a/N;
    double mean_b=sum_b/N;
    double sigma= Abs(mean_a - mean_b);
    var result=((mean_a+mean_b)/2*V,sigma*V);
    return result;
}

public static double corput ( int n , int b){
    double q=0, bk=(double)1.0/b ;
    while(n>0){
        q += (n % b)*bk ;
        n /= b ;
        bk /= b ;
    }
    return q ;
}

public static vector halton(int n , int d, int baseshift = 0){ // returns the n-th Halton d-dimentional point in the unit volume
    vector x = new vector(d);
    int[] bas = {2 ,3 ,5 ,7 ,11 ,13 ,17 ,19 ,23 ,29 ,31 ,37 ,41 ,43 ,47 ,53 ,59 ,61};
    int maxd = bas.Length;
    if(d > maxd + baseshift){throw new System.Exception("halton dimension exceeds limit");}
    for(int i =0; i<d; i++) x[i]=corput(n, bas[i + baseshift]) ;
    return x;
}


}