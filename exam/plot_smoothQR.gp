set terminal svg background "white"
set output outputname
set key bottom right
set xlabel "x"                  
set ylabel "signal"
set title "signal smoothened using QR-decomposition"

#get the lambda values (smoothing parameters)
lambda_a=system("awk 'NR==1 {print $3}' Out.smoothsignalQR.data")
lambda_b=system("awk 'NR==1 {print $4}' Out.smoothsignalQR.data")
lambda_c=system("awk 'NR==1 {print $5}' Out.smoothsignalQR.data")

plot [][] \
"Out.smoothsignalQR.data" using 1:2 linecolor rgb "black" lt 2 title "noisy signal"\
, "Out.smoothsignalQR.data" using 1:3 linecolor rgb "red" lt 7 ps 0.5 title sprintf(lambda_a)\
, "Out.smoothsignalQR.data" using 1:4 linecolor rgb "green" lt 7 ps 0.5 title sprintf(lambda_b)\
, "Out.smoothsignalQR.data" using 1:5 linecolor rgb "blue" lt 7 ps 0.5 title sprintf(lambda_c)\
