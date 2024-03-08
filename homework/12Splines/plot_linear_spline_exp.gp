set terminal svg background "white"
set key top left
set output outputname
set xlabel "x"                  
set ylabel "y"
set title "Linear interpolation, exp(x)"
set multiplot layout 2,1 columns

plot [0:8][] \
exp(x) with lines linetype 7 linecolor rgb "black" title "exp(x)"\
,"Out.linspline.exp.data" using 1:2 with lines dashtype 7 linecolor rgb "red" title "linear spline"\
, "Out.raw.exp.data" with points pointtype 2 linecolor rgb "blue" title "interpolation nodes"

plot [0:8][] \
exp(x) with lines linetype 7 linecolor rgb "black" title "exp(x)"\
, "Out.linspline.exp.data" using 1:3 with lines dashtype 7 linecolor rgb "red" title "integral of interpolation"\

unset multiplot 
