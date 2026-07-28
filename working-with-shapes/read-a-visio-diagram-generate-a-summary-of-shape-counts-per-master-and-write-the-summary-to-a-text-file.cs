using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class VisioShapeSummary
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string visioPath = "input.vsdx";

            // Output summary text file path
            string summaryPath = "shape_summary.txt";

            // Load the Visio diagram using the provided constructor (lifecycle rule)
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Dictionary to hold shape counts per master name
                Dictionary<string, int> masterShapeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Determine the master name; if the shape has no master, use a placeholder
                        string masterName = shape.Master != null && !string.IsNullOrEmpty(shape.Master.NameU)
                            ? shape.Master.NameU
                            : "NoMaster";

                        // Increment the count for this master
                        if (masterShapeCounts.ContainsKey(masterName))
                            masterShapeCounts[masterName]++;
                        else
                            masterShapeCounts[masterName] = 1;
                    }
                }

                // Write the summary to a text file (free‑form code, no specific rule needed)
                using (StreamWriter writer = new StreamWriter(summaryPath))
                {
                    writer.WriteLine("Shape Count per Master:");
                    foreach (var kvp in masterShapeCounts)
                    {
                        writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
