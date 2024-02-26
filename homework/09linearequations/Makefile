
Out.txt: main.exe
	mono $< > $@

main.exe : main.cs QRGS.cs matrix.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $(filter %.cs,$^)

matrix.dll : vector.cs matrix.cs
	mcs -target:library -out:$@ $^

clean:
	$(RM) *.dll *.exe [Oo]ut* *.data *.svg