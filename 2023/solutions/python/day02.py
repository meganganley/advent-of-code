from pathlib import Path

class Game:
    is_possible = 1
    min_red = 0
    min_blue = 0
    min_green = 0

    def __init__(self, line):
        game_info = line.split(':')
        self.id = int(game_info[0].split(' ')[1])
        self.subsets = self.create_subsets(game_info[1])

    def create_subsets(self, all_subsets):
        subsets_list = all_subsets.split(';')
        parsed_subsets = []

        for subset_string in subsets_list:
            subset = Subset(0,0,0)
            colours = subset_string.split(',')
            for colour in colours:
                if 'red' in colour:
                    subset.red = int(colour.split()[0])
                    if subset.red > max_red:
                        self.is_possible = 0
                    if subset.red > self.min_red:
                        self.min_red = subset.red

                if 'blue' in colour:
                    subset.blue = int(colour.split()[0])
                    if subset.blue > max_blue:
                        self.is_possible = 0
                    if subset.blue > self.min_blue:
                        self.min_blue = subset.blue

                if 'green' in colour:
                    subset.green = int(colour.split()[0])
                    if subset.green > max_green:
                        self.is_possible = 0
                    if subset.green > self.min_green:
                        self.min_green = subset.green
                        
            parsed_subsets.append(subset)

        return parsed_subsets

    def get_power(self):
        return self.min_red * self.min_blue * self.min_green


class Subset:
    def __init__(self, red, blue, green):
        self.red = red
        self.blue = blue
        self.green = green
        
    def __str__(self):
        return "red: {}, blue: {}, green: {}".format(self.red, self.blue, self.green)
    
### Read input
base_path = Path(__file__).parent
path = (base_path / "../../input/day02_input.txt").resolve()

with open(path) as f:
    lines = f.readlines()

### Set up problem
max_red = 12
max_blue = 14
max_green = 13 

total_possible = 0
total_power = 0

## Find solution
for line in lines:
    game = Game(line)
    
    if game.is_possible:
        total_possible += game.id

    total_power += game.get_power()

print(total_possible)
print(total_power)

 # Part 1 - 2685
 # Part 2 - 83707

