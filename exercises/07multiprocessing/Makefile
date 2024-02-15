default: Out_ManMulti.txt Out_ParallelFor.txt Out_ParallelThreadlocal.txt

N=1e8
TIME = time --portability --append --output $@
Out_ManMulti.txt: main.exe Makefile;
	>$@
	$(TIME) mono $< -terms:$N -threads:1 >>$@
	$(TIME) mono $< -terms:$N -threads:2 >>$@
	$(TIME) mono $< -terms:$N -threads:3 >>$@
	$(TIME) mono $< -terms:$N -threads:4 >>$@

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

clean:
	$(RM) *.dll *.exe [Oo]ut*