from pathlib import Path
import math
    
base_path = Path(__file__).parent
path = (base_path / "../input/day08_input.txt").resolve()
#path = (base_path / "../input/day08_sample_input.txt").resolve()

with open(path) as f:
    lines = f.readlines()

instructions = lines[0].strip()

network = lines[2:]

network_dict = {} 

for n in network:
    node = n.split('=')[0].strip()
    options = n.split('=')[1].strip().strip(')').strip('(').split(',')
    network_dict[node] = (options[0].strip(), options[1].strip())

#print(network_dict)

#node = 'AAA'
steps = 0
instruction_index = 0


current_nodes = []

for node in network_dict:
    if node[-1] == 'A':
        current_nodes.append(node)

steps = []

for node in current_nodes:  
    step_count = 0      
    while node[-1] != 'Z':
        #print('start : ' + node)
        if instruction_index >= len(instructions):
            instruction_index = 0 
        
        if instructions[instruction_index] == 'L':
            node = network_dict[node][0] 
        elif instructions[instruction_index] == 'R': 
            node = network_dict[node][1] 
        
        #print('going : ' + instructions[instruction_index])
        #print('found : ' + node)
        step_count += 1
        instruction_index += 1 
        #print()
    steps.append(step_count)
    
def LCMofArray(a):
  lcm = a[0]
  for i in range(1,len(a)):
    lcm = lcm*a[i]//math.gcd(lcm, a[i])
  return lcm

print(steps)
print(LCMofArray(steps))



'''
keep_going = True
#force_stop = 0

while keep_going:
    if instruction_index >= len(instructions):
            instruction_index = 0 

    #print('going : ' + instructions[instruction_index])
    future_nodes = []

    for node in current_nodes:            
        #print('starting at ' + node)       
        if instructions[instruction_index] == 'L':
            future_nodes.append(network_dict[node][0])
        elif instructions[instruction_index] == 'R': 
            future_nodes.append(network_dict[node][1]) 
               
    keep_going = False
    for node in future_nodes:
        #print('found : ' + node)
        if node[-1] != 'Z':
            keep_going = True

    current_nodes = future_nodes

    steps += 1
    instruction_index += 1 
    #force_stop += 1 

    #if force_stop > 50:
    #    keep_going = False
     
'''

#print(steps) # 20569
