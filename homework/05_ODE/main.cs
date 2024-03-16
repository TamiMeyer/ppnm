using System;
using static System.Console;
public class main{
    public static int Main(){
        WriteLine("---Task A-------");
        WriteLine(@"See 'Out.ode.harm.svg': 
        The ode u''=-u is solved with two different initial conditions.
        First u(0)=1 and u'(0)=0, i.e. this can be interpreted as a mass that is displaced from the equilibrium position
        1 unit and released from rest. It oscillates back and forth around the equilibrium position.
        As expected, u(t) corresponds to cos(t), for the first set of initial conditions.
        Second u(0)=0.7 and u'(0)=0.7. ");
        //first set of initial conditions
        var outstream_harm=new System.IO.StreamWriter("Out.harm1.data", append:false);
        Func<double, vector, vector> f = (x, y) => new vector(y[1], -y[0]);// u'' = -u
        vector ystart = new vector(1, 0);// Initial conditions: u(0)=1, u'(0)=0
        (genlist<double> xlist, genlist<vector> ylist) = ode.driver(f, (0, 10), ystart);
        outstream_harm.WriteLine("x    y0   y1");
        for(int i=0; i<xlist.size; i++){
            outstream_harm.Write($"{xlist[i]}");
            for(int j=0; j<ylist[i].size; j++){
                outstream_harm.Write($" {ylist[i][j]}"); 
            }
            outstream_harm.WriteLine();
        }
        outstream_harm.Close();
        //second set of initial ocnditions
        var outstream_harm2=new System.IO.StreamWriter("Out.harm2.data", append:false);
        vector ystart2 = new vector(0.7, 0.7);// Initial conditions: u(0)=0.7, u'(0)=0.7
        (genlist<double> xlist2, genlist<vector> ylist2) = ode.driver(f, (0, 10), ystart2);
        outstream_harm2.WriteLine("x    y0   y1");
        for(int i=0; i<xlist2.size; i++){
            outstream_harm2.Write($"{xlist2[i]}");
            for(int j=0; j<ylist2[i].size; j++){
                outstream_harm2.Write($" {ylist2[i][j]}"); 
            }
            outstream_harm2.WriteLine();
        }
        outstream_harm2.Close();

        WriteLine(@"See 'Out.ode.pend.svg': 
        The pendulum equation u''=-sin(u) is solved. For comparison cos(t) is shown as well, which would be a solution if u<<1");
        var outstream_pend=new System.IO.StreamWriter("Out.pend1.data", append:false);
        Func<double, vector, vector> f_pend = (x, y) => new vector(y[1], -Math.Sin(y[0]));// u'' = -sin(u)
        vector ystart3 = new vector(1, 0);// Initial conditions: u(0)=1, u'(0)=0
        (genlist<double> xlist3, genlist<vector> ylist3) = ode.driver(f_pend, (0, 10), ystart3);
        outstream_pend.WriteLine("x    y0   y1");
        for(int i=0; i<xlist3.size; i++){
            outstream_pend.Write($"{xlist3[i]}");
            for(int j=0; j<ylist3[i].size; j++){
                outstream_pend.Write($" {ylist3[i][j]}"); 
            }
            outstream_pend.WriteLine();
        }
        outstream_pend.Close();

        WriteLine(@"See 'Out.ode.oscfric.svg': 
        The equation for the oscillator with friction is solved.");
        WriteLine("");
        double b = 0.25;
        double c = 5.0;
        var outstream_oscfric=new System.IO.StreamWriter("Out.oscfric.data", append:false);
        Func<double, vector, vector> f_oscfric = (x, y) => new vector(y[1], -b*y[1]-c*Math.Sin(y[0]) );// u''(t) + b*u'(t) + c*sin(u(t)) = 0
        vector ystart4 = new vector(Math.PI-0.1, 0);// Initial conditions: nearly vertical and at rest
        (genlist<double> xlist4, genlist<vector> ylist4) = ode.driver(f_oscfric, (0, 10), ystart4);
        outstream_oscfric.WriteLine("x    y0   y1");
        for(int i=0; i<xlist4.size; i++){
            outstream_oscfric.Write($"{xlist4[i]}");
            for(int j=0; j<ylist4[i].size; j++){
                outstream_oscfric.Write($" {ylist4[i][j]}"); 
            }
            outstream_oscfric.WriteLine();
        }
        outstream_oscfric.Close();

        WriteLine("---Task B-------");
        WriteLine(@"See 'Out.ode.planet.svg': 
        The equation of equatorial motion of a planet around a star in General Relativity,
        u''(φ) + u(φ) = 1 + εu(φ)2 , is solved and plot in x-y-plane.
        Here u(φ) ≡ 1/r(φ) , r is the (circumference-reduced) radial coordinate, φ is the azimuthal angle,
        ε is the relativistic correction (on the order of the star's Schwarzschild radius divided by the radius of the planet's orbit),
        and primes denote the derivative with respect to φ.
        The equation is integrated with different initial conditions and different values for the relativistic correction.");
        WriteLine("");

        double eps = 0; //relativistic correction
        var outstream_planeti=new System.IO.StreamWriter("Out.planet.circular.data", append:false);
        Func<double, vector, vector> f_planet = (x, y) => new vector(y[1], 1 - y[0] + eps*y[0]*y[0] );//u''(φ) + u(φ) = 1 + εu(φ)^2 
        vector ystart5 = new vector(1, 0);// Initial conditions
        Func<double, vector> interpolant_planeti = ode.make_ode_ivp_interpolant(f_planet, (0, 2*Math.PI+(1.0/8)), ystart5);
        
        outstream_planeti.WriteLine("x    y0   y1");
        vector ys;
        for(double i=0; i<2*Math.PI+(1.0/8); i+=(1.0/8)){
            ys = interpolant_planeti(i);
            outstream_planeti.WriteLine($"{i} {ys[0]} {ys[1]}");
        }
        outstream_planeti.Close();

        var outstream_planetii=new System.IO.StreamWriter("Out.planet.elliptical.data", append:false);
        vector ystart6 = new vector(1, -0.5);// Initial conditions
        Func<double, vector> interpolant_planetii = ode.make_ode_ivp_interpolant(f_planet, (0, 2*Math.PI+(1.0/8)), ystart6);
        outstream_planetii.WriteLine("x    y0   y1");
        for(double i=0; i<2*Math.PI+(1.0/8); i+=(1.0/8)){
            ys = interpolant_planetii(i);
            outstream_planetii.WriteLine($"{i} {ys[0]} {ys[1]}");
        }
        outstream_planetii.Close();

        eps = 0.01;
        var outstream_planetiii = new System.IO.StreamWriter("Out.planet.precession.data", append:false);
        Func<double, vector> interpolant_planetiii = ode.make_ode_ivp_interpolant(f_planet, (0, 8*Math.PI+(1.0/8)), ystart6);
        outstream_planetiii.WriteLine("x    y0   y1");
        for(double i=0; i<8*Math.PI+(1.0/8); i+=(1.0/8)){
            ys = interpolant_planetiii(i);
            outstream_planetiii.WriteLine($"{i} {ys[0]} {ys[1]}");
        }
        outstream_planetiii.Close();

        WriteLine("---Task C-------");
        WriteLine(@"See 'Out.ode.threebody.svg':
        Shows the figure-8 solution to the three-body problem.");
        WriteLine(@"See 'Out.ode.threebody.animation.gif':
        An animation of the figure-8 solution to the three-body problem over a full period T ≃ 6.3259");

        vector z0 = new vector(0.4662036850, 0.4323657300, -0.93240737, -0.86473146, 0.4662036850, 0.4323657300, -0.97000436, 0.24308753, 0, 0, 0.97000436, -0.24308753); //inital conditions from wikipedia article
        var (t_list, pos_vel_list) = ode.driver(ode.threebody_eqs, (0, 6.375), z0);
        var outstream_3 = new System.IO.StreamWriter("Out.threebody.data", append:false);
        outstream_3.WriteLine("t   vx1   vy1   vx2   vy2   vx3   vy3   x1   y1   x2   y2   x3   y3");
        for(int i=0; i<t_list.size; i++){
            outstream_3.Write($"{t_list[i]}");
            for(int j=0; j<pos_vel_list[i].size; j++){
                outstream_3.Write($" {pos_vel_list[i][j]}"); 
            }
            outstream_3.WriteLine();
        }
        outstream_3.Close();
        return 0;
    }
}