using static System.Console;
using static vec;

public class main{
    static int Main(){
        vec u = new vec();
        vec v = new vec(1,2,3);
        vec w = new vec(4,5,6);
        WriteLine($"initial values: u=({u.ToString()}), v=({v.ToString()}), w=({w.ToString()}) ");
        vec sum = w+v;
        WriteLine($"Sum: w+v=({sum.ToString()})");
        WriteLine($"Product: 3*v = ({(3*v).ToString()}) and v*3 = ({(v*3).ToString()})");
        WriteLine($"Difference: w-v=({(w-v).ToString()})");
        WriteLine($"-v=({(-v).ToString()})");

        WriteLine($"Test the print methods:");
        Write($"    ");
        v.print();
        Write($"    ");
        v.print("This is my v vector:");

        WriteLine($"dot product: v*w={(v.dot(w)).ToString()}");


    return 0;
    }
}