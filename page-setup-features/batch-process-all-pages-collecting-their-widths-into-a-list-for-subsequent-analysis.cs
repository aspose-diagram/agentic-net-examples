using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be processed.
                // Replace with the actual file path or pass as a command‑line argument.
                string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // List to hold the width of each page (in inches).
                    List<double> pageWidths = new List<double>();

                    // Iterate over all pages in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the page width from the PageProps section.
                        double width = page.PageSheet.PageProps.PageWidth.Value;
                        pageWidths.Add(width);
                    }

                    // Output the collected widths for verification.
                    Console.WriteLine("Collected page widths (in inches):");
                    for (int i = 0; i < pageWidths.Count; i++)
                    {
                        Console.WriteLine($"Page {i + 1}: {pageWidths[i]}");
                    }

                    // At this point, 'pageWidths' can be used for further analysis.
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }