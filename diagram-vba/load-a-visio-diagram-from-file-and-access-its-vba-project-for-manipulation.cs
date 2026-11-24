using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdm");

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the VBA project (read‑only property)
                VbaProject vbaProject = diagram.VbaProject;
                if (vbaProject == null)
                {
                    Console.WriteLine("The diagram does not contain a VBA project.");
                }
                else
                {
                    // Output basic VBA project information
                    Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
                    Console.WriteLine($"Is Signed: {vbaProject.IsSigned}");

                    // List existing VBA modules
                    Console.WriteLine("Existing VBA Modules:");
                    foreach (VbaModule module in vbaProject.Modules)
                    {
                        Console.WriteLine($"- Module Name: {module.Name}");
                        Console.WriteLine($"  Code Length: {module.Codes?.Length ?? 0} characters");
                    }

                    // Add a new procedural VBA module
                    int newModuleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, "NewModule");
                    VbaModule newModule = vbaProject.Modules[newModuleIndex];
                    // Set simple VBA code for the new module
                    newModule.Codes = @"
Sub HelloWorld()
    MsgBox ""Hello from Aspose.Diagram!""
End Sub
";

                    Console.WriteLine($"Added new VBA module: {newModule.Name}");
                }

                // Save the modified diagram in a macro‑enabled format (VSDM)
                diagram.Save(outputPath, SaveFileFormat.Vsdm);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}