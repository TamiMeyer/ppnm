CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
DATA = Out.circle_pseudo.data Out.ellipse_pseudo.data

default: Out.txt Out.mc2D_pseudo.svg

Out.txt DATA: main.exe 
	mono $< >Out.txt

Out.mc2D_pseudo.svg: mc2D_pseudo.gp Out.circle_pseudo.data Out.ellipse_pseudo.data
	gnuplot -e "outputname='$@'" $<

main.exe : main.cs montecarlo.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

montecarlo.dll: montecarlo.cs  ../../matlib/vector.cs  ../../matlib/matrix.cs
	mcs -target:library -out:./montecarlo.dll $^ 

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg
