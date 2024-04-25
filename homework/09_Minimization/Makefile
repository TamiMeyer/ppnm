CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
DATA = 

default: Out.txt Out.higgs_fit.svg

Out.txt DATA: main.exe experiment_Higgs.data
	mono $< < experiment_Higgs.data 1>Out.txt 2>log

Out.higgs_fit.svg: plot_higgs_fit.gp experiment_Higgs.data
	gnuplot -e "outputname='$@'" $<

main.exe : main.cs minimization.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

minimization.dll: minimization.cs  ../../matlib/QRGS.cs ../../matlib/vector.cs  ../../matlib/matrix.cs  ../../matlib/genlist.cs
	mcs -target:library -out:./minimization.dll $^ 

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg log