set terminal svg background "white"
set output outputname
set xlabel "x"                  
set ylabel "y"
set multiplot layout 2,1 columns

builtin_spline_data = "Out.builtin_cspline.sin.data" # Define the filename for the output of Gnuplot's built-in spline interpolation
set table builtin_spline_data
    plot "Out.raw.sin.data" smooth cspline
unset table

set key at 6.5,1.5
set title "Comparison of cubic splines, sin(x)"
plot [0:9][] \
sin(x) with lines linetype 7 linecolor rgb "black" title "sin(x)"\
,"Out.cspline.sin.data" using 1:2 with lines dashtype 7 linecolor rgb "red" title "My cubic spline implementation"\
, builtin_spline_data using 1:2 with lines dashtype 5 linecolor rgb "green" title "Gnuplot's built-in cubic spline"\
, "Out.raw.sin.data" with points pointtype 2 linecolor rgb "blue" title "interpolation nodes"

set key at 7,0.02
set title "Difference of cubic splines to sin(x)"
plot [0:9][] \
"Out.cspline.sin.data" using 1:(sin($1) - $2) with lines dashtype 7 linecolor rgb "red" title "Difference of my cubic spline implementation to sin(x)"\
, builtin_spline_data using 1:(sin($1) - $2) with lines dashtype 5 linecolor rgb "green" title "Difference of Gnuplot's built-in cubic spline to sin(x)"

unset multiplot 
