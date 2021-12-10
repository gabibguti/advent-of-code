import * as fs from 'fs'

class Board {
  rows: Array<Array<number>> = []
  cols: Array<Array<number>> = []
  isWinner: boolean = false
  checkedNumbers: number[] = []
  static size: number = 5

  constructor(rows: Array<Array<number>>) {
    this.rows = Array.from(rows)

    for (let colIndex = 0; colIndex < Board.size; colIndex++) {
      const col = rows.map((row) => row[colIndex])
      this.cols.push(col)
    }
  }

  private isComplete = (list: number[]) => {
    const totalChecks = list.filter((el) =>
      this.checkedNumbers.includes(el)
    ).length
    const isComplete = totalChecks === list.length

    return isComplete
  }

  private checkRows = () => {
    this.rows.forEach((row) => {
      if (this.isComplete(row)) {
        this.isWinner = true
        return
      }
    })
  }

  private checkCols = () => {
    this.cols.forEach((col) => {
      if (this.isComplete(col)) {
        this.isWinner = true
        return
      }
    })
  }

  public checkBoard = (drawNumber: number) => {
    this.checkedNumbers.push(drawNumber)

    this.checkRows()

    if (this.isWinner) {
      return
    }

    this.checkCols()
  }
}

const LINE_BREAK = '\n'

const file = fs.readFileSync(
  './src/question_4_part_1_example_input.txt',
  'utf-8'
)

const lines = file.split(LINE_BREAK)

const [drawNumbersLine, emptyLine, ...boardsLines] = lines

const drawNumbers = drawNumbersLine
  .split(',')
  .map((number) => Number.parseInt(number, 10))

const { listOfBoardRows } = boardsLines.reduce<{
  listOfBoardRows: Array<Array<Array<number>>>
  currBoardRows: Array<Array<number>>
}>(
  ({ listOfBoardRows, currBoardRows }, currLine, currLineIndex) => {
    const isEmptyLine = !currLine
    const isLastLine = currLineIndex + 1 === boardsLines.length

    if (isEmptyLine) {
      // Start new board
      return {
        listOfBoardRows: [...listOfBoardRows, currBoardRows],
        currBoardRows: [],
      }
    }

    const newBoardRow = currLine
      .split(' ')
      .filter((el) => !!el)
      .map((number) => Number.parseInt(number, 10))

    const updatedCurrBoardRows = [...currBoardRows, newBoardRow]

    if (isLastLine) {
      return {
        listOfBoardRows: [...listOfBoardRows, updatedCurrBoardRows],
        currBoardRows: [],
      }
    }

    return { listOfBoardRows, currBoardRows: updatedCurrBoardRows }
  },
  { listOfBoardRows: [], currBoardRows: [] }
)

const boards = listOfBoardRows.map((boardRows) => new Board(boardRows))

let hasWinner: boolean = false

while (!hasWinner) {
  const drawNumber = drawNumbers.shift()

  if (drawNumber === undefined) {
    console.log(`No winners`)
    break
  }

  boards.forEach((board, index) => {
    board.checkBoard(drawNumber)
    if (board.isWinner) {
      hasWinner = true
      console.log(`Board ${index} is the winner!`)
      return
    }
  })
}
