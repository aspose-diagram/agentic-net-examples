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
            string filePath = "example.vsdx";

            // Open the file as a read‑only stream
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Load the Visio diagram from the stream using the Diagram(Stream) constructor
                using (Diagram diagram = new Diagram(stream))
                {
                    // Ensure the document contains at least one page
                    if (diagram.Pages.Count > 0)
                    {
                        // Get the first page (you can iterate over all pages if needed)
                        Page page = diagram.Pages[0];

                        // Iterate through the shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Example: output basic shape information
                            Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The diagram contains no pages.");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
