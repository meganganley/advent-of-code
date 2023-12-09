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
        if instruction_index >= len(instructions):
            instruction_index = 0 
        
        if instructions[instruction_index] == 'L':
            node = network_dict[node][0] 
        elif instructions[instruction_index] == 'R': 
            node = network_dict[node][1] 
        
        step_count += 1
        instruction_index += 1 
    steps.append(step_count)
    
def LCMofArray(a):
  lcm = a[0]
  for i in range(1,len(a)):
    lcm = lcm*a[i]//math.gcd(lcm, a[i])
  return lcm

print(steps)
print(LCMofArray(steps))
