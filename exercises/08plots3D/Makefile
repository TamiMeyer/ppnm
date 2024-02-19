Out.png: plotGamma.gpi GammaComplex.data
	gnuplot $<
	
GammaComplex.data: main.exe
	mono $< > $@

main.exe : main.cs sfuns.dll
	mcs -target:exe -out:$@ -reference:sfuns.dll -reference:System.Numerics.dll main.cs

sfuns.dll: sfuns.cs
	mcs -out:$@ -target:library -reference:System.Numerics.dll sfuns.cs

clean:
	$(RM)  [Oo]ut* *.exe *.dll *.png *.svg *.data