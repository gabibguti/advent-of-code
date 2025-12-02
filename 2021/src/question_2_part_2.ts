import * as fs from 'fs'

type Direction = 'forward' | 'down' | 'up'

type Command = {
  direction: Direction
  value: number
}

type Position = {
  horizontal: number
  depth: number
  aim: number
}

const LINE_BREAK = '\n'

const DEFAULT_POSITION: Position = {
  horizontal: 0,
  depth: 0,
  aim: 0,
}

const file = fs.readFileSync('./src/question_2_part_1_input.txt', 'utf-8')

const commands: Command[] = file.split(LINE_BREAK).map((line) => {
  const [direction, value] = line.split(' ')
  return { direction: direction as Direction, value: Number(value) }
})

const finalPosition = commands.reduce<Position>((position, command) => {
  switch (command.direction) {
    case 'forward':
      return {
        ...position,
        horizontal: position.horizontal + command.value,
        depth: position.depth + position.aim * command.value,
      }
    case 'down':
      return {
        ...position,
        aim: position.aim + command.value,
      }
    case 'up':
      return {
        ...position,
        aim: position.aim - command.value,
      }
  }

  return position
}, DEFAULT_POSITION)

console.log('Final submarine position: ', finalPosition)
console.log(
  'Final submarine position multiplied: ',
  finalPosition.horizontal * finalPosition.depth
)
