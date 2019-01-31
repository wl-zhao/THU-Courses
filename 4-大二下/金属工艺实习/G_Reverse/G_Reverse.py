
# coding: utf-8
# author: John Williams

import re
# define the G_line class which is a line in G code file
class G_line(object):
    def __init__(self, type1, X, Z, F, R = None):
        self.type = type1
        if type1 is 2 or type1 is 3:
            self.R = R
        else:
            self.R = None
        self.X, self.Z, self.F = X, Z, F
        
    def __str__(self):
        if self.R is None:
            return('G%02d X%.2f Z%.2f F%.2f'%(self.type, self.X, self.Z, self.F))
        else:
            return('G%02d X%.2f Z%.2f R%.2f F%.2f'%(self.type, self.X, self.Z, self.R, self.F))    
        
    def getReverse(self, initial_X, initial_Z):
        new_type = self.type
        if self.type is 2 or self.type is 3:
            new_type = 5 - self.type
            return G_line(new_type, initial_X, initial_Z, self.F, self.R)
        else:
            return G_line(new_type, initial_X, initial_Z, self.F)

# get initial position
float_pattern = '-?([1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|\d+)'
initial_X, initial_Z = 0, 0
while(True):
    str1 = input('Input initial X and Z: use \',\' to seperate: ')
    try:
        initial_X, initial_Z = re.findall(float_pattern, str1)
    except ValueError:
        print('Wrong Format!')
    else:
        break
initial_X, initial_Z = float(initial_X), float(initial_Z)
print('X =', initial_X, ', Z =', initial_Z)

# read G code file
G_file = open('input.txt')
G_list = []
for line in G_file:
    m_G = re.search('G\d+', line)
    m_X = re.search('X' + float_pattern, line)
    m_Z = re.search('Z' + float_pattern, line)        
    m_F = re.search('F' + float_pattern, line)
    m_R = re.search('R' + float_pattern, line)
    try:
        if m_R is None:
            R = None
        else:
            R = float(m_R.group()[1:])
        G = int(m_G.group()[1:])
        X, Z, F = float(m_X.group()[1:]), float(m_Z.group()[1:]), float(m_F.group()[1:])
        new_G = G_line(G, X, Z, F, R)
        G_list.append(new_G.getReverse(initial_X, initial_Z))
        initial_X, initial_Z = X, Z
    except AttributeError:
        print('Wrong Format of G Code!')
G_file.close()
G_list.reverse()

# write processed code to output.txt
with open('output.txt', 'w') as f:
    i = 0
    for G in G_list:
        i += 1
        f.write('N' + str(i)+ '0 ')
        f.write(str(G) + '\n')
f.close()

