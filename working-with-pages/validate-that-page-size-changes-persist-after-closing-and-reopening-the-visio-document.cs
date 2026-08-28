using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (required argument)
        string inputPath = args.Length > 0 ? args[0] : "";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Temporary file to store the modified diagram
        string tempPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "temp_modified.vsdx");

        // Desired page dimensions (A4 size in inches)
        double targetWidth = 8.27;
        double targetHeight = 11.69;

        try
        {
            // Load the original diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Set new page width and height using the correct cell paths
            page.PageSheet.PageProps.PageWidth.Value = targetWidth;
            page.PageSheet.PageProps.PageHeight.Value = targetHeight;

            // Save the modified diagram to a temporary file in VSDX format
            diagram.Save(tempPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during modification/saving
            Console.Error.WriteLine($"Error during modification: {ex.Message}");
            return;
        }

        try
        {
            // Re-open the saved diagram to verify persistence
            Diagram reopenedDiagram = new Diagram(tempPath);
            Page reopenedPage = reopenedDiagram.Pages[0];

            // Retrieve the persisted dimensions
            double persistedWidth = reopenedPage.PageSheet.PageProps.PageWidth.Value;
            double persistedHeight = reopenedPage.PageSheet.PageProps.PageHeight.Value;

            // Define a tolerance for floating‑point comparison
            const double tolerance = 0.001;

            // Validate width
            bool widthMatches = Math.Abs(persistedWidth - targetWidth) <= tolerance;
            // Validate height
            bool heightMatches = Math.Abs(persistedHeight - targetHeight) <= tolerance;

            if (widthMatches && heightMatches)
            {
                Console.WriteLine("Page size change persisted successfully.");
                Console.WriteLine($"Width: {persistedWidth} inches, Height: {persistedHeight} inches");
            }
            else
            {
                // Throw an exception to indicate validation failure
                throw new Exception($"Page size mismatch after reload. Expected ({targetWidth}, {targetHeight}) but got ({persistedWidth}, {persistedHeight}).");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during verification
            Console.Error.WriteLine($"Verification error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary file if it exists
            if (File.Exists(tempPath))
            {
                try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}