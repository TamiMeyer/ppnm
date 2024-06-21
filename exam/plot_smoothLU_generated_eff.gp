set terminal svg background "white"
set output outputname
set key bottom right
set xlabel "x"                  
set ylabel "signal"
set title "randomly generated signal smoothened using efficient LU-factorization"

#get the lambda values (smoothing parameters)
lambda_a=system("awk 'NR==1 {print $4}' Out.smoothsignalLU_generated_eff.data")

plot [-2.5:2.5][] \
"Out.smoothsignalLU_generated_eff.data" using 1:2 linecolor rgb "black" lt 7 ps 0.3 title "clean signal"\
, "Out.smoothsignalLU_generated_eff.data" using 1:3 linecolor rgb "red" lt 2 ps 0.5 title "noisy signal"\
, "Out.smoothsignalLU_generated_eff.data" using 1:4 linecolor rgb "blue" lt 7 ps 0.3 title sprintf(lambda_a)\
