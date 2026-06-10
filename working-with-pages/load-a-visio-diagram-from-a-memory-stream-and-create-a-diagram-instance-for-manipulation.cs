using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load Visio file bytes into a memory stream (could be from any source)
            byte[] visioBytes = File.ReadAllBytes("sample.vsdx");
            using (MemoryStream memoryStream = new MemoryStream(visioBytes))
            {
                // Ensure the stream is positioned at the beginning before loading
                memoryStream.Position = 0;

                // Create a Diagram instance directly from the memory stream
                Diagram diagram = new Diagram(memoryStream);

                // Diagram is now ready for manipulation; example: output page count
                Console.WriteLine($"Number of pages in diagram: {diagram.Pages.Count}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
