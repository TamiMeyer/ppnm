set terminal svg background "white"
set key bottom right
set output "OutGamma.svg"
#set xlabel "x"                  
#set ylabel "y"
set xtics axis
set ytics axis
set zeroaxis linetype 1 linecolor rgb '#222222'
set samples 800
set title "Gamma function"
set arrow from 0, graph 0 to 0, graph 1 nohead dt 2 linecolor rgb "blue"
set arrow from -1, graph 0 to -1, graph 1 nohead dt 2 linecolor rgb "blue"
set arrow from -2, graph 0 to -2, graph 1 nohead dt 2 linecolor rgb "blue"
set arrow from -3, graph 0 to -3, graph 1 nohead dt 2 linecolor rgb "blue"
set arrow from -4, graph 0 to -4, graph 1 nohead dt 2 linecolor rgb "blue"
plot [-5:5][-5:5] \
 "gamma.data" with lines linetype 1 linecolor rgb "red" title "gamma"\
 ,"gammaTabulated.txt" with points pointtype 4 title "tabulated gamma"
