set terminal svg background "white"
set output outputname
set xlabel "time t"                  
#set ylabel "u(t) or u'(t)"
set title "Solutions of oscillator with friction (theta''(t) + b*theta'(t) + c*sin(theta(t)) = 0), with b=0.25 and c=5.0"
set key box bottom right
set grid
set ytics ( -4, -2, 0, 2 )

plot [][-4.5:3.5] \
 "Out.oscfric.data" using 1:2 with lines linestyle 2 lw 2 linecolor rgb "blue" title "theta(t)", \
 "Out.oscfric.data" using 1:3 with lines dashtype 7 lw 2 linecolor rgb "forest-green" title "omega(t)", \
