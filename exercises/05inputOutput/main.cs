//using static System;
using static System.Console;
using static System.Math;

class main{

    public static int Main(string[] args){
        /*read the names of the "inputfile" and "outputfile" from the command-line.
        Read a set of numbers from the command-line argument in the form of a comma-separated list
        and print to the standard output.*/
        string infile=null, outfile=null;
        foreach(var arg in args){
	        var words = arg.Split(':');
    	    if(words[0]=="-numbers"){
	    	    var numbers=words[1].Split(',');
	    	    foreach(var number in numbers){
			       double x = double.Parse(number);
	    		   WriteLine($"{x} {Sin(x)} {Cos(x)}");
			    }
		    }
    	    if(words[0]=="-input"){infile=words[1];}
    	    if(words[0]=="-output"){outfile=words[1];}
	    }

        if(infile==null || outfile==null){
            Error.WriteLine("wrong filename argument");
            return 1;
        }

        /* Read a set of numbers separated by newline characters from "inputfile" and write
        them together with their sines and cosines (in a table form) to "outputfile".*/
        var instream=new System.IO.StreamReader(infile);
        var outstream=new System.IO.StreamWriter(outfile, append:false);
        for(string line=instream.ReadLine(); line!=null; line=instream.ReadLine()){  
            double x=double.Parse(line);
            outstream.WriteLine($"{x} {Sin(x)} {Cos(x)}");
        }
        instream.Close();
        outstream.Close();

        /*
        Read a sequence of numbers, separated by a combination of blanks (' '), tabs ('\t'), and newline
        characters ('\n'), from the standard input (e.g. from echo in Makefile) and print these numbers
        together with their sines and cosines (in a table form) to the standard error.
        */
        char[] split_delimiters = {' ','\t','\n'};
        #var split_options = StringSplitOptions.RemoveEmptyEntries; //
        for( string line = ReadLine(); line != null; line = ReadLine() ){
	        var numbers = line.Split(split_delimiters,System.StringSplitOptions.RemoveEmptyEntries);
	        foreach(var number in numbers){
		        double x = double.Parse(number);
		        Error.WriteLine($"{x} {Sin(x)} {Cos(x)}");
            }
        }

        return 0;   
    }//Main
}//main

