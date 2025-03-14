using System;
using System.Collections.Generic;

public class CodeSmellsAndReliability
{
    private List<string> myList;

    public CodeSmellsAndReliability()
    {
        myList = new List<string>();
    }

    public void ProcessData(string input)
    {
        // 1. Empty catch block (Reliability: S1075, S2292)
        try
        {
            int number = int.Parse(input);
            Console.WriteLine("Number: " + number);
        }
        catch (FormatException)
        {
            // Empty catch block - bad practice
        }

        // 2. Magic number (Code Smell: S109)
        for (int i = 0; i < 100; i++) // 100 is a magic number
        {
            myList.Add("Item " + i);
        }

        // 3. Unused variable (Code Smell: S1481)
        int unusedVariable = 5;

        // 4. String concatenation in a loop (Performance/Reliability: S1643)
        string result = "";
        for (int i = 0; i < myList.Count; i++)
        {
            result += myList[i] + ", "; // Inefficient string concatenation
        }

        Console.WriteLine(result);

        // 5. Comparing floating-point numbers with equality (Reliability: S2184)
        double a = 0.1 + 0.2;
        if (a == 0.3) // Problematic floating-point comparison
        {
            Console.WriteLine("Equal");
        }
        else
        {
            Console.WriteLine("Not equal");
        }

        //6. Method too long (Code Smell: S1135)
        //This method is already long, and adding much more would make it worse.
        //Imagine a method with 200+ lines.

        //7. Missing or incorrect XML documentation (Code Smell: S101)
        //No XML documentation on this class or method.
    }

    //8. Non-private static fields should not be mutable. (Code Smell: S2386)
    public static List<string> staticList = new List<string>();

}
