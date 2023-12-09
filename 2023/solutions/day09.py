from pathlib import Path
import math
    
base_path = Path(__file__).parent
path = (base_path / "../input/day09_input.txt").resolve()
#path = (base_path / "../input/day09_sample_input.txt").resolve()

with open(path) as f:
    lines = f.readlines()

result = 0
result_list = []

for line in lines:
    seq = line.split()
    seq = [int(item) for item in seq]
    diff = []

    last_diffs = [seq[-1]]

    if sum(seq) == seq[0]*len(seq):
        result += seq[len(seq) - 1] + (seq[len(seq) - 1] - seq[len(seq) - 2])
        continue

    while not all(ele == 0 for ele in seq):
        for i in range(1, len(seq)):
            diff.append(seq[i] - seq[i-1])
        last_diffs.insert(0,(seq[len(seq) - 1] - seq[len(seq) - 2]))
        #print(seq)
        seq = diff
        diff = []

    
    #next = last_diffs[0]
    next_diffs = [last_diffs[0]]
    for i in range(1, len(last_diffs)):
        next_diffs.append(last_diffs[i]+next_diffs[i-1])

    result += next_diffs[-1]
    result_list.append(next_diffs[-1])

    if ' 3250965' in line:
        print(last_diffs)
        print(next_diffs)

    #print(last_diffs)
    #print(next_diffs)
print(result)
#for i in result_list:
    #print(i)
#print(result_list)