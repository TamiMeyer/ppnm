set terminal svg background "white"
set key bottom right
set output outputname
set xlabel "x"                  
set ylabel "y"
set title "Linear interpolation"

plot [0:8][] \
"Out.linspline.sin.data" with lines linetype 7 linecolor rgb "black" title "linear spline"\
, "Out.raw.sin.data" with points pointtype 2 linecolor rgb "black" title "Sin(x) raw data"


 
