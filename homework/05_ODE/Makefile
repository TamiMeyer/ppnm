CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
DATA = Out.harm1.data Out.harm2.data Out.pend1.data Out.oscfric.data Out.planet.circular.data Out.planet.elliptical.data Out.planet.precession.data

default: Out.txt Out.ode.harm.svg Out.ode.pend.svg Out.ode.oscfric.svg Out.ode.planet.svg

Out.txt DATA: main.exe 
	mono $< >Out.txt

Out.ode.harm.svg : plot_ode_harm.gp Out.harm1.data Out.harm2.data
	gnuplot -e "outputname='$@'" $<

Out.ode.pend.svg : plot_ode_pend.gp Out.pend1.data
	gnuplot -e "outputname='$@'" $<

Out.ode.oscfric.svg : plot_ode_oscfric.gp Out.oscfric.data
	gnuplot -e "outputname='$@'" $<

Out.ode.planet.svg : plot_ode_planet.gp Out.planet.circular.data Out.planet.elliptical.data Out.planet.precession.data
	gnuplot -e "outputname='$@'" $<

main.exe : main.cs ode.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

ode.dll: ode.cs ../../matlib/genlist.cs  ../../matlib/vector.cs  ../../matlib/matrix.cs ../../matlib/linspline.cs
	mcs -target:library -out:./ode.dll $^ 

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg