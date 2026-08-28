using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be audited
                string visioPath = "input.vsdx";

                // Path to the CSV report to generate
                string csvPath = "PageAuditReport.csv";

                // Ensure the diagram is properly disposed after use
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Open a StreamWriter for the CSV file
                    using (StreamWriter writer = new StreamWriter(csvPath, false))
                    {
                        // Write CSV header
                        writer.WriteLine("PageIndex,Orientation,ScaleX,ScaleY");

                        // Iterate through each page in the diagram
                        foreach (Page page in diagram.Pages)
                        {
                            // Page index (using the page's ID)
                            int pageIndex = page.ID;

                            // Orientation: Landscape, Portrait, or SameAsPrinter
                            string orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value.ToString();

                            // Scaling factors (default to 1.0 if not set)
                            double scaleX = page.PageSheet.PrintProps.ScaleX.Value;
                            double scaleY = page.PageSheet.PrintProps.ScaleY.Value;

                            // Write the data row
                            writer.WriteLine($"{pageIndex},{orientation},{scaleX},{scaleY}");
                        }
                    }
                }

                Console.WriteLine("CSV audit report generated successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }