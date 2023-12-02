class Game:
    is_possible = 1

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
                if 'blue' in colour:
                    subset.blue = int(colour.split()[0])
                    if subset.blue > max_blue:
                        self.is_possible = 0
                if 'green' in colour:
                    subset.green = int(colour.split()[0])
                    if subset.green > max_green:
                        self.is_possible = 0
            parsed_subsets.append(subset)

        return parsed_subsets


class Subset:
    def __init__(self, red, blue, green):
        self.red = red
        self.blue = blue
        self.green = green
        
    def __str__(self):
        return "red: {}, blue: {}, green: {}".format(self.red, self.blue, self.green)

    

with open('C:\\Users\\Megan\\Documents\\Projects\\advent-of-code\\2023\\input\\day02_input.txt') as f:
    lines = f.readlines()


max_red = 12
max_blue = 14
max_green = 13 

total = 0

for line in lines:
    game = Game(line)
    
    if game.is_possible:
        total += game.id


print(total)

 # Part 1 - 2685

