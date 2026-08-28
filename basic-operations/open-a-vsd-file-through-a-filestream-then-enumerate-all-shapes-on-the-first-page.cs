using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (VSD format)
            string visioFilePath = "example.vsd";

            // Open the VSD file using a FileStream
            using (FileStream fs = new FileStream(visioFilePath, FileMode.Open, FileAccess.Read))
            {
                // Load the diagram from the stream
                using (Diagram diagram = new Diagram(fs))
                {
                    // Ensure there is at least one page
                    if (diagram.Pages.Count > 0)
                    {
                        // Get the first page (index 0)
                        Page firstPage = diagram.Pages[0];

                        // Enumerate all shapes on the first page
                        foreach (Shape shape in firstPage.Shapes)
                        {
                            // Example: output shape ID and name
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
