default: Out_ManMulti.txt Out_ParallelFor.txt Out_ParallelThreadlocal.txt

N=2e8
TIME = time --portability --append --format "$$nthreads %e %U" --output $@
Out_ManMulti.txt: main.exe Makefile;
	>$@ 
	$(TIME) mono $< -terms:$N -threads:1 >>$@
	$(TIME) mono $< -terms:$N -threads:2 >>$@
	$(TIME) mono $< -terms:$N -threads:3 >>$@
	$(TIME) mono $< -terms:$N -threads:4 >>$@
	$(TIME) mono $< -terms:$N -threads:5 >>$@
	$(TIME) mono $< -terms:$N -threads:6 >>$@
	$(TIME) mono $< -terms:$N -threads:7 >>$@
	$(TIME) mono $< -terms:$N -threads:8 >>$@

Out_ParallelFor.txt: main2.exe Makefile
	>$@
	$(TIME) mono $< -terms:$N >>$@

Out_ParallelThreadlocal.txt: main3.exe Makefile
	>$@
	$(TIME) mono $< -terms:$N >>$@

main.exe: main.cs
	mcs -target:exe -out:$@ $<

main2.exe: main2.cs
	mcs -target:exe -out:$@ $<

main3.exe: main3.cs
	mcs -target:exe -out:$@ $<

Out_ManMulti.times.png : Out_ManMulti.txt plotManMulti.times.gpi
	gnuplot plotManMulti.times.gpi

clean:
	$(RM) *.dll *.exe [Oo]ut*