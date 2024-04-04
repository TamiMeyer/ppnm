import numpy as np
from scipy import integrate

file = open("Out.pythonIntegrals.txt", "w")
def f(x):
    global ncallsF
    ncallsF += 1;
    return 1/np.sqrt(x)
ncallsF = 0
I_py = integrate.quad(f,0,1,epsabs=1e-4,epsrel=1e-4)
file.write("Calculate ∫₀¹ dx 1/√(x) with python integration routines: \n")
file.write(f"{I_py[0]}\n")
file.write("number of calls:\n")
file.write(f"{ncallsF}\n\n")

def g(x):
    global ncallsG
    ncallsG += 1;
    return np.log(x)/np.sqrt(x)
ncallsG = 0
I_py = integrate.quad(g,0,1,epsabs=1e-4,epsrel=1e-4)
file.write("Calculate ∫₀¹ dx ln(x)/√(x) with python integration routines: \n")
file.write(f"{I_py[0]}\n")
file.write("number of calls:\n")
file.write(f"{ncallsG}\n\n")

def h1(x):
    global ncallsh1
    ncallsh1 += 1;
    return 1.0/(x*x)
ncallsh1 = 0
I_py = integrate.quad(h1,1,np.Infinity,epsabs=1e-4,epsrel=1e-4)
file.write("Calculate ∫1,∞ dx 1/(x^2) with python integration routines: \n")
file.write(f"{I_py[0]}\n")
file.write("number of calls:\n")
file.write(f"{ncallsh1}\n\n")

def h2(x):
    global ncallsh2
    ncallsh2 += 1;
    return 1.0/(x*x*x)
ncallsh2 = 0
I_py = integrate.quad(h2,1,np.Infinity,epsabs=1e-4,epsrel=1e-4)
file.write("Calculate ∫1,∞ dx 1/(x^3) with python integration routines: \n")
file.write(f"{I_py[0]}\n")
file.write("number of calls:\n")
file.write(f"{ncallsh2}\n\n")

def h3(x):
    global ncallsh3
    ncallsh3 += 1;
    return 1.0/((1+x)*np.sqrt(x))
ncallsh3 = 0
I_py = integrate.quad(h3,0,np.Infinity,epsabs=1e-4,epsrel=1e-4)
file.write("Calculate ∫₀∞ dx 1/((1+x)√(x)) with python integration routines: \n")
file.write(f"{I_py[0]}\n")
file.write("number of calls:\n")
file.write(f"{ncallsh3}\n\n")

def h4(x):
    global ncallsh4
    ncallsh4 += 1;
    return np.exp(-x*x)
ncallsh4 = 0
I_py = integrate.quad(h4,-np.Infinity,np.Infinity,epsabs=1e-4,epsrel=1e-4)
file.write("Calculate ∫-∞,∞ dx exp(-x^2) with python integration routines: \n")
file.write(f"{I_py[0]}\n")
file.write("number of calls:\n")
file.write(f"{ncallsh4}\n\n")

def h5(x):
    global ncallsh5
    ncallsh5 += 1;
    return np.exp(x)
ncallsh5 = 0
I_py = integrate.quad(h5,-np.Infinity,0,epsabs=1e-4,epsrel=1e-4)
file.write("Calculate ∫-∞,0 dx exp(x) with python integration routines: \n")
file.write(f"{I_py[0]}\n")
file.write("number of calls:\n")
file.write(f"{ncallsh5}\n")

file.close()