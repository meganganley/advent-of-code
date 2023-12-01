import re

with open('C:\\Users\\Megan\\Documents\\Projects\\advent-of-code\\2023\\input\\day01_input.txt') as f:
    lines = f.readlines()

calibration_values = []

numbers = ['one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'] # no zero? 


for line in lines:
    calibration_value = ''

    digit = re.search('\d', line)
    calibration_value += digit.group(0)
    
    digit = re.search('\d', line[::-1])
    calibration_value += digit.group(0)
    
    calibration_values.append(int(calibration_value))

print(sum(calibration_values)) # Part 1 - 54081


 # Part 1 - 54081