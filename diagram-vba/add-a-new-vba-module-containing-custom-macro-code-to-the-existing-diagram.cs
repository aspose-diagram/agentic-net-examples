using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has a VBA project (macro‑enabled formats support this automatically).
                // Add a new procedural VBA module named "CustomMacroModule".
                int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "CustomMacroModule");

                // Retrieve the newly added module.
                VbaModule module = diagram.VbaProject.Modules[moduleIndex];

                // Define custom VBA macro code.
                string vbaCode = @"
Sub HelloWorld()
    MsgBox ""Hello from VBA!""
End Sub
";

                // Assign the VBA code to the module.
                module.Codes = vbaCode.Trim();

                // Save the diagram in a macro‑enabled format (VSDM) to preserve the VBA project.
                diagram.Save(outputPath, SaveFileFormat.Vsdm);
            }

            Console.WriteLine($"Diagram saved successfully with new VBA module: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}