set terminal svg background "white"
set output outputname
set key bottom right
set xlabel "x"                  
set ylabel "signal"
set title "TITLE"

#get the lambda values (smoothing parameters)
lambda_a=system("awk 'NR==1 {print $4}' Out.smoothsignal_LUeff_500.data")

plot [][] \
"Out.smoothsignal_LUeff_15000.data" using 1:2 linecolor rgb "red" lt 2 ps 0.5 title "noisy signal"\
, "Out.smoothsignal_LUeff_15000.data" using 1:3 linecolor rgb "blue" lt 7 ps 0.3 title sprintf(lambda_a)\
