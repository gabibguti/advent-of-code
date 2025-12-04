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

    public static int FindLowestDigit(string cur_out_joltage)
    {
        int min = 0;
        int index = -1;
        for (int i = 0; i < cur_out_joltage.Length; i++)
        {
            char digit = cur_out_joltage[i];
            if (min == 0 || int.Parse(digit.ToString()) < min)
            {
                min = int.Parse(digit.ToString());
                index = i;
            }
        }
        return index;
    }

    public static long MaxLocalJoltage(string cur_out_joltage, string battery_joltage)
    {
        long current_joltage = long.Parse(cur_out_joltage);

        // Option 1: Replace last digit
        long joltage_option_1 = long.Parse(cur_out_joltage.Substring(0, cur_out_joltage.Length - 1) + battery_joltage);
        // Option 2: Remove first digit and append new as last digit
        long joltage_option_2 = long.Parse(cur_out_joltage.Substring(1) + battery_joltage);
        // Option 3: Break digits sequence on the lowest number
        int index = FindLowestDigit(cur_out_joltage);
        string out_part1 = cur_out_joltage.Substring(0, index);
        string out_part2 = cur_out_joltage.Substring(index + 1);
        long joltage_option_3 = long.Parse(out_part1 + out_part2 + battery_joltage);

        long max = Math.Max(current_joltage, Math.Max(joltage_option_1, Math.Max(joltage_option_2, joltage_option_3)));

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

                Console.WriteLine("Debug: bank: " + bank);

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
                    Console.WriteLine("Max Local Joltage: " + max);
                    Console.WriteLine("debug_index: " + debug_index);

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