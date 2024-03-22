using static System.Math;
public static class funs{
    public static bool approx(double a, double b, double acc=1e-9, double eps=1e-9){
        if(Abs(b-a) <= acc) return true;
        if(Abs(b-a) <= Max(Abs(a),Abs(b))*eps) return true;
        return false;
    }

    /// single precision error function (Abramowitz and Stegun, from Wikipedia)
    public static double erf(double x){
        if(x<0) return -erf(-x);
        double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
        double t=1/(1+0.3275911*x);
        double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4])))); 
        return 1-sum*Exp(-x*x);
    }
}