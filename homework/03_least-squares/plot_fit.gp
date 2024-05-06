set terminal svg background "white"
#set key top left
set output outputname
set xlabel "Time t [days]"                  
set ylabel "Activity y of ThX [relative units]"
set title "Radioactive ThX decay"

lna=system("awk 'NR==2 {print $1}' Out.experiment_fitted.data")
lambda = system("awk 'NR==2 {print $2}' Out.experiment_fitted.data")
d_lna = system("awk 'NR==4 {print $1}' Out.experiment_fitted.data")
d_lambda= system("awk 'NR==4 {print $2}' Out.experiment_fitted.data")

f(x) = exp(lna-lambda*x)
f_m_p(x) = exp(lna-d_lna-(lambda+d_lambda)*x) #minimum values within the uncertainties
f_p_m(x) = exp(lna+d_lna-(lambda-d_lambda)*x) #maximum values within the uncertainties
f_plus_d_lna(x) = exp(lna+d_lna-lambda*x)
f_minus_d_lna(x) = exp(lna-d_lna-lambda*x)
f_plus_d_lambda(x) = exp(lna-(lambda+d_lambda)*x)
f_minus_d_lambda(x) = exp(lna-(lambda-d_lambda)*x)


plot [0:16][] \
 '+' using 1:(f_m_p($1)):(f_p_m($1)) with filledcurves linecolor rgb "grey90" title "range of fit within the uncertainty of the fitting coefficients"\
 , f(x) with lines linetype 7 linecolor rgb "black" lw 2 title sprintf("least-square fit f(t)=exp(lna-λ*t) with lna=%s and λ=%s", lna, lambda)\
 , f_plus_d_lna(x) with lines linetype 7 linecolor rgb "blue" title sprintf("with lna±d\\_lna")\
 , f_minus_d_lna(x) with lines linetype 7 linecolor rgb "blue" notitle\
 , f_plus_d_lambda(x) with lines linetype 7 linecolor rgb "red" title sprintf("with λ±dλ")\
 , f_minus_d_lambda(x) with lines linetype 7 linecolor rgb "red" notitle\
 , "experiment.data" using 1:2:3 every ::1 with yerrorbars linecolor rgb "black" lt 2 title "experiment"\


 
