class main{
    public static void Main(string[] args){
        int nterms = (int)1e8;
        foreach(var arg in args){
            var words = arg.Split(':');
            if(words[0]=="-terms"  ) nterms  =(int)float.Parse(words[1]);
        }
        double sum=0;
        System.Threading.Tasks.Parallel.For( 1, nterms+1, (int i) => sum+=1.0/i );
        System.Console.WriteLine("Sum: " +sum);
    }
}