default: Out.txt out.times.data Out.times.svg

CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))

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

main.exe : main.cs matrix.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

timeQR.exe : timeQR.cs matrix.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

matrix.dll :  ../../matlib/vector.cs  ../../matlib/matrix.cs ../../matlib/QRGS.cs
	mcs -target:library -out:./matrix.dll $^

clean:
	$(RM) *.dll *.exe [Oo]ut* *.data *.svg err fit.log