from pathlib import Path
import datetime
    
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

node = 'AAA'
steps = 0
instruction_index = 0

while node != 'ZZZ':
    #print('start : ' + node)
    if instruction_index >= len(instructions):
        instruction_index = 0 
    
    if instructions[instruction_index] == 'L':
        node = network_dict[node][0] 
    elif instructions[instruction_index] == 'R': 
        node = network_dict[node][1] 
    
    #print('going : ' + instructions[instruction_index])
    #print('found : ' + node)
    steps += 1
    instruction_index += 1 
    #print()

print(steps)
