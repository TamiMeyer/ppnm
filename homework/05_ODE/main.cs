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
        The pendulum ode u''=-sin(u) is solved.");
        var outstream_pend=new System.IO.StreamWriter("Out.pend1.data", append:false);
        Func<double, vector, vector> f_pend = (x, y) => new vector(y[1], -Math.Sin(y[0]));// u'' = -sin(u)
        vector ystart3 = new vector(1, 0);// Initial conditions: u(0)=1, u'(0)=0
        (genlist<double> xlist3, genlist<vector> ylist3) = ode.driver(f_pend, (0, 100), ystart3);
        outstream_pend.WriteLine("x    y0   y1");
        for(int i=0; i<xlist3.size; i++){
            outstream_pend.Write($"{xlist3[i]}");
            for(int j=0; j<ylist3[i].size; j++){
                outstream_pend.Write($" {ylist3[i][j]}"); 
            }
            outstream_pend.WriteLine();
        }
        outstream_pend.Close();



        return 0;
    }
}