using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source diagram and the output diagram
                string sourcePath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Index of the template page (zero‑based). Adjust as needed.
                int templatePageIndex = 0;

                // Indices of pages that should receive the template size.
                // Example: copy to pages 2, 3 and 4 (zero‑based indices 1, 2, 3)
                List<int> targetPageIndices = new List<int> { 1, 2, 3 };

                // Retrieve the template page
                Page templatePage = diagram.Pages[templatePageIndex];

                // Read width and height from the template page (values are in inches)
                double templateWidth = templatePage.PageSheet.PageProps.PageWidth.Value;
                double templateHeight = templatePage.PageSheet.PageProps.PageHeight.Value;

                // Apply the template size to each target page
                foreach (int idx in targetPageIndices)
                {
                    // Ensure the index is within the collection bounds
                    if (idx >= 0 && idx < diagram.Pages.Count)
                    {
                        Page targetPage = diagram.Pages[idx];
                        targetPage.PageSheet.PageProps.PageWidth.Value = templateWidth;
                        targetPage.PageSheet.PageProps.PageHeight.Value = templateHeight;
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Page index {idx} is out of range.");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Page sizes have been copied and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }