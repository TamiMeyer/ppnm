CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))

default: Out.txt Out.linspline.sin.svg Out.linspline.exp.svg Out.qspline.func1.svg Out.qspline.sin.svg Out.cspline.sin.svg Out.cspline.builtin.svg

Out.txt: main.exe 
	mono $< >Out.txt

Out.linspline.sin.svg: plot_linear_spline_sin.gp main.exe
	gnuplot -e "outputname='$@'" $<

Out.linspline.exp.svg: plot_linear_spline_exp.gp main.exe
	gnuplot -e "outputname='$@'" $<

Out.qspline.func1.svg : plot_quad_spline_func1.gp main.exe
	gnuplot -e "outputname='$@'" $<

Out.qspline.sin.svg : plot_quad_spline_sin.gp main.exe
	gnuplot -e "outputname='$@'" $<

Out.cspline.sin.svg : plot_cubic_spline_sin.gp main.exe
	gnuplot -e "outputname='$@'" $<

Out.cspline.builtin.svg : plot_cubic_spline_builtin.gp main.exe
	gnuplot -e "outputname='$@'" $<

main.exe : main.cs spline.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

spline.dll: ../../matlib/linspline.cs  ../../matlib/vector.cs  ../../matlib/matrix.cs ../../matlib/qspline.cs ../../matlib/cspline.cs
	mcs -target:library -out:./spline.dll $^

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg

#Out.linspline.sin.data Out.raw.sin.data Out.linspline.exp.data Out.raw.exp.data Out.raw.func1.data Out.qspline.func1.data