CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
DATA = Out.smoothsignalQR.data Out.smoothsignalQR_generated.data Out.smoothsignalLU_generated.data Out.smoothsignalLU_generated_eff.data Out.signalForTime.data
maxN1 = 1500 #maximum size of dataset for timing of smoothQR
maxN2 = 100000 #maximum size of dataset for timing of smoothLU_eff

default: Out.txt Out.smoothQR.svg Out.smoothQR_generated.svg Out.smoothLU_generated.svg Out.smoothLU_generated_eff.svg Out.timesSmooth.data Out.smoothTiming.svg

Out.txt DATA: main.exe Makefile
	mono $< -maxmaxN:$(maxN2) >Out.txt

main.exe : main.cs smoothing.dll signal.data
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

timeSmooth.exe : timeSmooth.cs smoothing.dll Out.signalForTime.data
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

Out.timesSmooth.data : timeSmooth.exe Makefile
	>$@
	# Loop over sizes and run the QR method
	echo "QR Method Timings" >> $@
	for N in $$(seq 250 250 $(maxN1)); do \
		time --format "$$N %e" --output $@ --append \
		mono $< -size:$$N -method:QR -maxN:$(maxN1) 2>err ;\
	done
	# Insert empty lines between the QR and LUeff method timings
	echo "" >> $@
	echo "" >> $@
	# Loop over sizes and run the LUeff method
	echo "LUeff Method Timings (small dataset)" >> $@
	for N in $$(seq 250 250 $(maxN1)); do \
		time --format "$$N %e" --output $@ --append \
		mono $< -size:$$N -method:LUeff -maxN:$(maxN1) 2>err ;\
	done
	echo "" >> $@
	echo "" >> $@
	# For larger datasets, Loop over sizes and run the LUeff method
	echo "LUeff Method Timings (large dataset)" >> $@
	for N in $$(seq 250 1000 $(maxN2)); do \
		time --format "$$N %e" --output $@ --append \
		mono $< -size:$$N -method:LUeff 2>err ;\
	done

Out.smoothQR.svg: plot_smoothQR.gp Out.smoothsignalQR.data
	gnuplot -e "outputname='$@'" $<

Out.smoothQR_generated.svg: plot_smoothAny_generated.gp Out.smoothsignalQR_generated.data
	gnuplot -e "outputname='$@'; inputname='$(word 2,$^)'; maintitle='Generated signal with random noise smoothened using QR-decomposition'" $<

Out.smoothLU_generated.svg: plot_smoothAny_generated.gp Out.smoothsignalLU_generated.data
	gnuplot -e "outputname='$@'; inputname='$(word 2,$^)'; maintitle='Generated signal with random noise smoothened using LU-factorization'" $<

Out.smoothLU_generated_eff.svg: plot_smoothAny_generated.gp Out.smoothsignalLU_generated_eff.data
	gnuplot -e "outputname='$@'; inputname='$(word 2,$^)'; maintitle='Generated signal with random noise smoothened using efficient LU-factorization'" $<

Out.smoothTiming.svg: plot_smoothTiming.gp Out.timesSmooth.data
	gnuplot -e "outputname='$@'" $<

smoothing.dll: smooth.cs ../matlib/vector.cs  ../matlib/matrix.cs ../matlib/QRGS.cs
	mcs -target:library -out:./smoothing.dll $^ 

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg err fit.log