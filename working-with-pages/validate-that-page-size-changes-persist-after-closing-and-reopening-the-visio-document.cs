using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define new page dimensions (in inches)
        double newWidth = 8.27;   // A4 width
        double newHeight = 11.69; // A4 height

        // Temporary output file path
        string outputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".vsdx");

        try
        {
            // Load the original diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Change page size
            page.PageSheet.PageProps.PageWidth.Value = newWidth;
            page.PageSheet.PageProps.PageHeight.Value = newHeight;

            // Save the modified diagram to a temporary file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during modification and save: {ex.Message}");
            return;
        }

        // Verify that the saved file exists
        if (!File.Exists(outputPath))
        {
            Console.Error.WriteLine($"Failed to create output file: {outputPath}");
            return;
        }

        try
        {
            // Re-open the saved diagram
            Diagram reopenedDiagram = new Diagram(outputPath);
            Page reopenedPage = reopenedDiagram.Pages[0];

            // Retrieve the persisted page dimensions
            double persistedWidth = reopenedPage.PageSheet.PageProps.PageWidth.Value;
            double persistedHeight = reopenedPage.PageSheet.PageProps.PageHeight.Value;

            // Validate that the dimensions match the expected values (tolerance of 0.001 inches)
            const double tolerance = 0.001;
            bool widthMatches = Math.Abs(persistedWidth - newWidth) <= tolerance;
            bool heightMatches = Math.Abs(persistedHeight - newHeight) <= tolerance;

            if (widthMatches && heightMatches)
            {
                Console.WriteLine("Page size change persisted successfully.");
                Console.WriteLine($"Width: {persistedWidth} inches, Height: {persistedHeight} inches");
            }
            else
            {
                throw new Exception($"Page size persistence validation failed. Expected ({newWidth}, {newHeight}) but got ({persistedWidth}, {persistedHeight}).");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during validation: {ex.Message}");
            return;
        }
        finally
        {
            // Clean up the temporary file
            try
            {
                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }
            }
            catch
            {
                // Suppress any cleanup errors
            }
        }
    }
}