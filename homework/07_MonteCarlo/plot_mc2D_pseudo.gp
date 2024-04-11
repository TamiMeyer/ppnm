set terminal svg background "white"
set key box top right
set output outputname
set xlabel "N"                  
set ylabel "error"

set multiplot layout 2,1 columns

f(x) = a + b * x**c
a=1;b=1;c=-0.5
fit f(x) "Out.circle_pseudo.data" using 1:4 via a, b
g(x) = d + e * x**f
d=1;e=1;f=-0.5
fit g(x) "Out.ellipse_pseudo.data" using 1:4 via d, e

set title "Errors of pseudo-random M.C. integration - area of unit circle"
plot [0:50000][0:0.1] \
 "Out.circle_pseudo.data" using 1:4 with points pointtype 2 title "actual error"\
 , "Out.circle_pseudo.data" using 1:3 with points pointtype 2 title "estimated error"\
 , f(x) title sprintf("actual error fit with %.2f+%.2f*N**%.2f", a, b, c) lw 2

set title "Errors of pseudo-random M.C. integration - area of ellipse"
plot [0:50000][0:0.1] \
 "Out.ellipse_pseudo.data" using 1:4 with points pointtype 2 title "actual error"\
 , "Out.ellipse_pseudo.data" using 1:3 with points pointtype 2 title "estimated error"\
 , g(x) title sprintf("actual error fit with %.2f+%.2f*N**%.2f", d, e, f) lw 2