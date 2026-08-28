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
            string networkFilePath = @"\\ServerName\ShareFolder\Diagram.vsd";

            // Load the Visio diagram from the network location.
            // The LoadFileFormat.Vsd enum value specifies that the source file is a binary VSD file.
            using (Diagram diagram = new Diagram(networkFilePath, LoadFileFormat.Vsd))
            {
                // Save the diagram as CSV.
                // Aspose.Diagram does not provide a dedicated CSV format in SaveFileFormat,
                // so here we demonstrate saving using a supported format (e.g., VDX) as a placeholder.
                // Replace SaveFileFormat.Vdx with the appropriate CSV format if it becomes available.
                string outputCsvPath = @"C:\Output\Diagram.csv";
                diagram.Save(outputCsvPath, SaveFileFormat.Csv);
            }

        }
        catch (System.IO.IOException ex)
        {
            Console.Error.WriteLine($"[IOException] {ex.Message}");
        }
    }
}
