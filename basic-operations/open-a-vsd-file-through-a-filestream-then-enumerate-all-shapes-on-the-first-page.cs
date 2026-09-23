using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VSD file
            string filePath = "sample.vsd";

            // Open the VSD file through a FileStream
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Load the diagram from the stream
                Diagram diagram = new Diagram(fs);

                // Get the first page (index 0)
                Page firstPage = diagram.Pages[0];

                // Enumerate all shapes on the first page
                foreach (Shape shape in firstPage.Shapes)
                {
                    // Example output: shape ID and name
                    Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
