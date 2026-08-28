using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Open a file stream to the Visio file
            using (FileStream stream = new FileStream("sample.vsdx", FileMode.Open, FileAccess.Read))
            {
                // Load the diagram from the stream using the Diagram(Stream) constructor
                Diagram diagram = new Diagram(stream);

                // Verify that the diagram contains at least one page
                if (diagram.Pages.Count > 0)
                {
                    Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
                }
                else
                {
                    Console.WriteLine("Diagram loaded but contains no pages.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
