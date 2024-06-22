set terminal svg background "white" dynamic size 600,750
set key top left
set output outputname
set ylabel "time [s]"
set multiplot layout 3,1 columns
set key box left
set grid

unset xlabel

#cubic fit of smoothQR timing
f(x) = a*x**3+b
fit f(x) "Out.timesSmooth.data" index 0 using 1:2 via a,b
#linear fit of smoothLU_eff timing
g(x) = m*x +c
fit g(x) "Out.timesSmooth.data" index 2 using 1:2 via m,c

set title "QR-smoothing (small dataset)"
plot [][] \
 "Out.timesSmooth.data" index 0 every ::1 with points pointtype 7 title "smoothQR"\
 , f(x) title sprintf("cubic fit a*N^3+b with a=%f and b=%f", a, b)

set ytics 0.01
set title "Efficient LU-smoothing (small dataset)"
plot [][] \
 "Out.timesSmooth.data" index 1 every ::1 with points pointtype 7 title "smoothLU\\_eff"\

set ytics auto
set xlabel "# of datapoints N"                  
set title "Efficient LU-smoothing (large dataset)"
plot [][] \
 "Out.timesSmooth.data" index 2 every ::1 with points pointtype 7 title "smoothLU\\_eff"\
 , g(x) title sprintf("linear fit m*N+c with m=%f and c=%f", m, c)


