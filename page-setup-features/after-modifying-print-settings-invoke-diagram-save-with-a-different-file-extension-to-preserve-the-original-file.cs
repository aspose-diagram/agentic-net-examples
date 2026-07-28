using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Load the original Visio diagram
        Diagram diagram = new Diagram("original.vsdx");

        // Create and configure print options
        PrintSaveOptions printOptions = new PrintSaveOptions();
        // Example modifications – adjust as needed
        printOptions.EnlargePage = true;                     // Enlarge page to fit content
        printOptions.SaveForegroundPagesOnly = true;         // Save only foreground pages
        printOptions.PageCount = 1;                          // Save only the first page
        // Additional settings can be modified here (e.g., DefaultFont, ExportGuideShapes, etc.)

        // Save the diagram to a different file format (PDF) to preserve the original file
        diagram.Save("original_converted.pdf", printOptions);
    }
}
