from pathlib import Path
import math
    
base_path = Path(__file__).parent
path = (base_path / "../input/day09_input.txt").resolve()
#path = (base_path / "../input/day09_sample_input.txt").resolve()

with open(path) as f:
    lines = f.readlines()

next_result = 0
previous_result = 0
result_list = []

for line in lines:
    seq = line.split()
    seq = [int(item) for item in seq]
    diff = []

    last_diffs = [seq[-1]]
    first_diffs = [seq[0]]

    while not all(ele == 0 for ele in seq):
        for i in range(1, len(seq)):
            diff.append(seq[i] - seq[i-1])
        last_diffs.insert(0,(seq[len(seq) - 1] - seq[len(seq) - 2]))
        first_diffs.insert(0,(seq[1] - seq[0]))
        seq = diff
        diff = []
    
    next_diffs = [last_diffs[0]]
    for i in range(1, len(last_diffs)):
        next_diffs.append(last_diffs[i]+next_diffs[i-1])

    previous_diffs = [first_diffs[0]]
    for i in range(1, len(first_diffs)):
        previous_diffs.append(first_diffs[i]-previous_diffs[i-1])

    next_result += next_diffs[-1]
    previous_result += previous_diffs[-1]

print(next_result) # 1993300041
print(previous_result) # 1038