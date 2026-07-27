using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VsdToPdfConverter
{
    static void Main()
    {
        try
        {

            // Path to the source VSD file
            string inputFile = "input.vsd";

            // Path where the PDF will be saved
            string outputFile = "output.pdf";

            // Load the Visio diagram from the file
            Diagram diagram = new Diagram(inputFile);

            // Save the diagram as PDF using the SaveFileFormat enum
            diagram.Save(outputFile, SaveFileFormat.Pdf);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
