set terminal svg background "white"
set output outputname
set xlabel "time t"                  
set ylabel "u(t) or u'(t)"
set title "Solutions of u''=-u for different initial conditions"
set key top right

plot [][-1.25:1.75] \
 "Out.harm1.data" using 1:2 with lines linestyle 2 linecolor rgb "red"title "u(t) with initial conditions u(0)=1, u''(0)=0", \
 "Out.harm1.data" using 1:3 with lines dashtype 7 linecolor rgb "red" title "u'(t) with initial conditions u(0)=1, u''(0)=0", \
 "Out.harm2.data" using 1:2 with lines linestyle 2 linecolor rgb "blue"title "u(t) with initial conditions u(0)=0.7, u''(0)=0.7", \
 "Out.harm2.data" using 1:3 with lines dashtype 7 linecolor rgb "blue"  title "u'(t) with initial conditions u(0)=0.7, u''(0)=0.7", \
 cos(x) with lines dashtype 3 linecolor rgb "black" lw 2  title "cos(t)"