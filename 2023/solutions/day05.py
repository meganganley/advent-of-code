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


list_of_maps = []
for key, value in maps.items():
    temp_dict = {}
    for r in value:
        values = r.split() 
        values = [eval(x) for x in values]
        count = 0
        for i in range(values[1], values[1]+values[2]):
            temp_dict[i] = values[0]+count
            count += 1
    list_of_maps.append(temp_dict)
    
locations = []

for seed in seeds:
    lookup = seed
    for map in list_of_maps:
        if lookup in map.keys():
            lookup = map[lookup]
    locations.append(lookup)
    

print(min(locations))



 # Part 1 - 2685
 # Part 2 - 83707

