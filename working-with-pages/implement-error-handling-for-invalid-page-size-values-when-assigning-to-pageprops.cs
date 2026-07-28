using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                const string inputPath = "input.vsdx";
                // Path for the output Visio file after processing
                const string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Desired page size (in inches)
                double newWidth = 8.27;   // Example: A4 width
                double newHeight = 11.69; // Example: A4 height

                // Validate page size values
                if (!IsValidPageSize(newWidth, newHeight))
                {
                    throw new Exception($"Invalid page dimensions: Width={newWidth}, Height={newHeight}. Both must be greater than zero.");
                }

                // Apply the new size to each page with error handling
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        page.PageSheet.PageProps.PageWidth.Value = newWidth;
                        page.PageSheet.PageProps.PageHeight.Value = newHeight;
                    }
                    catch (Exception ex)
                    {
                        // Log the error and continue with the next page
                        Console.WriteLine($"Failed to set size for page ID {page.ID}: {ex.Message}");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to validate page dimensions
        private static bool IsValidPageSize(double width, double height)
        {
            // Width and height must be positive numbers
            return width > 0 && height > 0;
        }
    }