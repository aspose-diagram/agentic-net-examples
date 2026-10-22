using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdm>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains a VBA project; if not, report and exit.
                if (diagram.VbaProject == null)
                {
                    Console.Error.WriteLine("The diagram does not contain a VBA project. Cannot add macro.");
                    return;
                }

                // Add a new procedural VBA module to the project.
                // VbaModuleType.Procedural indicates a standard code module.
                int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "TimestampModule");

                // Retrieve the newly added module.
                VbaModule timestampModule = diagram.VbaProject.Modules[moduleIndex];

                // VBA code that adds a timestamp paragraph to every newly added shape.
                timestampModule.Codes = @"
Public Sub Document_ShapeAdded(ByVal Shape As Visio.Shape)
    Dim para As Visio.Paragraph
    Set para = Shape.Characters.Paragraphs.Add
    para.Text = ""Created: "" & Format(Now, ""yyyy-mm-dd hh:nn:ss"")
End Sub
";

                // Save the modified diagram in a macro‑enabled format.
                diagram.Save(outputPath, SaveFileFormat.Vsdm);
            }

            Console.WriteLine("Macro added and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}