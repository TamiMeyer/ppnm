CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
DATA = Out.hydrogen_swave.data Out.hydrogen_convergence.data

default:Out.txt Out.wfctAndExact.svg Out.convergenceOfE0.svg

Out.txt DATA: main.exe 
	mono $< >Out.txt

Out.wfctAndExact.svg : plot_wfctAndExact.gp Out.hydrogen_swave.data
	gnuplot -e "outputname='$@'" $<

Out.convergenceOfE0.svg : plot_convergenceOfE0.gp Out.hydrogen_convergence.data
	gnuplot -e "outputname='$@'" $<

main.exe : main.cs rootfinding.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

rootfinding.dll: root.cs hydrogen_root.cs ../05_ODE/ode.cs ../../matlib/linspline.cs ../../matlib/vector.cs  ../../matlib/matrix.cs ../../matlib/QRGS.cs ../../matlib/genlist.cs
	mcs -target:library -out:./rootfinding.dll $^ 

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg