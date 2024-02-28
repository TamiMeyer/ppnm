default: Out.txt out.times.data Out.times.svg

Out.txt: main.exe
	mono $< > $@

Out.times.svg: plotTimes.gpi out.times.data
	gnuplot -e "outputname='$@'" $<

out.times.data : timeQR.exe Makefile
	>$@
	for N in $$(seq 100 50 900); do \
		time --format "$$N %e" --output $@ --append \
		mono $< -size:$$N 2>err ;\
	done 

main.exe : main.cs QRGS.cs matrix.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $(filter %.cs,$^)

timeQR.exe : timeQR.cs QRGS.cs matrix.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $(filter %.cs,$^)

matrix.dll :  ../../matlib/vector.cs  ../../matlib/matrix.cs
	mcs -target:library -out:./matrix.dll $^

clean:
	$(RM) *.dll *.exe [Oo]ut* *.data *.svg err log