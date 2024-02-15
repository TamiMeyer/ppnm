using System.Linq;

class main{
    public static void Main(string[] args){
        int nterms = (int)1e8;
        foreach(var arg in args){
            var words = arg.Split(':');
            if(words[0]=="-terms"  ) nterms  =(int)float.Parse(words[1]);
        }
        var sum = new System.Threading.ThreadLocal<double>( ()=>0, trackAllValues:true);
        System.Threading.Tasks.Parallel.For( 1, nterms+1, (int i)=>sum.Value+=1.0/i );
        double totalsum = sum.Values.Sum();
        System.Console.WriteLine("Total Sum: " + totalsum);
    }
}