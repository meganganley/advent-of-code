from pathlib import Path

base_path = Path(__file__).parent
path = (base_path / "../input/day02_input.txt").resolve()

with open(path) as f:
    lines = f.readlines()

## A & X - Rock (beats Scissors)
## B & Y - Paper (beats Rock)
## C & Z - Scissors  (beats Paper)

equivalent_moves = {'A': 'X', 'B': 'Y', 'C': 'Z'}
winning_moves = {'A': 'Y', 'B': 'Z', 'C': 'X'} 
move_scores = {'X': 1, 'Y': 2, 'Z': 3}
outcome_scores = {'win': 6, 'draw': 3, 'lose': 0}

count = 0

part1_score = 0

## Part 1 
for line in lines:
    (opponent, response) = line.split() 

    # get the points for my move
    part1_score += move_scores[response]

    # check if I won
    if winning_moves[opponent] == response:
        part1_score += outcome_scores['win']
    # check if we drew
    elif equivalent_moves[opponent] == response:
        part1_score += outcome_scores['draw']


losing_moves = {'A': 'Z', 'B': 'X', 'C': 'Y'} 
outcome_scores = {'X': 0, 'Y': 3, 'Z': 6}

## X - Loss needed
## Y - Draw needed
## Z - Win needed

part2_score = 0

## Part 2 
for line in lines:
    (opponent, outcome_needed) = line.split() 

    # get the points for my win/loss/draw
    part2_score += outcome_scores[outcome_needed]

    # get points for my score (find what my move needed to be)
    # draw
    if outcome_needed == 'Y':
        part2_score += move_scores[equivalent_moves[opponent]]
    # win
    elif outcome_needed == 'Z':
        move = winning_moves[opponent]
        part2_score += move_scores[move] 
    # loss
    elif outcome_needed == 'X':
        move = losing_moves[opponent]
        part2_score += move_scores[move] 


print('Part 1 Score: {}'.format(part1_score)) 
print('Part 2 Score: {}'.format(part2_score)) 

# Part 1 - 11767
# Part 2 - 13886