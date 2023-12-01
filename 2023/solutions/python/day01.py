

with open('C:\\Users\\Megan\\Documents\\Projects\\advent-of-code\\2023\\input\\day01_input.txt') as f:
    lines = f.readlines()

calibration_values = []

numbers = ['one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'] # no zero? 

for line in lines:
    calibration_value = ''
    for char in line:
        if char.isnumeric():
            calibration_value += char
            break
        else:
            continue

    for char in line[::-1]:
        if char.isnumeric():
            calibration_value += char
            break
        else:
            continue

    calibration_values.append(int(calibration_value))

print(sum(calibration_values)) # Part 1 - 54081