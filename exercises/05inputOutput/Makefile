out.txt: main.exe
	echo 7 8 9 > input.txt #read from standard input # -numbers in the line below is read from the command line
	#mono main.exe  -numbers:1,2,3,4,5 < input.txt > out.txt  2>out2.txt #seperate file for standard output and standard error
	#mono main.exe  -numbers:1,2,3,4,5 < input.txt > out.txt  2>>out.txt #append the standard error to out.txt
	mono main.exe  -numbers:1,2,3,4,5 -input:input3.txt -output:out3.txt < input.txt > out.txt  2>out2.txt

main.exe: main.cs
	mcs -target:exe -out:$@ $<

clean:
	$(RM) out.txt out2.txt out3.txt main.exe