CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))

default: Out.txt Out.ode.harm.svg Out.ode.pend.svg Out.ode.oscfric.svg Out.ode.planet.svg

Out.txt : main.exe 
	mono $< >Out.txt

Out.ode.harm.svg : plot_ode_harm.gp main.exe
	gnuplot -e "outputname='$@'" $<

Out.ode.pend.svg : plot_ode_pend.gp main.exe
	gnuplot -e "outputname='$@'" $<

Out.ode.oscfric.svg : plot_ode_oscfric.gp main.exe
	gnuplot -e "outputname='$@'" $<

Out.ode.planet.svg : plot_ode_planet.gp main.exe
	gnuplot -e "outputname='$@'" $<

main.exe : main.cs ode.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

ode.dll: ode.cs ../../matlib/genlist.cs  ../../matlib/vector.cs  ../../matlib/matrix.cs ../../matlib/linspline.cs
	mcs -target:library -out:./ode.dll $^

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg