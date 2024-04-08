set terminal svg background "white"
set key top right
set output outputname
set xlabel "N"                  
set ylabel "error"

set multiplot layout 2,1 columns

f(x) = a/sqrt(x)
fit f(x) "Out.circle_pseudo.data" using 1:4 via a
g(x) = b/sqrt(x)
fit g(x) "Out.ellipse_pseudo.data" using 1:4 via b

set title "Errors of pseudo-random M.C. integration - area of unit circle"
plot [0:50000][0:0.1] \
 "Out.circle_pseudo.data" using 1:4 with points pointtype 2 title "actual error"\
 , "Out.circle_pseudo.data" using 1:3 with points pointtype 2 title "estimated error"\
 , f(x) title sprintf("fit with %.2f/Sqrt(N)", a) lw 2

set title "Errors of pseudo-random M.C. integration - area of ellipse"
plot [0:50000][0:0.1] \
 "Out.ellipse_pseudo.data" using 1:4 with points pointtype 2 title "actual error"\
 , "Out.ellipse_pseudo.data" using 1:3 with points pointtype 2 title "estimated error"\
 , g(x) title sprintf("fit with %.2f/Sqrt(N)", b) lw 2