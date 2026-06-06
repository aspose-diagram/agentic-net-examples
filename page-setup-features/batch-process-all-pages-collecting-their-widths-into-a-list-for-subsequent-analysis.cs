using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // List to store page widths
                List<double> pageWidths = new List<double>();

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Iterate over each page explicitly typed as Aspose.Diagram.Page
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the page width (in inches) from the PageProps cell
                        double width = page.PageSheet.PageProps.PageWidth.Value;

                        // Add the width to the collection
                        pageWidths.Add(width);
                    }
                }

                // Output the collected widths for verification
                Console.WriteLine("Collected page widths:");
                for (int i = 0; i < pageWidths.Count; i++)
                {
                    Console.WriteLine($"Page {i + 1}: {pageWidths[i]} inches");
                }

                // Further analysis can be performed using the pageWidths list

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }