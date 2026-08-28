using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // List to hold report lines
                    List<string> reportLines = new List<string>();

                    // Iterate through each page
                    foreach (Page page in diagram.Pages)
                    {
                        // Capture original dimensions (in inches)
                        double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Define new dimensions (example: increase each side by 1 inch)
                        double newWidth = originalWidth + 1.0;
                        double newHeight = originalHeight + 1.0;

                        // Apply new dimensions
                        page.PageSheet.PageProps.PageWidth.Value = newWidth;
                        page.PageSheet.PageProps.PageHeight.Value = newHeight;

                        // Record the change in the report
                        string line = $"Page ID {page.ID} (Name: {page.Name}) - Original: {originalWidth:F2}\" x {originalHeight:F2}\" , New: {newWidth:F2}\" x {newHeight:F2}\"";
                        reportLines.Add(line);
                    }

                    // Output the report to console
                    Console.WriteLine("=== Page Size Modification Report ===");
                    foreach (string line in reportLines)
                    {
                        Console.WriteLine(line);
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Modified diagram saved to: {outputPath}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }