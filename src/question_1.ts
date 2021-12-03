import * as fs from 'fs'

const LINE_BREAK = '\n'

const file = fs.readFileSync('./src/question_1_input.txt', 'utf-8')

const measurements = file.split(LINE_BREAK).map(Number)

let totalIncreases = 0

measurements.reduce((prev, curr) => {
  if (curr > prev) {
    totalIncreases += 1
  }

  return curr
})

console.log('The total number of increases were ', totalIncreases)
