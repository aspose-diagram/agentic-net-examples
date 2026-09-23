using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Custom company name for the center header
            string companyName = "Acme Corporation";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Update the center header text
            diagram.HeaderFooter.HeaderCenter = companyName;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
