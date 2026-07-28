using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file to be audited
                const string diagramPath = "input.vsdx";
                // Output CSV file path
                const string csvPath = "audit_pages.csv";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Prepare the CSV file
                    using (StreamWriter writer = new StreamWriter(csvPath, false))
                    {
                        // Write CSV header
                        writer.WriteLine("PageIndex,Orientation,ScaleX,ScaleY");

                        // Iterate over each page in the diagram
                        foreach (Page page in diagram.Pages)
                        {
                            // Page index (ID)
                            int pageIndex = page.ID;

                            // Orientation (Landscape, Portrait, SameAsPrinter)
                            string orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value.ToString();

                            // Scaling factors (default 1.0 if not set)
                            double scaleX = page.PageSheet.PrintProps.ScaleX.Value;
                            double scaleY = page.PageSheet.PrintProps.ScaleY.Value;

                            // Write a CSV line for the current page
                            writer.WriteLine($"{pageIndex},{orientation},{scaleX},{scaleY}");
                        }
                    }
                }

                Console.WriteLine($"Audit CSV generated at: {Path.GetFullPath(csvPath)}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }