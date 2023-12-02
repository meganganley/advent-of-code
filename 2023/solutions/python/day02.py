import re

def create_subsets(all_subsets):
    subsets_list = all_subsets.split(';')

    parsed_subsets = []

    for subset_string in subsets_list:
        subset = Subset(0,0,0)
        colours = subset_string.split(',')
        for colour in colours:
            if 'red' in colour:
                subset.red = colour.split()[0]
            if 'blue' in colour:
                subset.blue = colour.split()[0]
            if 'green' in colour:
                subset.green = colour.split()[0]
        parsed_subsets.append(subset)

    return parsed_subsets

class Subset:
    def __init__(self, red, blue, green):
        self.red = red
        self.blue = blue
        self.green = green
        
    def set_red(self, red):
        self.red = red
        
    def set_blue(self, blue):
        self.blue = blue
        
    def set_green(self, green):
        self.green = green
        
    def __str__(self):
        return "red: {}, blue: {}, green: {}".format(self.red, self.blue, self.green)

    

with open('C:\\Users\\Megan\\Documents\\Projects\\advent-of-code\\2023\\input\\day02_input.txt') as f:
    lines = f.readlines()

games = {}

max_red = 12
max_green = 13 
max_blue = 14

count = 0

for line in lines:
    game_info = line.split(':')
    game_id = game_info[0].split(' ')[1]
    subsets = create_subsets(game_info[1])
    print('Game: {}'.format(game_id)) 

    for subset in subsets:
        print(subset)

    count += 1 
    if count > 4:
        break


 # Part 1 - 54081
 # Part 2 - 54649

