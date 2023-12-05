from pathlib import Path
    
base_path = Path(__file__).parent
path = (base_path / "../input/day05_input.txt").resolve()

with open(path) as f:
    lines = f.readlines()

seeds = []
current_map = ''

maps = {}

for line in lines:
    if 'seeds' in line:
        seeds_str = line.split(':')[1].split()
        seeds = [eval(x) for x in seeds_str]
        continue

    if ':' in line:
        current_map = line.split(':')[0]
        continue

    if line == '\n':
        continue

    if current_map in maps.keys():
        maps[current_map] += [line.strip()]
    else:
        maps[current_map] = [line.strip()]

print(maps)

'''

lists_and_offsets = []
for key, value in maps.items():
    temp_list = []
    for r in value:
        values = r.split() 
        values = [eval(x) for x in values]
        count = 0
        offset = values[0] - values[1] # 98
        temp_list = []
        temp_list.extend(range(values[1], values[1]+values[2]))

        #temp_list = []
        #for i in range(values[1], values[1]+values[2]):
        #    temp_dict[i] = values[0]+count
        #    count += 1
        #lists_and_offsets.append((temp_list, offset))
    break
'''

locations = []

# each seed from first line of input
for seed in seeds:
    lookup = seed
    # each set of maps, e.g. 'seed-to-soil'
    for key in maps:  
        lookup_found = False      
        # there are multiple options for each map, check until you find at least one that fits
        for map in maps[key]:
            #print(map)
            if lookup_found:
                break

            values = map.split() 
            values = [eval(x) for x in values]
            #print('lookup checking: ' + str(lookup) + ' : ' + str(values[1]) + ' -> ' + str(values[1] + values[2]) + ' for ' + key)

            if lookup >= values[1] and lookup < values[1] + values[2]: 
                print('lookup found: ' + str(lookup) + ' -> ' + str(lookup - (values[1] - values[0])) + ' for ' + key)
                lookup = lookup - (values[1] - values[0])
                lookup_found = True
                break
    
    print('location found ' + str(lookup) )
    locations.append(lookup)
    

print(min(locations)) # 165788812
    




 # Part 1 - 2685
 # Part 2 - 83707

