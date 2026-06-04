using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source VSD file
            string inputFile = "input.vsd";

            // Path where the PNG image will be saved
            string outputFile = "output.png";

            // Load the Visio diagram from the file
            using (Diagram diagram = new Diagram(inputFile))
            {
                // Save the diagram as a PNG image
                diagram.Save(outputFile, SaveFileFormat.Png);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
