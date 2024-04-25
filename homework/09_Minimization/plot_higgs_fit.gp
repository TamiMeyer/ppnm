set terminal svg background "white"
set output outputname
set grid
set ylabel "signal σ(E) [certain units]"
set xlabel "energy E [GeV]"
set title "Higgs boson and fit with Breit-Wigner function F(E|m,Γ,A) = A/[(E-m)²+Γ²/4]"

#fitting parameters
m = 125.972230457689
gamma = 2.08539953182196
A = 9.87151662912582

#Breit-Wigner function
f(x) = A / (( x - m ) * ( x -m ) + gamma*gamma/4)

plot [][] \
 "experiment_Higgs.data" using 1:2:3 every ::1 with yerrorbars linecolor rgb "red" title "experiment", \
 f(x) with lines dashtype 3 linecolor rgb "black" lw 2  title sprintf("fit m =%.2f, Γ=%.3f, A=%.2f", m, gamma, A)
 
