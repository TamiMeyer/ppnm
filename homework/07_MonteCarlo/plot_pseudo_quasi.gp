set terminal svg background "white" size 800,800
set key top right
set output outputname
set xlabel "N"                  
set ylabel "error"

set multiplot layout 2,1 columns


f1(x) = a1 + b1 * x**c1
a1=1;b1=1;c1=-0.5
fit f1(x) "Out.circle_pseudo.data" using 1:4 via a1, b1, c1
g1(x) = d1 + e1 * x**f1
d1=1;e1=1;f1=-0.5
fit g1(x) "Out.circle_quasi.data" using 1:4 via d1, e1, f1

f(x) = a + b * x**c
a=1;b=1;c=-0.5
fit f(x) "Out.ellipse_pseudo.data" using 1:4 via a, b, c
g(x) = d + e * x**f
d=1;e=1;f=-0.5
fit g(x) "Out.ellipse_quasi.data" using 1:4 via d, e, f

set title "Errors of pseudo and quasi random M.C. integration - area of circle"
plot [0:50000][0:0.06] \
 "Out.circle_pseudo.data" using 1:4 with points pointtype 1 linecolor "navy" title "pseudo, actual error"\
 , "Out.circle_quasi.data" using 1:4 with points pointtype 1 linecolor "dark-red" title "quasi, actual error"\
 , "Out.circle_pseudo.data" using 1:3 with points pointtype 2 linecolor "royalblue" title "pseudo, estimated error"\
 , "Out.circle_quasi.data" using 1:3 with points pointtype 2 linecolor "light-coral" title "quasi, estimated error"\
 , f1(x) title sprintf("pseudo, actual error fit with %.2f+%.2f*N**%.2f", a1, b1, c1) lw 2 linecolor "forest-green"\
 , g1(x) title sprintf("quasi, actual error fit with %.2f+%.2f*N**%.2f",d1, e1, f1) lw 2 linecolor "orange"

set title "Errors of pseudo and quasi random M.C. integration - area of ellipse"
plot [0:50000][0:0.06] \
 "Out.ellipse_pseudo.data" using 1:4 with points pointtype 1 linecolor "navy" title "pseudo, actual error"\
 , "Out.ellipse_quasi.data" using 1:4 with points pointtype 1 linecolor "dark-red" title "quasi, actual error"\
 , "Out.ellipse_pseudo.data" using 1:3 with points pointtype 2 linecolor "royalblue" title "pseudo, estimated error"\
 , "Out.ellipse_quasi.data" using 1:3 with points pointtype 2 linecolor "light-coral" title "quasi, estimated error"\
 , f(x) title sprintf("pseudo, actual error fit with %.2f+%.2f*N**%.2f", a, b, c) lw 2 linecolor "forest-green"\
 , g(x) title sprintf("quasi, actual error fit with %.2f+%.2f*N**%.2f",d, e, f) lw 2 linecolor "orange"

