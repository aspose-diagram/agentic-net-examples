using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Open the diagram in read‑only mode using a FileStream with FileAccess.Read.
            using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(fs))
                {
                    // Iterate over all pages in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate over all shapes on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // If the shape hosts an ActiveX control, disable it.
                            if (shape.ActiveXControl != null)
                            {
                                // The IsEnabled property is a native bool, so assign false directly.
                                shape.ActiveXControl.IsEnabled = false;
                            }
                        }
                    }

                    // Save the modified diagram to the specified output path.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}