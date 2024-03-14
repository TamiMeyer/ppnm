set terminal svg background "white"
set output outputname
set xlabel "x"                  
set ylabel "y" 
set title "Solutions of the equation of equatorial motion, u''(φ) + u(φ) = 1 + εu(φ)^2"
set key box below
set size ratio -1
set grid
set bmargin at screen 0.25

plot [-1.5:1.5][-1:2] \
 "Out.planet.circular.data" using (1/$2)*cos($1):(1/$2)*sin($1) with lines linestyle 2 linecolor rgb "red" title "ε=0, u(0)=1, u'(0)=0 (Newtonian circular motion)", \
 "Out.planet.elliptical.data" using (1/$2)*cos($1):(1/$2)*sin($1) with lines linestyle 2 linecolor rgb "blue" title "ε=0, u(0)=1, u'(0)=-0.5  (Newtonian elliptical motion)", \
 "Out.planet.precession.data" using (1/$2)*cos($1):(1/$2)*sin($1) with lines linestyle 2 linecolor rgb "forest-green" title "ε=0.01, u(0)=1, u'(0)=-0.5 (Relativistic precession)", \
