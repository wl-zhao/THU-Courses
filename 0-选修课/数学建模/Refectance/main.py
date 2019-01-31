from global_var import *
from functions import *
import numpy as np
import matplotlib.pyplot as plt
import os
if __name__ == '__main__':
    angle = np.linspace(0, 10, 11)
    ratio = angle
    np.random.seed()
    # calc R for calm first
    for a in angle:
        print(a)
        R_calm = calc_R(a)
        print('calm: ', R_calm)
        # calc R for turbulence
        EMIT_ANGLE = a
        if MONTECARLO:
           R_turbu = monte_carlo()
        else:
           R_turbu = iterate()
        print('turbu', R_turbu)
        print('the ratio of turbulence R to calm R = ', R_turbu / R_calm)
        ratio[int(a)] = (R_turbu/R_calm)

    plt.figure()
    plt.plot(angle, ratio)
    plt.show()
