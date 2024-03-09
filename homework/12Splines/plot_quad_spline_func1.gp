set terminal svg background "white"
set output outputname
set xlabel "x"                  
set ylabel "y"
set title "Quadratic interpolation, func1"
set key top left

plot [0:14][] \
"Out.qspline.func1.data" using 1:2 with lines dashtype 7 linecolor rgb "red" title "qspline"\
, "Out.raw.func1.data" with points pointtype 2 linecolor rgb "blue" title "interpolation nodes"

