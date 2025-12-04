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

    public static string IncrementOutputJoltage(string battery_joltage, string cur_out_joltage, int out_joltage_digits)
    {
        if (cur_out_joltage.Length == out_joltage_digits)
        {
            return cur_out_joltage; // Initial output joltage is complete
        }

        return cur_out_joltage + battery_joltage;
    }

    public static List<string> AllReplacementCombinations(string cur_out_joltage, string battery_joltage)
    {
        List<string> combinations = new List<string> { };

        for (int i = 0; i < cur_out_joltage.Length; i++)
        {
            string out_part1 = i == 0 ? "" : cur_out_joltage.Substring(0, i);
            string out_part2 = i == cur_out_joltage.Length ? "" : cur_out_joltage.Substring(i + 1);
            string joltage_option = out_part1 + out_part2 + battery_joltage;

            combinations.Add(joltage_option);
        }
        return combinations;
    }

    public static long MaxLocalJoltage(string cur_out_joltage, string battery_joltage)
    {
        long current_joltage = long.Parse(cur_out_joltage);

        List<string> combinations = AllReplacementCombinations(cur_out_joltage, battery_joltage);

        long max = current_joltage;

        foreach (string combination in combinations)
        {
            if (Math.Max(current_joltage, long.Parse(combination)) > max)
            {
                max = long.Parse(combination);
            }
        }

        return max;
    }

    public static void Part2()
    {
        Console.WriteLine("Day 3: Lobby: Part 2");

        string filePath = "./day_3_input.txt";

        try
        {
            long max_joltage = 0;

            foreach (string line in File.ReadLines(filePath))
            {
                string bank = line;

                string out_joltage = "";
                int out_joltage_digits = 12;
                int debug_index = 0;

                foreach (char battery_char in bank)
                {
                    string battery_joltage = battery_char.ToString();

                    string new_out_joltage = IncrementOutputJoltage(battery_joltage, out_joltage, out_joltage_digits);

                    if (!new_out_joltage.Equals(out_joltage))
                    {
                        out_joltage = new_out_joltage;
                        continue;
                    }

                    long max = MaxLocalJoltage(out_joltage, battery_joltage);

                    // Update output joltage
                    out_joltage = max.ToString();

                    debug_index += 1;
                }

                long max_bank_joltage = long.Parse(out_joltage);
                max_joltage += max_bank_joltage;

                Console.WriteLine("For bank [" + bank + "] the largest joltage possible is " + max_bank_joltage);
            }
            Console.WriteLine("Max Joltage Sum is " + max_joltage);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An I/O error occurred: {ex.Message}");
        }
    }
}