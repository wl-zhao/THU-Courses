Lx = 10
Ly = 10
M = 20
N = 20
g_c = 9.81
n1=1.00
n2=1.34
x = 5000  # 风区
phi = 1  # 风向
U_10 = 10  # 海面10m高处的风速
x1 = g_c * x / U_10 ** 2  # 无因次风区
omega_0 = 22 * x1 ** (-0.33)  # 峰频率
alpha = 0.076 * x1 ** (-0.22)  # 无因次常数
gamma = 1.7  # 峰升高因子

MAX_RANGE = 1000000
TIME_STEP = 0.1
CALC_TIME = 100
# initial variables
EMIT_ANGLE = 0

# method
MONTECARLO = True
