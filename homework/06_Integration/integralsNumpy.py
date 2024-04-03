import numpy as np
from scipy import integrate

def f(x):
    global ncallsF
    ncallsF += 1;
    return 1/np.sqrt(x)

def g(x):
    global ncallsG
    ncallsG += 1;
    return np.log(x)/np.sqrt(x)

file = open("Out.pythonIntegrals.txt", "w")

ncallsF = 0
I_py = integrate.quad(f,0,1,epsabs=1e-4,epsrel=1e-4)
file.write("Calculate ∫₀¹ dx 1/√(x) with python integration routines: \n")
file.write(f"{I_py[0]}\n")
file.write("number of calls:\n")
file.write(f"{ncallsF}\n\n")

ncallsG = 0
I_py = integrate.quad(g,0,1,epsabs=1e-4,epsrel=1e-4)
file.write("Calculate ∫₀¹ dx ln(x)/√(x) with python integration routines: \n")
file.write(f"{I_py[0]}\n")
file.write("number of calls:\n")
file.write(f"{ncallsG}\n")

file.close()