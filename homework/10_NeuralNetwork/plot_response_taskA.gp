set terminal svg background "white"
set output outputname
set grid
set ylabel "g"
set xlabel "x"
set title "ANN (with 6 hidden neurons) trained to approximate g(x)=Cos(5*x-1)*Exp(-x*x)"
set key bottom right

f(x) = cos(5*x-1)*exp(-x*x)

plot [][] \
 "Out.train.data" with points linecolor rgb "red" title "training data", \
 f(x) with lines dashtype 3 linecolor rgb "black" lw 2  title "g(x)=Cos(5*x-1)*Exp(-x*x)", \
 "Out.response.data" with lines linecolor rgb "red" title "response"

 
