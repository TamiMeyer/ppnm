set terminal svg background "white"
set output outputname
set xlabel "time t"                  
set ylabel "u(t) or u'(t)"
set title "Solutions of pendulum equation u''=-sin(u)"
set key top right

plot [][-1.25:1.75] \
 "Out.pend1.data" using 1:2 with lines linestyle 2 linecolor rgb "red"title "u(t) with initial conditions u(0)=1, u''(0)=0", \
 "Out.pend1.data" using 1:3 with lines dashtype 7 linecolor rgb "red" title "u'(t) with initial conditions u(0)=1, u''(0)=0", \