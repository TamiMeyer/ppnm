set terminal svg background "white" dynamic size 800,800
set output outputname
set grid
set multiplot layout 3,1 columns title "ANN (with 6 hidden neurons) trained to approximate g(x)=Cos(5*x-1)*Exp(-x*x)"
set key outside

f(x) = -5*sin(5*x-1)*exp(-x*x)+cos(5*x-1)*(-2*x)*exp(-x*x) #first derivative of approximated function
ff(x) = -25*cos(5*x-1)*exp(-x*x)-5*sin(5*x-1)*2*x*exp(-x*x)-5*sin(5*x-1)*(-2*x)*exp(-x*x)+cos(5*x-1)*(-2)*exp(-x*x)+cos(5*x-1)*(-2*x)*(-2*x)*exp(-x*x)
#FI(x) = (sqrt(pi)*(cos(1)*erf((-5*I + 2*x)/2) + cos(1)*erf((5*I + 2*x)/2) - (erfi(5/2 - I*x) + erfi(5/2 + I*x))*sin(1)))/(4*exp(25/4)) 

set title "First derivative"
set ylabel "g'(x)"
set xlabel "x"
plot [][-5:5.5] \
 "Out.derivative.data" index 0 with lines linecolor rgb "red" title "ANN first derivative", \
 f(x) with lines dashtype 3 linecolor rgb "black" lw 2  title "analytic first derivative"

set title "Second derivative"
set ylabel "g''(x)"
set xlabel "x"
plot [][-30:30] \
 "Out.derivative.data" index 1 with lines linecolor rgb "red" title "ANN second derivative", \
 ff(x) with lines dashtype 3 linecolor rgb "black" lw 2  title "analytic second derivative"

set title "Anti-derivative"
set ylabel "∫_{-1}^{z} g(x)dx"
set xlabel "z"
plot [][] \
 "Out.derivative.data" index 2 with lines linecolor rgb "red" title "ANN anti-derivative", \
 "Out.spline_antiderivative.data" every ::1 with lines dashtype 3 linecolor rgb "blue" lw 2  title "cspline anti-derivative"
 #FI(x) with lines dashtype 3 linecolor rgb "black" lw 2  title "analytic integral"
