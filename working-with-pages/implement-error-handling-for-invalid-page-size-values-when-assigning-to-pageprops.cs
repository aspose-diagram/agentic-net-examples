using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    // Validates and sets page size for all pages in the diagram.
    // Width and height must be positive numbers (in inches).
    static void SetPageSize(Diagram diagram, double widthInches, double heightInches)
    {
        // Basic validation
        if (widthInches <= 0)
            throw new Exception($"Invalid page width: {widthInches}. Width must be greater than zero.");
        if (heightInches <= 0)
            throw new Exception($"Invalid page height: {heightInches}. Height must be greater than zero.");

        // Apply size to each page
        foreach (Page page in diagram.Pages)
        {
            // Assign values via the .Value property as required by Aspose.Diagram
            page.PageSheet.PageProps.PageWidth.Value = widthInches;
            page.PageSheet.PageProps.PageHeight.Value = heightInches;
        }
    }

    static void Main()
    {
        try
        {
            // Load an existing Visio diagram (replace with actual path)
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Example: set A4 size (8.27 x 11.69 inches)
                SetPageSize(diagram, 8.27, 11.69);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram page size updated and saved successfully.");
        }
        catch (Exception ex)
        {
            // Handle any errors (e.g., file not found, invalid sizes)
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
