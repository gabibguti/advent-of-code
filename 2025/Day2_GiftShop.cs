using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventXMas2025;

public class DivisorFinder
{
    private readonly int _number;
    private IEnumerator<int> _divisorEnumerator;

    public DivisorFinder(int n)
    {
        _number = n;
        // 1. Initialize the enumerator to the sequence of divisors
        _divisorEnumerator = GetDivisors(n).GetEnumerator();
    }

    // This method is called repeatedly to get the next divisor
    public int FindNextDivisor()
    {
        // 2. Move to the next item in the sequence
        if (_divisorEnumerator.MoveNext())
        {
            // 3. Return the current item
            return _divisorEnumerator.Current;
        }
        
        // 4. If no more divisors are available, return a sentinel value (e.g., -1)
        return -1; 
    }

    // Iterator block to generate all divisors (excluding 1)
    private static IEnumerable<int> GetDivisors(int n)
    {
        if (n <= 1)
            yield break; // Exit if the number is 1 or less

        // We only check up to the square root
        for (int i = 2; i * i <= n; i++)
        {
            if (n % i == 0)
            {
                // Yield the current divisor
                yield return i;

                // Yield the complementary divisor, but only if it's different
                // Example: For 30, when i=2, yield 2 and 15 (30/2)
                // If i*i != n (e.g., 25, when i=5, i*i=n, we only yield 5 once)
                if (i * i != n)
                {
                    yield return n / i;
                }
            }
        }
        
        // Handle the case where n is prime or a perfect square's root
        // If n is prime, this loop doesn't execute fully, and n itself must be yielded last.
        // We ensure n is yielded only if the loop didn't cover it (which means n is prime or a perfect square)
        if (n > 1)
        {
            yield return n;
        }
    }
}

public static class Day2_GiftShop
{
    public static int FindSmallestDivisor(int n)
    {
        // 1. Handle Trivial Case
        if (n <= 1)
        {
            // 1 has no divisor greater than 1.
            return n;
        }

        // 2. Check for Divisibility by 2
        if (n % 2 == 0)
        {
            return 2;
        }
        // 3. Check Odd Divisors up to the Square Root
        // We only need to check up to the square root of n (i*i <= n).
        for (int i = 3; i * i <= n; i += 2)
        {
            if (n % i == 0)
            {
                // Found the smallest divisor
                return i;
            }
        }

        // 4. If no divisor was found, the number is prime.
        // The smallest divisor greater than 1 is the number itself.
        return n;
    }

    public static bool IsValidProductIdWithPartition(string product_id_str, int divisor)
    {
        int n_digits = product_id_str.Length / divisor; // 5
        // Console.WriteLine("Debug n_digits: " + n_digits);

        string last_slice = "*";

        for (int part = 1; part < divisor + 1; part++) // 3 parts
        {
            // Console.WriteLine("Debug part: " + part);
            // Find current product ID slice
            int init = n_digits * (part - 1); // 0, 5, 10

            // Console.WriteLine("Debug init: " + init);

            string product_id_slice = "";

            // Console.WriteLine("Debug Substring with init and end");
            product_id_slice = product_id_str.Substring(init, n_digits);
            // Console.WriteLine("Debug product_id_slice: " + product_id_slice);

            // Compare to last slice
            if (last_slice != "*" && !last_slice.Equals(product_id_slice))
            {
                // Console.WriteLine("VALID!");
                return true;
            }
            last_slice = product_id_slice;
        }
        // Console.WriteLine("INVALID!");
        return false;
    }

    public static bool RecursiveIsValidProductIdWithPartition(DivisorFinder finder, string product_id_str)
    {
        int divisor = finder.FindNextDivisor();

        if (divisor <= 1)
        {
            return true;
        }

        if (!IsValidProductIdWithPartition(product_id_str, divisor))
        {
            return false;
        }

        return RecursiveIsValidProductIdWithPartition(finder, product_id_str);
    }

    public static bool IsValidProductId(long product_id)
    {
        string product_id_str = product_id.ToString();

        // int smallest_divisor = FindSmallestDivisor(product_id_str.Length);
        DivisorFinder finder = new DivisorFinder(product_id_str.Length);

        return RecursiveIsValidProductIdWithPartition(finder, product_id_str);

        // if (smallest_divisor != 2)
        // {
        // 111 case
        // char last_c = '*';
        // foreach (char c in product_id_str)
        // {
        //     if (last_c != '*' && c != last_c)
        //     {
        //         return true;
        //     }
        //     last_c = c;
        // }
        // return false;
        // }

        // if (smallest_divisor == 2)
        // {
        //     int halfLength = product_id_str.Length / 2;

        //     string firstHalf = product_id_str.Substring(0, halfLength);
        //     string secondHalf = product_id_str.Substring(halfLength);

        //     // Console.WriteLine("HALFS: " + firstHalf + " and " + secondHalf);

        //     if (firstHalf.Equals(secondHalf))
        //     {
        //         // Console.WriteLine("Invalid ID: " + product_id_str);
        //         return false;
        //     }
        //     return true;
        // }


    }
    public static void Part1()
    {
        Console.WriteLine("Day 2: Gift Shop");

        string filepath = "./day_2_input.txt";

        try
        {
            string file_content = File.ReadAllText(filepath);
            string[] product_id_ranges = file_content.Split(",");

            long invalid_product_id_sum = 0;
            List<long> invalid_product_ids = new List<long> { };

            foreach (string product_id_range in product_id_ranges)
            {
                string[] product_id_boundary = product_id_range.Split("-");
                long initial_product_id = long.Parse(product_id_boundary[0]);
                long final_product_id = long.Parse(product_id_boundary[1]);

                for (long product_id = initial_product_id; product_id < final_product_id + 1; product_id++)
                {
                    Console.WriteLine("Product ID: " + product_id);
                    if (!IsValidProductId(product_id))
                    {
                        invalid_product_id_sum += product_id;
                        invalid_product_ids.Add(product_id);
                    }
                }
            }

            Console.WriteLine("Sum of Invalid IDs: " + invalid_product_id_sum);
            string result = string.Join(", ", invalid_product_ids);
            Console.WriteLine("Invalid IDs: " + result);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An I/O error occurred: {ex.Message}");
        }
    }
}