using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Add a new VBA module of type Procedural with the name "MyMacroModule".
            int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "MyMacroModule");

            // Retrieve the newly added module.
            VbaModule module = diagram.VbaProject.Modules[moduleIndex];

            // Define the VBA macro code.
            string vbaCode = @"
Attribute VB_Name = ""MyMacroModule""
Sub HelloWorld()
    MsgBox ""Hello from Aspose.Diagram!""
End Sub
";

            // Assign the code to the module.
            module.Codes = vbaCode;

            // Validate the VBA code syntax (basic validation).
            if (!ValidateVbaCode(module.Codes))
            {
                // Validation failed – abort the operation.
                diagram.Dispose();
                return;
            }

            // Save the diagram in a macro‑enabled format.
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

            // Clean up resources.
            diagram.Dispose();

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to count occurrences of a substring within a string.
    private static int CountOccurrences(string source, string substring)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(substring))
            return 0;

        int count = 0;
        int index = 0;
        while ((index = source.IndexOf(substring, index, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            count++;
            index += substring.Length;
        }
        return count;
    }

    // Basic VBA syntax validation as per the requirements.
    private static bool ValidateVbaCode(string code)
    {
        int subCount = CountOccurrences(code, "Sub ");
        int endSubCount = CountOccurrences(code, "End Sub");
        int functionCount = CountOccurrences(code, "Function ");

        // Ensure at least one Sub or Function exists.
        if (subCount == 0 && functionCount == 0)
        {
            Console.Error.WriteLine("VBA validation error: No Sub or Function definitions found.");
            return false;
        }

        // Ensure Sub/End Sub counts match.
        if (subCount != endSubCount)
        {
            Console.Error.WriteLine($"VBA validation error: Mismatched Sub/End Sub count (Sub: {subCount}, End Sub: {endSubCount}).");
            return false;
        }

        // Additional validation can be added here if needed.
        return true;
    }
}