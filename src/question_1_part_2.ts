import * as fs from 'fs'

const LINE_BREAK = '\n'

const file = fs.readFileSync('./src/question_1_part_1_input.txt', 'utf-8')

const measurements = file.split(LINE_BREAK).map(Number)

// Tendo X linhas, eu faco X - 2 iteracoes, com um minimo de 3
const totalWindows = Math.max(measurements.length - 2, 3)

const totalMeasurementByWindow = []

// Iterate all windows
for (let i = 0; i < totalWindows; i++) {
  // A index 0 - 2
  // B index 1 - 3
  // C index 2 - 4
  // D index 4 - 6
  // E index 5 - 7
  // ...

  const windowStartIndex = i
  const windowEndIndex = i + 3

  // Get window
  const window = measurements.filter(
    (_measurement, index) => index >= windowStartIndex && index < windowEndIndex
  )

  // Sum window measurements
  const windowTotal = window.reduce((prev, curr) => prev + curr)

  totalMeasurementByWindow.push(windowTotal)
}

let totalIncreases = 0

totalMeasurementByWindow.reduce((prev, curr) => {
  if (curr > prev) {
    totalIncreases += 1
  }

  return curr
})

console.log('The total number of increases were ', totalIncreases)
