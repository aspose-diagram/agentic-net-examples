using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provided via command line or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdm";

        // Guard: ensure the source diagram exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram already contains a VBA project; cannot create a new one
                if (diagram.VbaProject == null)
                {
                    Console.Error.WriteLine("Error: The diagram does not contain a VBA project. Cannot add macro.");
                    return;
                }

                // VBA macro that adds a timestamp paragraph to every new shape
                string macroCode = @"
Public Sub Document_ShapeAdded(ByVal Shape As IVShape)
    Dim ts As String
    ts = ""Timestamp: "" & Now
    Shape.Text = ts
End Sub
";

                // Add a new procedural VBA module and set its code
                int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "TimestampModule");
                VbaModule module = diagram.VbaProject.Modules[moduleIndex];
                module.Codes = macroCode;

                // Save the modified diagram with the macro embedded in a macro-enabled format
                diagram.Save(outputPath, SaveFileFormat.Vsdm);
            }

            Console.WriteLine($"Diagram saved with timestamp macro: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}