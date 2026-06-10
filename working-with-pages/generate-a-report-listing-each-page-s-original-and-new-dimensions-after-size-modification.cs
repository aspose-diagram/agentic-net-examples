using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file (provide path as first argument or modify the literal)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                // Output Visio file after resizing
                string outputPath = "resized_output.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // List to hold report lines
                    List<string> reportLines = new List<string>();
                    reportLines.Add("Page Resize Report");
                    reportLines.Add("-------------------");

                    // Define resize factor (e.g., increase size by 20%)
                    const double resizeFactor = 1.20;

                    // Iterate through each page
                    foreach (Page page in diagram.Pages)
                    {
                        // Capture original dimensions (in inches)
                        double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Compute new dimensions
                        double newWidth = originalWidth * resizeFactor;
                        double newHeight = originalHeight * resizeFactor;

                        // Apply new dimensions
                        page.PageSheet.PageProps.PageWidth.Value = newWidth;
                        page.PageSheet.PageProps.PageHeight.Value = newHeight;

                        // Add entry to report
                        string line = $"Page ID {page.ID} ('{page.Name}'): " +
                                      $"Original = {originalWidth:F2}in x {originalHeight:F2}in, " +
                                      $"New = {newWidth:F2}in x {newHeight:F2}in";
                        reportLines.Add(line);
                    }

                    // Output report to console
                    foreach (string line in reportLines)
                    {
                        Console.WriteLine(line);
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }