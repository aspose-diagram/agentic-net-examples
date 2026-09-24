using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class VisioMasterSummary
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Dictionary to hold master name and its shape count
            Dictionary<string, int> masterShapeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Only consider shapes that are based on a master
                    if (shape.Master != null)
                    {
                        string masterName = shape.Master.NameU;

                        if (masterShapeCounts.ContainsKey(masterName))
                            masterShapeCounts[masterName]++;
                        else
                            masterShapeCounts[masterName] = 1;
                    }
                    else
                    {
                        // Optionally handle shapes without a master (e.g., count as "NoMaster")
                        const string noMasterKey = "NoMaster";
                        if (masterShapeCounts.ContainsKey(noMasterKey))
                            masterShapeCounts[noMasterKey]++;
                        else
                            masterShapeCounts[noMasterKey] = 1;
                    }
                }
            }

            // Write the summary to a text file
            using (StreamWriter writer = new StreamWriter("summary.txt"))
            {
                foreach (KeyValuePair<string, int> entry in masterShapeCounts)
                {
                    writer.WriteLine($"{entry.Key}: {entry.Value}");
                }
            }

            Console.WriteLine("Summary written to summary.txt");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
