using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Dictionary to store master name and its usage count
            Dictionary<string, int> masterUsage = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Some shapes (e.g., groups) may not have an associated master
                    if (shape.Master == null)
                        continue;

                    // Get the universal name of the master
                    string masterName = shape.Master.NameU;

                    // Increment usage count for this master
                    if (masterUsage.ContainsKey(masterName))
                        masterUsage[masterName]++;
                    else
                        masterUsage[masterName] = 1;
                }
            }

            // Output the summary report
            Console.WriteLine("Master Usage Frequency Report:");
            foreach (var entry in masterUsage)
            {
                Console.WriteLine($"Master: {entry.Key}, Usage Count: {entry.Value}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
