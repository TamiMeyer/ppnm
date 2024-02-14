using System;
using static System.Console;
class main{

    static double a=1;

    public static System.Func<double,double> make_power(int i){
        System.Func<double,double> f = delegate(double x){return System.Math.Pow(x*a,i);}; // a is captured by value when the function is defined 
        return f; //when returning f, a is returned by reference
    }

    public static int Main(){
        var list = new genlist<double[]>();
        char[] delimiters = {' ','\t'};
        var options = StringSplitOptions.RemoveEmptyEntries;
        for(string line = ReadLine(); line!=null; line = ReadLine()){
	        var words = line.Split(delimiters,options);
	        int n = words.Length;
	        var numbers = new double[n];
	        for(int i=0;i<n;i++) numbers[i] = double.Parse(words[i]);
	        list.add(numbers);
       	}
        for(int i=0;i<list.size;i++){
	        var numbers = list[i];
	        foreach(var number in numbers)Write($"{number : 0.00e+00;-0.00e+00} ");
	        WriteLine();
        }


        tryGenlistDelegatesClosure();
        return 0;
    }

    public static int tryGenlistDelegatesClosure(){
        var outstream = new System.IO.StreamWriter("outTryGenlistDelegatesClosure.txt", append:true);

        genlist<double> list = new genlist<double>();
        list.add(1.2);
        list.add(2.0);
        for(int i=0; i<list.size;i++)outstream.WriteLine(list[i]);
        outstream.WriteLine();
        list[0]=0;
        list[1]=1;
        list.add(2);
        for(int i=0; i<list.size;i++)outstream.WriteLine(list[i]);
        outstream.WriteLine();
        list.remove(1);
        for(int i=0; i<list.size;i++)outstream.WriteLine(list[i]);
        outstream.WriteLine();
        
        a=7;
        double x=10;
        System.Func<double,double> f = delegate(double tmp){return a*x;};   //System.Func<returntype, firstArgument, secondArgument,...>
        a=9;
        outstream.WriteLine($"f({x})={f(x)}");

        var flist = new genlist<System.Func<double,double>>();
        flist.add(f);
        flist.add(System.Math.Sin);
        flist.add(System.Math.Cos);

        a=1;
        var f1 = make_power(1);
        flist.add(f1); 
        a=2;
        var f2 = make_power(1);
        flist.add(f2);
        a=666;

        for(int i=0; i<flist.size;i++){
        outstream.WriteLine($"f[{i}]({x})={flist[i](x)}"); // f1 and f2 both cause the output with a=666 
        }

        outstream.Close();
        
        return 0;
    }
}