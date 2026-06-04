using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file (VSD)
            string inputFile = "input.vsd";

            // Output PDF file
            string outputFile = "output.pdf";

            // Load the diagram from the VSD file
            using (Diagram diagram = new Diagram(inputFile))
            {
                // Save the diagram as PDF using the Save method with SaveFileFormat.Pdf
                diagram.Save(outputFile, SaveFileFormat.Pdf);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
