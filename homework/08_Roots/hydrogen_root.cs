using System;

public static class hydrogen_root{

    public static (genlist<double> , genlist<vector>) FE(double E, double r, double rmin = 1e-4, double acc = 0.01, double eps = 0.01){ 
        ////if(r<rmin) return r-r*r;
        Func<double, vector, vector> f = (x, y) => new vector(y[1], -2*(1/x + E) * y[0]);// differential equation: -(1/2)f'' -(1/r)f = Ef
        vector ystart = new vector(rmin-rmin*rmin, 1 - 2*rmin);
        (genlist<double> xlist, genlist<vector> ylist) = ode.driver(f, (rmin,r), ystart, 0.125, acc, eps, 999);

        return (xlist, ylist);
    }

    public static double FE_at_rmax(double E, double rmax, double rmin = 1e-4, double acc = 0.01, double eps = 0.01){//returns the value of FE(rmax) where FE(r) solves the s-wave radial Schrödinger equation
        (genlist<double> xlist, genlist<vector> ylist) = FE(E, rmax, rmin, acc, eps);
        double FE_rmax = ylist.Last()[0];
        return FE_rmax;
    }

    public static double lowest_E0(double rmax, double rmin = 1e-4, double acc = 0.01, double eps = 0.01){
        Func<vector,vector> M = delegate(vector y){return new vector(FE_at_rmax(y[0], rmax, rmin, acc, eps));};
        vector E0 = root.newton(M, new vector(-1.0));
        //todo: check various staring points
        return E0[0];
    }

    


}