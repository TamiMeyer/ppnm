default: OutErr.png OutGamma.png OutLngamma.png

OutErr.png: plotErr.gnu data
	gnuplot plotErr.gnu

OutGamma.png: plotGamma.gnu data
	gnuplot plotGamma.gnu

OutLngamma.png: plotLngamma.gnu data
	gnuplot plotLngamma.gnu
	
data: main.exe
	mono $< > err.data

main.exe : main.cs sfuns.dll
	mcs -target:exe -out:$@ -reference:sfuns.dll main.cs

sfuns.dll: sfuns.cs
	mcs -out:$@ -target:library sfuns.cs

clean:
	$(RM)  [Oo]ut* *.exe *.dll *.png *.svg *.data