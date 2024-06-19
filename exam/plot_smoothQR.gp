set terminal svg background "white"
set output outputname
set key bottom right
set xlabel "x"                  
set ylabel "signal"
set title "signal smoothened using QR-decomposition"

#get the lambda values (smoothing parameters)
lambda_a=system("awk 'NR==1 {print $3}' Out.smoothsignal.data")
lambda_b=system("awk 'NR==1 {print $4}' Out.smoothsignal.data")
lambda_c=system("awk 'NR==1 {print $5}' Out.smoothsignal.data")

plot [][] \
"Out.smoothsignal.data" using 1:2 linecolor rgb "black" lt 2 title "noisy signal"\
, "Out.smoothsignal.data" using 1:3 linecolor rgb "red" lt 7 ps 0.5 title sprintf(lambda_a)\
, "Out.smoothsignal.data" using 1:4 linecolor rgb "green" lt 7 ps 0.5 title sprintf(lambda_b)\
, "Out.smoothsignal.data" using 1:5 linecolor rgb "blue" lt 7 ps 0.5 title sprintf(lambda_c)\
