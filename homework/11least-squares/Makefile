CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))


matrix.dll :  ../../matlib/vector.cs  ../../matlib/matrix.cs jacobi.cs
	mcs -target:library -out:./matrix.dll $^

clean:
	$(RM) *.dll *.exe [Oo]ut* *.data *.svg 