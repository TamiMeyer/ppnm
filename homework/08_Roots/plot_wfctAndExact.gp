set terminal svg background "white"
set output outputname
set xlabel "r"                  
set ylabel "f0"
set key top right
set grid

parameters=system("awk 'NR==1' Out.hydrogen_swave.data")
set title sprintf("Lowest wavefunction (%s)", parameters)

plot [][] \
 "Out.hydrogen_swave.data" using 1:2 every ::1 with lines linestyle 2 linecolor rgb "red"title "Shooting method", \
 x*exp(-x) with lines dashtype 3 linecolor rgb "black" lw 2  title "exact (r*e^-r)"
 