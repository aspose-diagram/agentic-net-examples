using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class VisioShapeSummary
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputFile = "input.vsdx";

            // Output summary text file path
            string outputFile = "summary.txt";

            // Load the Visio diagram from file (uses Diagram(string) constructor)
            Diagram diagram = new Diagram(inputFile);

            // Dictionary to hold shape count per master (key: master name)
            Dictionary<string, int> masterShapeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Initialize dictionary entries for all masters present in the document
            foreach (Master master in diagram.Masters)
            {
                string masterName = !string.IsNullOrEmpty(master.NameU) ? master.NameU : master.Name;
                if (!string.IsNullOrEmpty(masterName) && !masterShapeCounts.ContainsKey(masterName))
                {
                    masterShapeCounts[masterName] = 0;
                }
            }

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Each shape may reference a master; if so, count it
                    Master shapeMaster = shape.Master;
                    if (shapeMaster != null)
                    {
                        string masterName = !string.IsNullOrEmpty(shapeMaster.NameU) ? shapeMaster.NameU : shapeMaster.Name;
                        if (string.IsNullOrEmpty(masterName))
                            continue; // skip if master has no identifiable name

                        // Ensure the master is present in the dictionary
                        if (!masterShapeCounts.ContainsKey(masterName))
                        {
                            masterShapeCounts[masterName] = 0;
                        }

                        masterShapeCounts[masterName]++;
                    }
                }
            }

            // Write the summary to a text file
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                writer.WriteLine("Shape Count per Master:");
                foreach (var kvp in masterShapeCounts)
                {
                    writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }

            // Optional: inform the user
            Console.WriteLine($"Summary written to '{outputFile}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
