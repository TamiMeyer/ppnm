CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
DATA = Out.response.data Out.train.data

default:Out.txt Out.response_taskA.svg

Out.txt DATA: main.exe 
	mono $< >Out.txt

Out.response_taskA.svg: plot_response_taskA.gp Out.train.data Out.response.data
	gnuplot -e "outputname='$@'" $<

main.exe : main.cs neuralnet.dll
	mcs -target:exe -out:$@ $(LIBS) $(CODE)

neuralnet.dll: ann.cs  bbpso.cs ../../matlib/vector.cs  ../../matlib/matrix.cs ../../matlib/minimization.cs ../../matlib/QRGS.cs
	mcs -target:library -out:./neuralnet.dll $^ 

clean:
	$(RM) *.dll *.exe [Oo]ut* *.svg
