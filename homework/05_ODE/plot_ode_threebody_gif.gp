set terminal gif animate delay 4 optimize
set output 'Out.animation.gif'

set xlabel "X Position"
set ylabel "Y Position"
set xrange [-1.3:1.3]
set yrange [-0.4:0.4]

filedata = 'Out.threebody.data'
n = system(sprintf('cat %s | wc -l', filedata)) #counts the number of lines 



do for [j=1:n] {
    plot filedata using 8:9 w l lw 2 linecolor "black", \
        filedata u 8:9 every ::j::j w p pt 7 ps 2 lc "blue" notitle, \
        filedata u 10:11 every ::j::j w p pt 7 ps 2 lc "blue" notitle, \
        filedata u 12:13 every ::j::j w p pt 7 ps 2 lc "blue" notitle
    
    set label "time t = "
}


#plotFrame(i) = sprintf("< awk 'NR==%d {print $8, $9}' | tail -n +2 Out.threebody.data", i)
#
#do for [i=1:5] {
#    plot plotFrame(i) with points title sprintf("Frame %d", i)
#}