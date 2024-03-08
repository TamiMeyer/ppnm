CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))

default: Out.txt Out.linspline.sin.svg Out.linspline.exp.svg

Out.txt : main.exe Makefile
	mono $< >$@

Out.linspline.sin.svg: plot_linear_spline_sin.gp main.exe
	gnuplot -e "outputname='$@'" $<

Out.linspline.exp.svg: plot_linear_spline_exp.gp main.exe
	gnuplot -e "outputname='$@'" $<

main.exe : main.cs spline.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

spline.dll: ../../matlib/linspline.cs
	mcs -target:library -out:./spline.dll $^

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg 