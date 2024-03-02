default: OutTaskA.txt Out.E0_dr.svg Out.E0_rmax.svg

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))

OutTaskA.txt : mainTaskA.exe Makefile
	mono $< -rmax:10 -dr:0.5 >$@

Out.E0_dr.svg: plot_E0_dr.gpi Out.E0_dr.data
	gnuplot -e "outputname='$@'" $<

Out.E0_rmax.svg: plot_E0_rmax.gpi Out.E0_rmax.data
	gnuplot -e "outputname='$@'" $<

Out.Wfct.svg: plot_Wfct.gpi Out.Wfct.data
	gnuplot -e "outputname='$@'" $<
	
Out.E0_dr.data : mainTaskBConvergence.exe Makefile
	>$@
	for dr in $$(seq 0.05 0.05 0.5); do \
		mono $< -rmax:10 -dr:$$dr >> Out.E0_dr.data ;\
	done 

Out.E0_rmax.data : mainTaskBConvergence.exe Makefile
	>$@
	for rmax in $$(seq 2 1 9); do \
		mono $< -dr:0.1 -rmax:$$rmax >> Out.E0_rmax.data ;\
	done

Out.Wfct.data : mainTaskBWfct.exe Makefile
	>$@
	for rmax in $$(seq 9 1 9); do \
		mono $< -dr:0.1 -rmax:$$rmax >> Out.Wfct.data ;\
	done

mainTaskA.exe : mainTaskA.cs matrix.dll hydrogen.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

mainTaskBConvergence.exe : mainTaskBConvergence.cs matrix.dll hydrogen.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

mainTaskBWfct.exe : mainTaskBwfct.cs hydrogen.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

matrix.dll :  ../../matlib/vector.cs  ../../matlib/matrix.cs jacobi.cs
	mcs -target:library -out:./matrix.dll $^

hydrogen.dll :  hydrogen.cs matrix.dll
	mcs -target:library -out:hydrogen.dll -reference:matrix.dll $<

clean:
	$(RM) *.dll *.exe [Oo]ut* *.data *.svg 