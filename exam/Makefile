CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
DATA = Out.smoothsignal.data

default: Out.txt Out.smoothQR.svg

Out.txt DATA: main.exe 
	mono $< >Out.txt

main.exe : main.cs smoothing.dll signal.data
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

Out.smoothQR.svg: plot_smoothQR.gp Out.smoothsignal.data
	gnuplot -e "outputname='$@'" $<

smoothing.dll: smooth.cs ../matlib/vector.cs  ../matlib/matrix.cs ../matlib/QRGS.cs
	mcs -target:library -out:./smoothing.dll $^ 

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg