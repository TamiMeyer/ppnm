set terminal svg background "white"
#set key top left
set output outputname
set xlabel "Time t [days]"                  
set ylabel "Activity y of ThX [relative units]"
set title "Radioactive ThX decay"

b=system("awk 'NR==2 {print $1}' Out.experiment_fitted.data")
lambda = system("awk 'NR==2 {print $2}' Out.experiment_fitted.data")

f(x) = exp(b-lambda*x)

plot [][] \
 "experiment.data" using 1:2:3 every ::1 with yerrorbars linecolor rgb "blue" title "experiment"\
 , f(x) with lines linetype 7 title sprintf("least-square fit f(t)=exp(%s-%s*t)", b, lambda)\