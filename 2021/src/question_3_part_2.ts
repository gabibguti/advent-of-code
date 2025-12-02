import * as fs from 'fs'

const LINE_BREAK = '\n'
const BINARY_BASE = 2

type Rate = {
  binary: string
  decimal?: number
}

type DecisionResponse =
  | 'useBinaryNumbersWithBitZero'
  | 'useBinaryNumbersWithBitOne'

type Decide = (totalOnes: number, totalZeros: number) => DecisionResponse

const file = fs.readFileSync('./src/question_3_part_1_input.txt', 'utf-8')

const binaryNumbers = file.split(LINE_BREAK)

let oxygenRate: Rate = {
  binary: '',
}
let co2Rate: Rate = {
  binary: '',
}

const mostCommonValue: Decide = (totalOnes, totalZeros) => {
  if (totalZeros > totalOnes) {
    return 'useBinaryNumbersWithBitZero'
  } else {
    return 'useBinaryNumbersWithBitOne'
  }
}

const leastCommonValue: Decide = (totalOnes, totalZeros) => {
  if (totalOnes < totalZeros) {
    return 'useBinaryNumbersWithBitOne'
  } else {
    return 'useBinaryNumbersWithBitZero'
  }
}

const getRate = (
  validNumbersForOxygenRate: string[],
  decide: Decide,
  bitPosition: number = 0
): string => {
  if (validNumbersForOxygenRate.length <= 1) {
    const [finalValidNumber] = validNumbersForOxygenRate
    return finalValidNumber ?? ''
  }

  const binaryNumbersWithBitZero = validNumbersForOxygenRate.filter(
    (binaryNumber) => binaryNumber.substr(bitPosition, 1) === '0'
  )

  const totalZeros = binaryNumbersWithBitZero.length

  const binaryNumbersWithBitOne = validNumbersForOxygenRate.filter(
    (binaryNumber) => binaryNumber.substr(bitPosition, 1) === '1'
  )

  const totalOnes = binaryNumbersWithBitOne.length

  const decision = decide(totalOnes, totalZeros)

  if (decision === 'useBinaryNumbersWithBitZero') {
    return getRate(binaryNumbersWithBitZero, decide, bitPosition + 1)
  } else {
    return getRate(binaryNumbersWithBitOne, decide, bitPosition + 1)
  }
}

oxygenRate.binary = getRate(binaryNumbers, mostCommonValue)
co2Rate.binary = getRate(binaryNumbers, leastCommonValue)

oxygenRate.decimal = Number.parseInt(oxygenRate.binary, BINARY_BASE)
co2Rate.decimal = Number.parseInt(co2Rate.binary, BINARY_BASE)

console.log('Oxygent rate:', oxygenRate)
console.log('C02 rate:', co2Rate)

console.log('Life support rate:', oxygenRate.decimal * co2Rate.decimal)
