Out.txt: main.exe
	mono $< 1> Out.txt 2> log

main.exe: main.cs
	mcs -out:$@ -target:exe -reference:System.Numerics.dll main.cs

clean:
	$(RM) *.dll *.exe log* [Oo]ut*