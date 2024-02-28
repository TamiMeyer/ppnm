default: OutTaskA.txt

OutTaskA.txt : mainTaskA.exe
	mono $< >$@

mainTaskA.exe : mainTaskA.cs matrix.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $(filter %.cs,$^)

matrix.dll :  ../../matlib/vector.cs  ../../matlib/matrix.cs jacobi.cs
	mcs -target:library -out:./matrix.dll $^

clean:
	$(RM) *.dll *.exe [Oo]ut* *.data *.svg 