using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load VSDX file bytes (replace with your actual VSDX data source)
            byte[] vsdxBytes = File.ReadAllBytes("sample.vsdx");

            // Create a memory stream containing the VSDX data
            using (MemoryStream memoryStream = new MemoryStream(vsdxBytes))
            {
                // Initialize Diagram object from the memory stream
                Diagram diagram = new Diagram(memoryStream);

                // Confirm parsing succeeded by checking that at least one page is present
                if (diagram.Pages != null && diagram.Pages.Count > 0)
                {
                    Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
                }
                else
                {
                    Console.WriteLine("Diagram loaded, but no pages were found.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
