set terminal png                #plot will be saved as png
set key bottom right            #positions the legend
set output "OutLngamma.png"         #specifies the output file name
set xlabel "x"                  
set ylabel "y"
#set tics out                   #Draws the tics outside the plot area
set xzeroaxis                   #Draws the x-axis line through the origin (x = 0)
set yzeroaxis                   #Draws the y-axis line through the origin (y = 0)
set samples 800                 # 800 points will be sampled along the function
set title "Gamma function"
plot [0:5][:5] \
 "lngamma.data" with lines linetype 1 title "gamma"\
 