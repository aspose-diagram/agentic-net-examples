using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.pdf";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Diagram diagram = null;

        // Load the Visio diagram inside a try‑catch to handle loading errors
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Prepare PDF save options (no special settings required for basic conversion)
        PdfSaveOptions pdfOptions = new PdfSaveOptions();

        FileStream fileStream = null;

        // Perform the conversion inside a try‑catch and ensure the stream is closed in finally
        try
        {
            // Open a FileStream for the output PDF
            fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);

            // Save the diagram to the stream using the PDF options
            diagram.Save(fileStream, pdfOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
        finally
        {
            // Ensure the FileStream is properly closed even if an exception occurs
            if (fileStream != null)
            {
                try
                {
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error closing FileStream: {ex.Message}");
                }
            }
        }
    }
}