using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram path and output diagram path are required.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Determine if the file is marked as read‑only on the file system.
                bool isReadOnly = (File.GetAttributes(inputPath) & FileAttributes.ReadOnly) != 0;

                if (isReadOnly)
                {
                    // Iterate over all pages.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate over all shapes on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // If the shape contains an ActiveX control, disable it.
                            if (shape.ActiveXControl != null)
                            {
                                // The IsEnabled property is a native bool, so assign false directly.
                                shape.ActiveXControl.IsEnabled = false;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}