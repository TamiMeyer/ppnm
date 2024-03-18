set terminal gif animate delay 4 optimize
set output outputname
set title "Newtonian gravitational three-body problem"
set xlabel "X Position"
set ylabel "Y Position"
set xrange [-1.3:1.3]
set yrange [-0.5:0.5]
set size ratio -1
set key top left

filedata = 'Out.threebody.data'
n = system(sprintf('cat %s | wc -l', filedata)) #counts the number of lines 

do for [j=1:n-1] {
    TIME =system(sprintf("awk 'NR== %d {printf \"%%.3f\", $1}' %s", j, filedata))
    plot filedata using 8:9 w l lw 2 linecolor "black" notitle, \
        keyentry w points ps 0 title sprintf("Time t = %s",TIME), \
        filedata u 8:9 every ::j::j w p pt 7 ps 2 lc "blue" notitle, \
        filedata u 10:11 every ::j::j w p pt 7 ps 2 lc "blue" notitle, \
        filedata u 12:13 every ::j::j w p pt 7 ps 2 lc "blue" notitle
}