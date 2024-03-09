set terminal svg background "white"
set output outputname
set xlabel "x"                  
set ylabel "y"
set title "Quadratic interpolation, sin(x)"
#set multiplot layout 2,1 columns

set key at 3,0
plot [0:9][] \
sin(x) with lines linetype 7 linecolor rgb "black" title "sin(x)"\
,"Out.qspline.sin.data" using 1:2 with lines dashtype 7 linecolor rgb "red" title "quadratic spline"\
, "Out.raw.sin.data" with points pointtype 2 linecolor rgb "blue" title "interpolation nodes"

#set key at 7.7,1.7
#plot [0:8][] \
#-cos(x)+1 with lines linetype 7 linecolor rgb "black" title "-cos(x)+1"\
#, "Out.linspline.sin.data" using 1:3 with lines dashtype 7 linecolor rgb "red" title "integral of interpolation"\

#unset multiplot 