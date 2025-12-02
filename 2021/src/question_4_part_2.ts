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

  public sumUnchecked = () => {
    return this.rows.reduce<number>((sum, currRow) => {
      const uncheckedRowNumbers = currRow.filter(
        (el) => !this.checkedNumbers.includes(el)
      )
      const totalUncheckedInRow = uncheckedRowNumbers.reduce<number>(
        (rowSum, el) => rowSum + el,
        0
      )

      return sum + totalUncheckedInRow
    }, 0)
  }
}

const LINE_BREAK = '\n'

const file = fs.readFileSync('./src/question_4_part_1_input.txt', 'utf-8')

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

let winners: Array<{
  board?: Board
  points?: number
}> = []

while (winners.length !== boards.length) {
  const drawNumber = drawNumbers.shift()

  if (drawNumber === undefined) {
    console.log(`No draw numbers left`)
    break
  }

  boards.forEach((board) => {
    // Is already winner, go to the next one
    if (board.isWinner) {
      return
    }

    board.checkBoard(drawNumber)

    if (board.isWinner) {
      const winner = {
        board,
        points: board.sumUnchecked() * drawNumber,
      }
      winners.push(winner)
    }
  })
}

const lastWinner = winners[winners.length - 1]

console.log('Last winner points', lastWinner.points)
