using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramEventMacroUpdater <inputFile> <outputFile>");
            return;
        }

        string inputPath = args[0];
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the diagram from the provided file
            Diagram diagram = new Diagram(inputPath);

            // Ensure a VBA project exists; abort if missing (cannot create a new one)
            if (diagram.VbaProject == null)
            {
                Console.Error.WriteLine("Failed to access VbaProject.");
                return;
            }

            // Add a new VBA module containing the custom macro
            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "CustomMacroModule");
            VbaModule macroModule = diagram.VbaProject.Modules[moduleIndex];
            macroModule.Codes = @"
Public Sub MyCustomMacro()
    MsgBox ""Custom macro executed!""
End Sub
";

            // Iterate through all pages and shapes, applying a macro-triggering event
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // NOTE: The original EventMouseEnter cell does not exist in Aspose.Diagram.
                    // Using EventDblClick as a representative event cell that is supported.
                    shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"MyCustomMacro\")";
                }
            }

            // Save the modified diagram in a macro-enabled format
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved with updated event cells to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}