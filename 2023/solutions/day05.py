from pathlib import Path
import datetime
    
base_path = Path(__file__).parent
path = (base_path / "../input/day05_input.txt").resolve()

with open(path) as f:
    lines = f.readlines()

seeds_part1 = []
seeds_part2 = []
current_map = ''

maps = {}

seeds_str = lines[0].split(':')[1].split()
seeds_part1 = [eval(x) for x in seeds_str]


for line in lines:
    if 'seeds' in line:
        #print(seeds_part2)
        continue

    if ':' in line:
        current_map = line.split(':')[0]
        continue

    if line == '\n':
        continue

    if current_map in maps.keys():
        values = line.strip().split()
        values = [eval(x) for x in values]
        maps[current_map] += [values]
    else:
        values = line.strip().split()
        values = [eval(x) for x in values]
        maps[current_map] = [values]

#print(maps)

min_location = 9999999999999

for i in range(0, len(seeds_part1), 2):
    print('Starting from ' + str(seeds_part1[i]) + ' at ' + str(datetime.datetime.now()) )
    #print(str(range(seeds_part1[i], seeds_part1[i+1])))
    for j in range(seeds_part1[i], seeds_part1[i+1] + seeds_part1[i]):
        lookup = j


# each seed from first line of input
#for seed in seeds_part1:
#for seed in seeds_part1:
        #lookup = seed
        # each set of maps, e.g. 'seed-to-soil'
        for key in maps:  
            lookup_found = False   
            # there are multiple options for each map, check until you find at least one that fits
            for map in maps[key]:
                if lookup_found:
                    break

                if lookup >= map[1] and lookup < map[1] + map[2]: 
                    #print('lookup found: ' + str(lookup) + ' -> ' + str(lookup - (values[1] - values[0])) + ' for ' + key)
                    lookup = lookup - (map[1] - map[0])
                    lookup_found = True
                    break
        
        #print('location found ' + str(lookup) )
        if lookup < min_location:
            min_location = lookup
    

print(min_location) # 165788812
    




 # Part 1 - 2685
 # Part 2 - 83707

