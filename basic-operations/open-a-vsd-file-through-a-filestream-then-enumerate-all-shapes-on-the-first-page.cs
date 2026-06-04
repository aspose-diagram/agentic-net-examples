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

            // Open the VSD file via a FileStream
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Load the diagram from the stream using the Diagram(Stream) constructor
                Diagram diagram = new Diagram(stream);

                // Access the first page (index 0) of the diagram
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
