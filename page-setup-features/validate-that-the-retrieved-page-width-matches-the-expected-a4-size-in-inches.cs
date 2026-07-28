using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file to be validated
                string diagramPath = "input.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Retrieve the first page (index 0)
                    Page page = diagram.Pages[0];

                    // Get the page width in inches
                    double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

                    // Expected A4 width in inches (8.27 inches)
                    const double expectedA4Width = 8.27;

                    // Allow a small tolerance for floating‑point differences
                    const double tolerance = 0.01;

                    // Validate the width
                    if (Math.Abs(pageWidthInches - expectedA4Width) > tolerance)
                    {
                        // Width does not match the expected A4 size – raise an error
                        throw new Exception(
                            $"Page width validation failed. Actual: {pageWidthInches} inches, Expected: {expectedA4Width} inches.");
                    }
                    else
                    {
                        // Width matches the expected A4 size
                        Console.WriteLine(
                            $"Page width validation succeeded. Width: {pageWidthInches} inches matches A4 size.");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }