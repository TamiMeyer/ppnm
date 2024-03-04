CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))

default: Out.txt Out.fit.svg

Out.txt : main.exe Makefile
	mono $< -input:experiment.data >$@

Out.fit.svg: plot_fit.gp Out.experiment_fitted.data
	gnuplot -e "outputname='$@'" $<

main.exe : main.cs matrix.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

matrix.dll :  ../../matlib/vector.cs  ../../matlib/matrix.cs ../../matlib/QRGS.cs ../../matlib/fit.cs
	mcs -target:library -out:./matrix.dll $^

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg 