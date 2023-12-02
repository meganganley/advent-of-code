import re
from pathlib import Path

### Read input
base_path = Path(__file__).parent
path = (base_path / "../../input/day01_input.txt").resolve()

with open(path) as f:
    lines = f.readlines()

### Set up problem
numbers = {'one':'1', 'two':'2', 'three':'3', 'four':'4', 'five':'5', 'six':'6', 'seven':'7', 'eight':'8', 'nine':'9'}

pattern = '([0-9])'
reverse_pattern = '([0-9])'

calibration_values = []

# build up the regex patterns for words (forward and back)
for key in numbers:
    pattern += '|('+key+')'
    reverse_pattern += '|('+key[::-1]+')'
 
### Find solution
for line in lines:
    calibration_value = ''

    # get first number
    num = re.search(pattern, line)
    if num.group(0).isnumeric():
        calibration_value += num.group(0)
    else:
        # use dict to convert word into integer
        calibration_value += numbers[num.group(0)]
   
    # get second number (reading backwards from end)
    num = re.search(reverse_pattern, line[::-1])
    if num.group(0).isnumeric():
        calibration_value += num.group(0)
    else:
        # use dict to convert word into integer (and un-reverse word)
        calibration_value += numbers[num.group(0)[::-1]]
    
    calibration_values.append(int(calibration_value))

print(sum(calibration_values)) 

 # Part 1 - 54081
 # Part 2 - 54649