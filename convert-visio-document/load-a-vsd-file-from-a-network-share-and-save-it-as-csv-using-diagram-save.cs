using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VSD file on a network share
            string inputPath = @"\\ServerName\ShareFolder\Diagram.vsd";

            // Desired output CSV file path
            string outputPath = @"C:\Temp\Diagram.csv";

            // Load the Visio diagram from the network location.
            // The constructor with (string, LoadFileFormat) explicitly specifies the VSD format.
            Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsd);

            // Save the diagram as CSV.
            // Assuming Aspose.Diagram supports CSV via the SaveFileFormat enumeration.
            diagram.Save(outputPath, SaveFileFormat.Csv);

        }
        catch (System.IO.IOException ex)
        {
            Console.Error.WriteLine($"[IOException] {ex.Message}");
        }
    }
}
