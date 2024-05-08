CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
DATA = Out.response.data Out.train.data Out.derivative.data

default:Out.txt Out.response_taskA.svg Out.derivativeAndAnti_taskB.svg Out.spline_antiderivative.data

Out.txt DATA: main.exe 
	mono $< >Out.txt

Out.spline_antiderivative.data: main_taskB_spline.exe
	mono $< >Out.spline_antiderivative.data

Out.response_taskA.svg: plot_response_taskA.gp Out.train.data Out.response.data
	gnuplot -e "outputname='$@'" $<

Out.derivativeAndAnti_taskB.svg: plot_deriv_and_anti_taskB.gp Out.derivative.data Out.spline_antiderivative.data
	gnuplot -e "outputname='$@'" $<

main.exe : main.cs neuralnet.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

main_taskB_spline.exe : main_taskB_spline.cs Out.train.data spline.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

neuralnet.dll: ann.cs ../../matlib/vector.cs  ../../matlib/matrix.cs ../../matlib/minimization.cs ../../matlib/QRGS.cs
	mcs -target:library -out:./neuralnet.dll $^

spline.dll: ../../matlib/linspline.cs  ../../matlib/vector.cs  ../../matlib/matrix.cs ../../matlib/qspline.cs ../../matlib/cspline.cs
	mcs -target:library -out:./spline.dll $^

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg
