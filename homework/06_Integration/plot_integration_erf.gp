set terminal svg background "white"
set output outputname
set xlabel "x"                  
set ylabel "erf(x)"
set xzeroaxis                   #Draws the x-axis line through the origin (x = 0)
set yzeroaxis                   #Draws the y-axis line through the origin (y = 0)
set title "Error function"
set key bottom right

plot [][] \
"Out.integ_erf.data" with lines linestyle 2 linecolor rgb "red" title "integrator implemented error function", \
"Out.singleprec_erf.data" with lines dashtype 7 linecolor rgb "blue" title "single precision error function", \
"tabulated_erf.data" with points pointtype 4 linecolor rgb "forest-green" title "tabulated erf (wikipedia)", \
"tabulated_erf.data" using (-($1)):(-($2)) with points pointtype 4 linecolor rgb "forest-green" notitle, \


