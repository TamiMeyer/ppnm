CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
DATA = Out.smoothsignalQR.data Out.smoothsignalQR_generated.data Out.smoothsignalLU_generated.data Out.smoothsignalLU_generated_eff.data

default: Out.txt Out.smoothQR.svg Out.smoothQR_generated.svg Out.smoothLU_generated.svg Out.smoothLU_generated_eff.svg

Out.txt DATA: main.exe 
	mono $< >Out.txt

main.exe : main.cs smoothing.dll signal.data
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

Out.smoothQR.svg: plot_smoothQR.gp Out.smoothsignalQR.data
	gnuplot -e "outputname='$@'" $<

Out.smoothQR_generated.svg: plot_smoothQR_generated.gp Out.smoothsignalQR_generated.data
	gnuplot -e "outputname='$@'" $<

Out.smoothLU_generated.svg: plot_smoothLU_generated.gp Out.smoothsignalLU_generated.data
	gnuplot -e "outputname='$@'" $<

Out.smoothLU_generated_eff.svg: plot_smoothLU_generated_eff.gp Out.smoothsignalLU_generated_eff.data
	gnuplot -e "outputname='$@'" $<

smoothing.dll: smooth.cs ../matlib/vector.cs  ../matlib/matrix.cs ../matlib/QRGS.cs
	mcs -target:library -out:./smoothing.dll $^ 

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg