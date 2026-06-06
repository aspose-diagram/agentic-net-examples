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

            // Load the original Visio diagram
            Diagram diagram = new Diagram("original.vsdx");

            // Create and configure print options
            PrintSaveOptions printOptions = new PrintSaveOptions();
            // Example modifications – adjust as needed
            printOptions.IsExportComments = true;          // export comments
            printOptions.ExportGuideShapes = true;         // export guide shapes
            printOptions.EnlargePage = true;               // enlarge page if required
            // printOptions.PageSize = new PageSize(8.5, 11); // optional page size

            // Save the diagram using a different file extension (PDF) to keep the original file intact
            diagram.Save("original_modified.pdf", printOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
