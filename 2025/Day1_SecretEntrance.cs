using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventXMas2025;

public static class Day1_SecretEntrance
{
    public static void Part1()
    {
        Console.WriteLine("Day 1: Secret Entrance");

        string filePath = "./day_1_input.txt";

        try
        {
            int zero_hit = 0;
            int dial_arrow = 50;
            // Read Dial Rotation Sequence and find Secret Code
            foreach (string line in File.ReadLines(filePath))
            {
                // Parse dial rotation direction and clicks inputs
                Match match_r = Regex.Match(line, @"[LR]");
                string rotation_direction = match_r.Value;
                Match match_c = Regex.Match(line, @"\d+");
                int clicks = int.Parse(match_c.Value);

                if (rotation_direction.Equals("R"))
                {
                    int next = dial_arrow + clicks;
                    if (next > 99)
                    {
                        next = next % 100;
                    }
                    dial_arrow = next;
                }
                else // L
                {
                    int next = dial_arrow - clicks;
                    if (next < 0)
                    {
                        next = (next % 100 + 100) % 100;
                    }
                    dial_arrow = next;
                }

                Console.WriteLine("The dial is rotated " + line + " to point at " + dial_arrow);

                if (dial_arrow == 0)
                {
                    zero_hit += 1;
                }

            }

            Console.WriteLine("Zero hits: " + zero_hit);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file was not found at {filePath}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An I/O error occurred: {ex.Message}");
        }

    }

    public static void Part2()
    {
        Console.WriteLine("Day 1: Secret Entrance (Part 2)");

        string filePath = "./day_1_input.txt";

        try
        {
            int zero_hit = 0;
            int dial_arrow = 50;
            // Read Dial Rotation Sequence and find Secret Code
            foreach (string line in File.ReadLines(filePath))
            {
                // Parse dial rotation direction and clicks inputs
                Match match_r = Regex.Match(line, @"[LR]");
                string rotation_direction = match_r.Value;
                Match match_c = Regex.Match(line, @"\d+");
                int clicks = int.Parse(match_c.Value);

                int hit_or_pass_zero = 0;

                if (rotation_direction.Equals("R"))
                {
                    int next = dial_arrow + clicks;
                    if (next > 99)
                    {
                        hit_or_pass_zero = next / 100;

                        next = next % 100;
                    }
                    dial_arrow = next;
                }
                else // L
                {
                    int next = dial_arrow - clicks;
                    if (next < 0)
                    {
                        hit_or_pass_zero = Math.Abs(next) / 100;
                        if (dial_arrow != 0)
                        {
                            hit_or_pass_zero += 1;
                        }

                        next = (next % 100 + 100) % 100;

                    }
                    else if (next == 0)
                    {
                        hit_or_pass_zero += 1;
                    }
                    dial_arrow = next;
                }

                zero_hit += hit_or_pass_zero;

                Console.WriteLine("The dial is rotated " + line + " to point at " + dial_arrow);
                Console.WriteLine("Hit or pass zero: " + hit_or_pass_zero);
            }

            Console.WriteLine("Zero hits: " + zero_hit);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file was not found at {filePath}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An I/O error occurred: {ex.Message}");
        }

    }
}