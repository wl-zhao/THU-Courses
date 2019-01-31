from global_var import *
import numpy as np

def calc_i(theta, n):
    theta = theta / 180 * np.pi
    cos_i = np.abs(n[0] * np.cos(theta) + n[2] * np.sin(theta)) / np.sqrt(np.sum(np.square(n)))
    return np.arccos(cos_i)

def monte_carlo():
    sum = 0
    for i in range(CALC_TIME):
        t = np.random.rand(1, 1) * MAX_RANGE
        sum += whole_process(t)
    return sum / CALC_TIME

def iterate():
    t = 0
    sum = 0
    while t < MAX_RANGE:
        sum += whole_process(t)
        t += TIME_STEP
    return sum / (t / TIME_STEP)

# calc R from t
def whole_process(t):
    n = calc_normal(t)
    i = calc_i(EMIT_ANGLE, n)
    i = i * 180 / np.pi
    return calc_R(i)


def calc_R(theta):
    theta = theta / 180 * np.pi
    numerator_s = n1*np.cos(theta)-n2*np.sqrt(1-np.square(n1/n2*np.sin(theta)))
    denominator_s = n1*np.cos(theta)+n2*np.sqrt(1-np.square(n1/n2*np.sin(theta)))
    Rs = np.square(numerator_s/denominator_s)

    numerator_p = n1*np.sqrt(1-np.square(n1/n2*np.sin(theta)))-n2*np.cos(theta)
    denominator_p = n1*np.sqrt(1-np.square(n1/n2*np.sin(theta)))+n2*np.cos(theta)
    Rp = np.square(numerator_p/denominator_p)
    R=(Rs+Rp)/2
    return R

def calc_normal(t):
    mold_x = np.complex(0)
    for m_k in range(int(-M/2+1), int(M/2)):
        for n_k in range(int(-N/2+1), int(N/2)):
            m_k += 0.1
            n_k += 0.1
            mold_x += np.sqrt(calc_s(k_ml(m_k),k_nl(n_k))) * np.e**(calc_omega(m_k,n_k)*t*np.complex(0,1)) * np.random.rand(1,1) * k_ml(m_k)
    dfx = 2*np.pi/np.sqrt(Lx*Ly)*np.abs(mold_x)

    mold_y = complex(0)
    for m_k in range(int(-M/2+1), int(M/2)):
        for n_k in range(int(-N/2+1), int(N/2)):
            m_k += 0.1
            n_k += 0.1
            mold_y += np.sqrt(calc_s(k_ml(m_k),k_nl(n_k))) * np.e**(calc_omega(m_k,n_k)*t*np.complex(0,1)) * np.random.rand(1,1) * k_nl(n_k)
    dfy = 2*np.pi/np.sqrt(Lx*Ly)*np.abs(mold_y)
    return (-np.abs(dfx), -np.abs(dfy), 1)

def k_ml(m_k):
    return 2*np.pi*m_k/Lx

def k_nl(n_k):
    return 2*np.pi*n_k/Ly

# def calc_omega(k_m, k_n):
#  	k=np.sqrt(k_m ** 2+k_n ** 2)
#  	omega=np.sqrt(g_c*k)
#     return omega
def calc_omega(k_m, k_n):
    k = np.sqrt(k_m ** 2 + k_n ** 2)
    omega = np.sqrt(g_c * k)
    return omega


def calc_s(k_m,k_n):

    theta_0=np.arctan(k_n/k_m)
    omega = calc_omega(k_m, k_n)
    #峰型参数
    if omega <= omega_0:
        sigma = 0.07
    else:
        sigma = 0.09
# sigma = (omega <= omega_0) ? 0.07 : 0.09 #峰型参数
    S_omega=alpha*g_c/(omega**5)*np.exp(-5/4*(omega_0/omega) ** 4)*gamma ** (np.exp(-(omega-omega_0) ** 2/(2*sigma**2*omega_0**2)))#国际标准海谱
    if (np.abs(theta_0-phi)<2/np.pi):
        G = (2/np.pi)*np.cos(theta_0-phi) ** 2
    else:
        G = 0
    S=S_omega*G

    d_omega=g_c*np.pi/(omega*np.sqrt(k_m**2+k_n**2))*(k_m/Lx+k_n/Ly)
    d_theta=1/(1+(k_n/k_m)**2)*(2*np.pi/(Ly*k_m)-2*np.pi*k_n/(k_m**2*Lx))
    S_mn=S*Lx*Ly/(4*np.pi**2)*d_omega*d_theta
    S_mn = np.complex(S_mn)
    return S_mn

