import * as fs from 'fs'

const LINE_BREAK = '\n'

type Board = Array<Array<number>>

type Boards = Array<Board>

const file = fs.readFileSync(
  './src/question_4_part_1_example_input.txt',
  'utf-8'
)

const lines = file.split(LINE_BREAK)

const [drawNumbersLine, emptyLine, ...boardsLines] = lines

const drawNumbers = drawNumbersLine
  .split(',')
  .map((number) => Number.parseInt(number, 10))

const { boards } = boardsLines.reduce<{ boards: Boards; currBoard: Board }>(
  ({ boards, currBoard }, currLine, currLineIndex) => {
    const isEmptyLine = !currLine
    const isLastLine = currLineIndex + 1 === boardsLines.length

    if (isEmptyLine) {
      // Start new board
      return { boards: [...boards, currBoard], currBoard: [] }
    }

    const newBoardLine = currLine
      .split(' ')
      .filter((el) => !!el)
      .map((number) => Number.parseInt(number, 10))

    const updatedCurrBoard = [...currBoard, newBoardLine]

    if (isLastLine) {
      return { boards: [...boards, updatedCurrBoard], currBoard: [] }
    }

    return { boards, currBoard: updatedCurrBoard }
  },
  { boards: [], currBoard: [] }
)
