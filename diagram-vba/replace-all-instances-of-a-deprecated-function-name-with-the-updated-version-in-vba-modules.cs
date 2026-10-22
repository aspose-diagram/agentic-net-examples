using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <outputVisioPath> <oldFunctionName> <newFunctionName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string oldFunctionName = args[2];
        string newFunctionName = args[3];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram (supports macro‑enabled formats).
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains a VBA project.
            if (diagram.VbaProject == null)
            {
                Console.Error.WriteLine("The diagram does not contain a VBA project.");
                return;
            }

            // Iterate through all VBA modules and replace the deprecated function name.
            foreach (VbaModule module in diagram.VbaProject.Modules)
            {
                if (string.IsNullOrEmpty(module.Codes))
                    continue; // Skip empty modules.

                // Perform a simple string replacement.
                string updatedCode = module.Codes.Replace(oldFunctionName, newFunctionName);

                // Update the module's code if a change occurred.
                if (!updatedCode.Equals(module.Codes, StringComparison.Ordinal))
                {
                    module.Codes = updatedCode;
                    Console.WriteLine($"Updated module: {module.Name}");
                }
            }

            // Save the modified diagram in a macro‑enabled format to preserve VBA code.
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}