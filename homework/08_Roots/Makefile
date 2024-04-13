CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
DATA = 

default:Out.txt

Out.txt DATA: main.exe 
	mono $< >Out.txt

main.exe : main.cs rootfinding.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

rootfinding.dll: root.cs ../../matlib/vector.cs  ../../matlib/matrix.cs ../../matlib/QRGS.cs
	mcs -target:library -out:./rootfinding.dll $^ 

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg