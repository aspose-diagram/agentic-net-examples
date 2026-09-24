using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Assume VSDX data is available as a byte array.
            // Replace the path with the actual location of your VSDX file or provide the byte array directly.
            byte[] vsdxData = File.ReadAllBytes("sample.vsdx");

            // Initialize a memory stream with the VSDX data.
            using (MemoryStream ms = new MemoryStream(vsdxData))
            {
                try
                {
                    // Load the diagram from the memory stream.
                    Diagram diagram = new Diagram(ms);

                    // Confirm parsing succeeded by checking basic properties.
                    if (diagram != null && diagram.Pages.Count > 0)
                    {
                        Console.WriteLine("Diagram loaded successfully. Page count: " + diagram.Pages.Count);
                    }
                    else
                    {
                        Console.WriteLine("Diagram loaded, but it appears to be empty.");
                    }
                }
                catch (Exception ex)
                {
                    // Parsing failed; output the error.
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
