using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportVisibleLayersToVdx
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file (any supported format, e.g., VSDX)
            string inputFile = "input.vsdx";

            // Path for the legacy VDX output file
            string outputFile = "output.vdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputFile);

            // Create save options specifying VDX format
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);

            // Save the diagram using the specified options.
            // This will produce a VDX file compatible with older Visio versions.
            diagram.Save(outputFile, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
