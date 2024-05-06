set terminal svg background "white" dynamic size 1200,800
set output outputname
set xlabel "x"                  
set ylabel "y"
unset key
set multiplot layout 3,3 columns

set title "Quadratic interpolation, y=4"
plot [0:4][0:16] \
"Out.qspline.const.data" using 1:2 with lines dashtype 7 linecolor rgb "red" title "qspline"\
, "Out.raw.const.data" with points pointtype 2 linecolor rgb "blue" title "interpolation nodes"
unset title
plot [0:4][-1:16] \
"Out.qspline.const.data" using 1:3 with lines dashtype 7 linecolor rgb "red" title "integral of qspline"
plot [0:4][-1:16] \
"Out.qspline.const.data" using 1:4 with lines dashtype 7 linecolor rgb "red" title "derivative of qspline"\

set ytics
set format y ''
unset ylabel

set title "Quadratic interpolation, y=4x"
plot [0:4][0:16] \
"Out.qspline.xlin.data" using 1:2 with lines dashtype 7 linecolor rgb "red" title "qspline"\
, "Out.raw.xlin.data" with points pointtype 2 linecolor rgb "blue" title "interpolation nodes"
unset title
plot [0:4][-1:16] \
"Out.qspline.xlin.data" using 1:3 with lines dashtype 7 linecolor rgb "red" title "integral of qspline"
plot [0:4][-1:16] \
"Out.qspline.xlin.data" using 1:4 with lines dashtype 7 linecolor rgb "red" title "derivative of qspline"\

set key outside

set title "Quadratic interpolation, y=x^2"
plot [0:4][-1:16] \
x*x with lines linetype 7 linecolor rgb "black" title "x^2"\
,"Out.qspline.xsquared.data" using 1:2 with lines dashtype 7 linecolor rgb "red" title "qspline"\
, "Out.raw.xsquared.data" with points pointtype 2 linecolor rgb "blue" title "interpolation nodes"
unset title
plot [0:4][-1:16] \
x*x*x/3 with lines linetype 7 linecolor rgb "black" title "1/3*x^3"\
, "Out.qspline.xsquared.data" using 1:3 with lines dashtype 7 linecolor rgb "red" title "integral of qspline"
plot [0:4][-1:16] \
"Out.qspline.xsquared.data" using 1:4 with lines dashtype 7 linecolor rgb "red" title "derivative of qspline"\

unset multiplot 
