using System;
using System.IO;

namespace AdventXMas2025;

public static class Day3_Lobby
{
    public static void Part1()
    {
        Console.WriteLine("Day 3: Lobby");

        string filePath = "./day_3_input.txt";

        try
        {
            int max_joltage = 0;

            foreach (string line in File.ReadLines(filePath))
            {
                string bank = line;

                Console.WriteLine("Debug: bank: " + bank);

                string out_joltage_first = "";
                string out_joltage_second = "";

                foreach (char battery_char in bank)
                {
                    string battery_joltage = battery_char.ToString();

                    if (out_joltage_first.Equals("") && out_joltage_second.Equals(""))
                    {
                        out_joltage_first = battery_joltage;
                        continue;
                    }

                    if (!out_joltage_first.Equals("") && out_joltage_second.Equals(""))
                    {
                        out_joltage_second = battery_joltage;
                        continue;
                    }

                    int current_joltage = int.Parse(out_joltage_first + out_joltage_second);

                    int joltage_option_1 = int.Parse(out_joltage_first + battery_joltage);
                    int joltage_option_2 = int.Parse(out_joltage_second + battery_joltage);

                    int max = Math.Max(current_joltage, Math.Max(joltage_option_1, joltage_option_2));

                    out_joltage_first = max.ToString()[0].ToString();
                    out_joltage_second = max.ToString()[1].ToString();
                }

                int max_bank_joltage = int.Parse(out_joltage_first + out_joltage_second);
                max_joltage += max_bank_joltage;

                // Console.WriteLine("For bank [" + bank + "] the largest joltage possible is " + max_bank_joltage);
            }
            Console.WriteLine("Max Joltage Sum is " + max_joltage);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An I/O error occurred: {ex.Message}");
        }
    }
}