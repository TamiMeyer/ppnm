set terminal svg background "white"
set output outputname
set xlabel "x"                  
set ylabel "y"
set multiplot layout 2,1 columns

set title "Linear interpolation, sin(x)"
set key at 3,0
plot [0:9][-1.1:1.1] \
sin(x) with lines linetype 7 linecolor rgb "black" title "sin(x)"\
,"Out.linspline.sin.data" using 1:2 with lines dashtype 7 linecolor rgb "red" title "linear spline"\
, "Out.raw.sin.data" with points pointtype 2 linecolor rgb "blue" title "interpolation nodes"

set title "Integral of linear interpolation, sin(x)"
set key at 7.9,1.7
plot [0:9][-0.1:2.1] \
-cos(x)+1 with lines linetype 7 linecolor rgb "black" title "-cos(x)+1"\
, "Out.linspline.sin.data" using 1:3 with lines dashtype 7 linecolor rgb "red" title "integral of interpolation"\

unset multiplot 
