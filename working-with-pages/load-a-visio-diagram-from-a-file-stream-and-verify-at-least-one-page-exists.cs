using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string filePath = "sample.vsdx";

            // Open the file as a read‑only stream
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Load the diagram from the stream using the Diagram(Stream) constructor
                Diagram diagram = new Diagram(stream);

                // Verify that the diagram contains at least one page
                if (diagram.Pages != null && diagram.Pages.Count > 0)
                {
                    Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
                }
                else
                {
                    Console.WriteLine("The diagram does not contain any pages.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
