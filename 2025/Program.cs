using System.IO;
using System.Text.RegularExpressions;

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
