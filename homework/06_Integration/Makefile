CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))

default:Out.txt

Out.txt: main.exe 
	mono $< >Out.txt

main.exe : main.cs integ.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

integ.dll: integ.cs ../../matlib/funs.cs
	mcs -target:library -out:./integ.dll $^ 

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg