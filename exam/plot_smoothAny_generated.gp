set terminal svg background "white" dynamic size 600,750
set output outputname
set xlabel "x"                  
set ylabel "signal"
set title maintitle
set multiplot layout 2,1 columns
set grid

#get the lambda value (smoothing parameter)
lambda_a=system(sprintf("awk 'NR==1 {print $4}' %s", inputname))

#polynom
set key box top left
plot [-2.5: 2.5][] \
inputname index 0 every ::1 using 1:2 linecolor rgb "black" lt 7 ps 0.3 title "clean polynom signal"\
, inputname index 0 every ::1 using 1:3 linecolor rgb "red" lt 2 ps 0.5 title "noisy signal"\
, inputname index 0 every ::1 using 1:4 linecolor rgb "blue" lt 7 ps 0.3 title sprintf(lambda_a)\

#sin wave
set key box top right
unset title
plot [][] \
inputname index 1 every ::1 using 1:2 linecolor rgb "black" lt 7 ps 0.3 title "clean sin signal"\
, inputname index 1 every ::1 using 1:3 linecolor rgb "red" lt 2 ps 0.5 title "noisy signal"\
, inputname index 1 every ::1 using 1:4 linecolor rgb "blue" lt 7 ps 0.3 title sprintf(lambda_a)\
