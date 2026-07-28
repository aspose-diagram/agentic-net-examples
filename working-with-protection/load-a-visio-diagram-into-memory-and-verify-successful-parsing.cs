using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = "sample.vsdx";

            // Detect the file format before loading (optional verification step)
            var formatInfo = FileFormatUtil.DetectFileFormat(filePath);
            Console.WriteLine($"Detected format: {formatInfo.FileFormatType}");

            Diagram diagram = null;
            try
            {
                // Load the diagram into memory using the file‑path constructor
                diagram = new Diagram(filePath);

                // Simple verification: ensure the diagram contains at least one page
                if (diagram.Pages.Count > 0)
                {
                    Console.WriteLine($"Diagram loaded successfully. Pages count: {diagram.Pages.Count}");
                }
                else
                {
                    Console.WriteLine("Diagram loaded but contains no pages.");
                }
            }
            catch (Exception ex)
            {
                // Loading failed – output the error message
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
            }
            finally
            {
                // Release resources
                diagram?.Dispose();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
