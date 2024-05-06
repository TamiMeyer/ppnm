set terminal svg background "white" dynamic size 800,800
set output outputname
set xlabel "x"                  
set ylabel "y"
set multiplot layout 3,1 columns
set key outside

set title "Cubic interpolation, sin(x)"
plot [0:9][] \
sin(x) with lines linetype 7 linecolor rgb "black" title "sin(x)"\
,"Out.cspline.sin.data" using 1:2 with lines dashtype 7 linecolor rgb "red" title "cubic spline"\
, "Out.raw.sin.data" with points pointtype 2 linecolor rgb "blue" title "interpolation nodes"

set title "Integral of cubic interpolation, sin(x)"
plot [0:8][] \
-cos(x)+1 with lines linetype 7 linecolor rgb "black" title "-cos(x)+1"\
, "Out.cspline.sin.data" using 1:3 with lines dashtype 7 linecolor rgb "red" title "integral of cspline"\

set title "Derivative of cubic interpolation, sin(x)"
plot [0:8][] \
cos(x) with lines linetype 7 linecolor rgb "black" title "cos(x)"\
, "Out.cspline.sin.data" using 1:4 with lines dashtype 7 linecolor rgb "red" title "derivative of cspline"\

unset multiplot 