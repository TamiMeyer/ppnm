Out.txt: main.exe Makefile
	mono main.exe > Out.txt

vec.dll: vec.cs
	mcs -target:library -out:vec.dll vec.cs

main.exe: main.cs vec.dll
	mcs -target:exe -reference:vec.dll -out:$@ $<

clean:
	$(RM) Out.txt main.exe