set terminal svg background "white"
set output outputname
set grid
set ylabel "E0"

set multiplot layout 2,2 columns title "Convergence of E0 (with Shooting method) towards exact result "
unset title
unset key

set xlabel "rmax"                  
plot [][] \
 "Out.hydrogen_convergence.data" index 0 using 1:2 every ::1 with points linestyle 2 linecolor rgb "red"title "Shooting method", \
 -0.5 with lines dashtype 3 linecolor rgb "black" lw 2  title "exact"

set xtics 0.1
set xlabel "rmin"                  
plot [][] \
 "Out.hydrogen_convergence.data" index 1 using 1:2 every ::1 with points linestyle 2 linecolor rgb "red"title "Shooting method", \
 -0.5 with lines dashtype 3 linecolor rgb "black" lw 2  title "exact"

set xtics 0.01
set xlabel "acc"                  
plot [][:-0.49995] \
 "Out.hydrogen_convergence.data" index 2 using 1:2 every ::1 with points linestyle 2 linecolor rgb "red"title "Shooting method", \
 -0.5 with lines dashtype 3 linecolor rgb "black" lw 2  title "exact"

set bmargin at screen 0.25
set key below box
set xlabel "eps"                  
plot [][:-0.49995] \
 "Out.hydrogen_convergence.data" index 3 using 1:2 every ::1 with points linestyle 2 linecolor rgb "red"title "Shooting method", \
 -0.5 with lines dashtype 3 linecolor rgb "black" lw 2  title "exact"
 