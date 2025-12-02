import * as fs from 'fs'

const LINE_BREAK = '\n'
const BINARY_BASE = 2

type Rate = {
  binary: string
  decimal?: number
}

const file = fs.readFileSync('./src/question_3_part_1_input.txt', 'utf-8')

const binaryNumbers = file.split(LINE_BREAK)

const [firstBinaryNumber] = binaryNumbers

const totalBitsPerBinaryNumber = firstBinaryNumber.length

let gamaRate: Rate = {
  binary: '',
}
let episilonRate: Rate = {
  binary: '',
}

for (
  let bitPosition = 0;
  bitPosition < totalBitsPerBinaryNumber;
  bitPosition++
) {
  const totalZeros = binaryNumbers.filter(
    (binaryNumber) => binaryNumber.substr(bitPosition, 1) === '0'
  ).length
  const totalOnes = binaryNumbers.filter(
    (binaryNumber) => binaryNumber.substr(bitPosition, 1) === '1'
  ).length

  if (totalZeros >= totalOnes) {
    gamaRate.binary = gamaRate.binary.concat('0')
    episilonRate.binary = episilonRate.binary.concat('1')
  } else {
    gamaRate.binary = gamaRate.binary.concat('1')
    episilonRate.binary = episilonRate.binary.concat('0')
  }
}

gamaRate.decimal = Number.parseInt(gamaRate.binary, BINARY_BASE)
episilonRate.decimal = Number.parseInt(episilonRate.binary, BINARY_BASE)

console.log('Gama rate:', gamaRate)
console.log('Episilon rate:', episilonRate)

console.log('Power consumption:', gamaRate.decimal * episilonRate.decimal)
