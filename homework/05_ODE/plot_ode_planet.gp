set terminal svg background "white"
set output outputname
set xlabel "x" #"azimuthal angle φ"                  
set ylabel "y" #"u(φ)"
set title "Solutions of the equation of equatorial motion, u''(φ) + u(φ) = 1 + εu(φ)^2"
set key top right

plot [-1:1][-1:1] \
 "Out.planet.circular.data" using (1/$2)*cos($1):(1/$2)*sin($1) with lines linestyle 2 linecolor rgb "red" title "ε=0, u(0)=1, u'(0)=0 (Newtonian circular motion)", \
# cos(x) with lines dashtype 3 linecolor rgb "black" lw 2  title "cos(t)"
