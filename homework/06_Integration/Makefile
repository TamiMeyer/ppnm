CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
DATA = Out.integ_erf.data Out.singleprec_erf.data

default:Out.txt Out.integ_erf.svg

Out.txt DATA: main.exe 
	mono $< >Out.txt

Out.integ_erf.svg : plot_integration_erf.gp Out.integ_erf.data tabulated_erf.data Out.singleprec_erf.data
	gnuplot -e "outputname='$@'" $<

main.exe : main.cs integ.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

integ.dll: integ.cs ../../matlib/funs.cs
	mcs -target:library -out:./integ.dll $^ 

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg