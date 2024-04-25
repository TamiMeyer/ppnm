CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
DATA = 

default:

Out.txt DATA: main.exe 
	mono $< >Out.txt

main.exe : main.cs integ.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

integ.dll: integ.cs ../../matlib/genlist.cs  ../../matlib/vector.cs  ../../matlib/matrix.cs ../../matlib/linspline.cs
	mcs -target:library -out:./ode.dll $^ 

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg