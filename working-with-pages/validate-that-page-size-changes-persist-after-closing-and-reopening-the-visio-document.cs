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

        // Define temporary output path
        string tempPath = Path.Combine(Path.GetTempPath(), "temp_page_size_test.vsdx");

        // Desired new page dimensions (in inches)
        double newWidth = 8.27;   // A4 width
        double newHeight = 11.69; // A4 height

        try
        {
            // Load the original diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Change page size using PageProps
            page.PageSheet.PageProps.PageWidth.Value = newWidth;
            page.PageSheet.PageProps.PageHeight.Value = newHeight;

            // Save the modified diagram to a temporary file
            diagram.Save(tempPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during modification and save: {ex.Message}");
            return;
        }

        // Verify that the changes persisted after reopening
        try
        {
            // Re-open the saved diagram
            Diagram reopenedDiagram = new Diagram(tempPath);

            // Access the first page again
            Page reopenedPage = reopenedDiagram.Pages[0];

            // Retrieve the saved dimensions
            double savedWidth = reopenedPage.PageSheet.PageProps.PageWidth.Value;
            double savedHeight = reopenedPage.PageSheet.PageProps.PageHeight.Value;

            // Simple tolerance for floating point comparison
            const double tolerance = 0.001;

            // Validate width
            if (Math.Abs(savedWidth - newWidth) > tolerance)
            {
                throw new Exception($"Page width mismatch. Expected: {newWidth}, Actual: {savedWidth}");
            }

            // Validate height
            if (Math.Abs(savedHeight - newHeight) > tolerance)
            {
                throw new Exception($"Page height mismatch. Expected: {newHeight}, Actual: {savedHeight}");
            }

            Console.WriteLine("Page size change persisted successfully after reopening the document.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Validation error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary file
            try
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
            catch
            {
                // Suppress any cleanup errors
            }
        }
    }
}