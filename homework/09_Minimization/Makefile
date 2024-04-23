CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
DATA = 

default: Out.txt

Out.txt DATA: main.exe 
	mono $< >Out.txt

main.exe : main.cs minimization.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

minimization.dll: minimization.cs  ../../matlib/QRGS.cs ../../matlib/vector.cs  ../../matlib/matrix.cs
	mcs -target:library -out:./minimization.dll $^ 

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg