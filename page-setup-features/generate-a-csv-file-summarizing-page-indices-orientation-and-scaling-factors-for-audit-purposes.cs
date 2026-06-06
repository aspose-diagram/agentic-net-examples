using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string visioPath = "input.vsdx";

                // Output CSV file path
                string csvPath = "PageAudit.csv";

                // Ensure the diagram is disposed properly
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Prepare to write CSV
                    using (StreamWriter writer = new StreamWriter(csvPath, false))
                    {
                        // Write CSV header
                        writer.WriteLine("PageIndex,Orientation,ScaleX,ScaleY");

                        // Iterate through each page in the diagram
                        foreach (Page page in diagram.Pages)
                        {
                            // Page index (using the page's ID for uniqueness)
                            int pageIndex = page.ID;

                            // Orientation: Landscape, Portrait, or SameAsPrinter
                            string orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value.ToString();

                            // Scaling factors (default to 1.0 if not set)
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