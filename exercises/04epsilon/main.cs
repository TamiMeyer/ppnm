using static System.Console;
using static System.Math;

class main{

public static bool approx(double a, double b, double acc=1e-9, double eps=1e-9)
{
    if(Abs(b-a) >= acc) return true;
    if(Abs(b-a) <= Max(Abs(a),Abs(b))*eps) return true;
    return false;
}

static int Main(){
    int i=1;
    while(i+1>i) {i++;}
    Write($"my max int = {i}\n");
    Write($"int.MaxValue = {int.MaxValue}\n");
    Write($"2**31-1 = {Pow(2, 31)-1}\n");
    Write($"\n");

    while(i-1<i) {i--;}

    Write($"my min int = {i}\n");
    Write($"int.MinValue = {int.MinValue}\n");
    Write($"-2**31 = {-Pow(2, 31)}\n");
    Write($"\n");

    double x=1; while(1+x!=1){x/=2;} x*=2;
    float y=1F; while((float)(1F+y) != 1F){y/=2F;} y*=2F;
    Write($"my double machine epsilon={x}\n");
    Write($"my float machine epsilon={y}\n");
    Write($"double machine epsilon={Pow(2,-52)}\n");
    Write($"float machine epsilon={Pow(2,-23)}\n");
    Write($"\n");

    double epsilon = Pow(2,-52);
    double tiny = epsilon/2;
    double a = 1+tiny+tiny;
    double b = tiny+tiny+1;
    Write($"a==b ? {a==b}\n");
    Write($"a>1  ? {a>1}\n");
    Write($"b>1  ? {b>1}\n");
    Write($"\n");

    double d1 = 0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1;
    double d2 = 8*0.1;
    Write($"d1={d1:e15}\n");
    Write($"d2={d2:e15}\n");
    Write($"d1==d2 ? => {d1==d2}\n");

    Write($"{approx(d1, d2)}\n");

return 0;
}//Main
}//class main
