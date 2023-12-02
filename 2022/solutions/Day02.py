from pathlib import Path

base_path = Path(__file__).parent
path = (base_path / "../input/day02_input.txt").resolve()

with open(path) as f:
    lines = f.readlines()

## A & X - Rock (beats Scissors)
## B & Y - Paper (beats Rock)
## C & Z - Scissors  (beats Paper)

equivalent_moves = {'A': 'X', 'B': 'Y', 'C': 'Z'}
winning_moves = {'X': 'C', 'Y': 'A', 'Z': 'B'}
scores = {'X': 1, 'Y': 2, 'Z': 3}

count = 0

score = 0

for line in lines:
    (opponent, response) = line.split()
    # get the points for my move
    score += scores[response]

    # check if I won
    if winning_moves[response] == opponent:
        score += 6
    # check if we drew
    elif equivalent_moves[opponent] == response:
        score += 3


print('Score: {}'.format(score)) 

# Part 1 - 11767