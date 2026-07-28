using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Expect the diagram file path as the first command‑line argument
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: DiagramPageWidthCollector <path-to-diagram>");
                    return;
                }

                string diagramPath = args[0];

                // Load the Visio diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // List to hold the width of each page (in inches)
                    List<double> pageWidths = new List<double>();

                    // Iterate over all pages explicitly typing the iterator as Page
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the page width from the PageProps cell collection
                        double width = page.PageSheet.PageProps.PageWidth.Value;
                        pageWidths.Add(width);
                    }

                    // Output the collected widths for verification or further analysis
                    Console.WriteLine("Collected page widths (in inches):");
                    for (int i = 0; i < pageWidths.Count; i++)
                    {
                        Console.WriteLine($"Page {i + 1}: {pageWidths[i]}");
                    }

                    // The pageWidths list can now be used for any subsequent analysis
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }