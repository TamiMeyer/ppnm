set terminal svg background "white"
set output outputname
set xlabel "x"                  
set ylabel "y" 
set title "Newtonian gravitational three-body problem"
unset key
set size ratio -1
set grid

plot [][] \
 "Out.threebody.data" using 8:9 with lines linestyle 2 linecolor rgb "red" title "x1:y1", \
 "Out.threebody.data" using 10:11 with lines linestyle 2 linecolor rgb "red" title "x2:y2", \
 "Out.threebody.data" using 12:13 with lines linestyle 2 linecolor rgb "red" title "x3:y3", \

