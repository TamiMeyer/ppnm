// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Math;
using static System.Console;
public partial class vector{

private double[] data;

public int size => data.Length;

public double this[int i]{
[MethodImpl(MethodImplOptions.AggressiveInlining)]
	get => data[i];
[MethodImpl(MethodImplOptions.AggressiveInlining)]
	set => data[i]=value;
}

public vector(int n){data=new double[n];}
public vector(params double[] list){data=list;}
public vector(string s){
	char[] separators={',',' '};
	var options = StringSplitOptions.RemoveEmptyEntries;
        string[] words = s.Split(separators,options);
        int n = words.Length;
        data = new double[n];
        for(int i=0;i<size;i++){
                        this[i]=double.Parse(words[i]);
                        }
	}

public static implicit operator vector (double[] a){ return new vector(a); }
public static implicit operator double[] (vector v){ return v.data; }

public void print
(string s="",string format="{0,10:g3} ",TextWriter file=null){
	if(file==null)file = System.Console.Out;
	file.Write(s);
	for(int i=0;i<size;i++) file.Write(format,this[i]);
	file.Write("\n");
	}

public void printNoLn
(string s="",string format="{0,10:g3} ",TextWriter file=null){
	if(file==null)file = System.Console.Out;
	file.Write(s);
	for(int i=0;i<size;i++) file.Write(format,this[i]);
	}

public void fprint
(TextWriter file,string s="",string format="{0,10:g3} "){
	file.Write(s);
	for(int i=0;i<size;i++) file.Write(format,this[i]);
	file.Write("\n");
	}

public string getString(string format="{0,4:g3}"){
	StringBuilder sb = new StringBuilder();
	sb.Append("(");
	for(int i=0;i<size-1;i++) {
		sb.AppendFormat(format,this[i]);
		sb.Append(",");
	}
	sb.AppendFormat(format,this[size-1]);
	sb.Append(")");
	return sb.ToString();
}

public static vector operator+(vector v, vector u){
	vector r=new vector(v.size);
	for(int i=0;i<r.size;i++)r[i]=v[i]+u[i];
	return r; }

public static vector operator-(vector v){
	vector r=new vector(v.size);
	for(int i=0;i<r.size;i++)r[i]=-v[i];
	return r; }

public static vector operator-(vector v, vector u){
	vector r=new vector(v.size);
	for(int i=0;i<r.size;i++)r[i]=v[i]-u[i];
	return r; }

public static vector operator*(vector v, double a){
	vector r=new vector(v.size);
	for(int i=0;i<v.size;i++)r[i]=a*v[i];
	return r; }

public static vector operator*(double a, vector v){
	return v*a; }

public static vector operator/(vector v, double a){
	vector r=new vector(v.size);
	for(int i=0;i<v.size;i++)r[i]=v[i]/a;
	return r; }

public static vector operator/(vector v, vector w){
	vector r=new vector(v.size);
	for(int i=0;i<v.size;i++)r[i]=v[i]/w[i];
	return r; }

public double dot(vector o){
	double sum=0;
	for(int i=0;i<size;i++)sum+=this[i]*o[i];
	return sum;
	}
public static double operator%(vector a,vector b){
	return a.dot(b);
	}

public vector map(System.Func<double,double>f){
	vector v=new vector(size);
	for(int i=0;i<size;i++)v[i]=f(this[i]);
	return v;
	}

public double maxabs(){
	double r=Abs(this[0]);
	for(int i=1;i<size;i++)if(Abs(this[i])>r)r=Abs(this[i]);
	return r;
	}

public double max(){
	double r=this[0];
	for(int i=1;i<size;i++)if(this[i]>r)r=this[i];
	return r;
	}

public double min(){
	double r=this[0];
	for(int i=1;i<size;i++)if(this[i]<r)r=this[i];
	return r;
	}

public double norm(){
	double meanabs=0;
	for(int i=0;i<size;i++)meanabs+=Abs(this[i]);
	if(meanabs==0)meanabs=1;
	meanabs/=size;
	double sum=0;
	for(int i=0;i<size;i++)sum+=(this[i]/meanabs)*(this[i]/meanabs);
	return meanabs*Sqrt(sum);
	}

public vector copy(){
	vector b=new vector(this.size);
	for(int i=0;i<this.size;i++)b[i]=this[i];
	return b;
}

public static bool approx(double x, double y, double acc=1e-9, double eps=1e-9){
	if(Abs(x-y)<acc)return true;
	if(Abs(x-y)/Max(Abs(x),Abs(y))<eps)return true;
	return false;
	}

public static bool approx(vector a,vector b,double acc=1e-9,double eps=1e-9){
	if(a.size!=b.size)return false;
	for(int i=0;i<a.size;i++)
		if(!approx(a[i],b[i],acc,eps))return false;
	return true;
}
public bool approx(vector o){
	for(int i=0;i<size;i++)
		if(!approx(this[i],o[i]))return false;
	return true;
	}

public double[] toArray(){
	vector v = this.copy();
	return v.data;
}

/*create random vectors for tests*/

public static vector random_vector(int n){
    var rnd = new System.Random();
    vector v = new vector(n);
    for(int i = 0; i<n; i++){
        v[i] = 10*rnd.NextDouble();
    }
    return v;
}

/*random vector in range*/
public static vector random_vector(vector a,vector b,Random rnd){
	vector v=new vector(a.size);
	for(int i=0;i<a.size;i++) v[i] = a[i] + (b[i]-a[i])*rnd.NextDouble();
	return v;
}


}//vector