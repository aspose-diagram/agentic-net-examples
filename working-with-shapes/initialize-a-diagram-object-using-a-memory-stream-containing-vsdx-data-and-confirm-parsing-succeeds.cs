using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load VSDX binary data into a byte array (replace with actual data source)
            byte[] vsdxData = File.ReadAllBytes("sample.vsdx");

            // Initialize a memory stream with the VSDX data
            using (MemoryStream ms = new MemoryStream(vsdxData))
            {
                try
                {
                    // Create Diagram object from the memory stream
                    Diagram diagram = new Diagram(ms);

                    // Simple validation to confirm parsing succeeded
                    if (diagram != null && diagram.Pages.Count > 0)
                    {
                        Console.WriteLine("Diagram loaded successfully. Page count: " + diagram.Pages.Count);
                    }
                    else
                    {
                        Console.WriteLine("Diagram loaded, but no pages were found.");
                    }
                }
                catch (Exception ex)
                {
                    // If parsing fails, an exception will be thrown
                    Console.WriteLine("Failed to load diagram: " + ex.Message);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
