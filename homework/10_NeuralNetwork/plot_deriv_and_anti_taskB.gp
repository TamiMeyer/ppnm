set terminal svg background "white"
set output outputname
set grid
set ylabel "g'"
set xlabel "x"
set title "ANN (with 6 hidden neurons) trained to approximate g(x)=Cos(5*x-1)*Exp(-x*x)"
set key top right

f(x) = -5*sin(5*x-1)*exp(-x*x)+cos(5*x-1)*(-2*x)*exp(-x*x)

plot [][] \
 "Out.derivative.data" with lines linecolor rgb "red" title "ANN derivative", \
 f(x) with lines dashtype 3 linecolor rgb "black" lw 2  title "analytic derivative"

 