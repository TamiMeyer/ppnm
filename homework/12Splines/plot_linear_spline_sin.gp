set terminal svg background "white"
set key bottom left
set output outputname
set xlabel "x"                  
set ylabel "y"
set title "Linear interpolation, sin(x)"
set multiplot layout 2,1 columns

plot [0:8][] \
sin(x) with lines linetype 7 linecolor rgb "black" title "sin(x)"\
,"Out.linspline.sin.data" using 1:2 with lines dashtype 7 linecolor rgb "red" title "linear spline"\
, "Out.raw.sin.data" with points pointtype 2 linecolor rgb "blue" title "interpolation nodes"

plot [0:8][] \
-cos(x)+1 with lines linetype 7 linecolor rgb "black" title "-cos(x)+1"\
, "Out.linspline.sin.data" using 1:3 with lines dashtype 7 linecolor rgb "red" title "integral of interpolation"\

unset multiplot 
