Out.txt : sqrt.exe              # Out.txt depends on hello.exe
	mono sqrt.exe > Out.txt # run hello.exe, send output to Out.txt

sqrt.exe : sqrt.cs             # hello.exe depends on hello.cs
	mcs sqrt.cs             # compile hello.cs into hello.exe

clean:                           # a phoney target, no dependencies
	rm -f Out.txt sqrt.exe  # remove secondary files
