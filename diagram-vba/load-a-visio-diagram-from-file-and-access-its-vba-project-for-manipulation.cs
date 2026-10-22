using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdm";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

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
                newModule.Codes = @"Sub HelloWorld()
    MsgBox ""Hello from Aspose.Diagram!""
End Sub";

                Console.WriteLine($"Added new VBA module: {newModule.Name}");
            }

            // Save the modified diagram in a macro‑enabled format (VSDM)
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during Aspose operations
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}