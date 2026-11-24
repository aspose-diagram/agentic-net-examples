using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: inputPath, outputPath, oldFunctionName, newFunctionName
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath> <oldFunctionName> <newFunctionName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string oldFunc = args[2];
        string newFunc = args[3];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram (supports macro‑enabled formats)
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains a VBA project
            VbaProject vbaProject = diagram.VbaProject;
            if (vbaProject == null)
            {
                Console.Error.WriteLine("The diagram does not contain a VBA project.");
                return;
            }

            // Iterate through each VBA module and replace the deprecated function name
            foreach (VbaModule module in vbaProject.Modules)
            {
                string originalCode = module.Codes ?? string.Empty;
                string updatedCode = originalCode.Replace(oldFunc, newFunc);

                // Validate VBA syntax after replacement
                if (!ValidateVbaModule(updatedCode, module.Name))
                {
                    Console.Error.WriteLine($"Syntax validation failed for module '{module.Name}'. Aborting.");
                    return;
                }

                // Assign the updated code back to the module
                module.Codes = updatedCode;
            }

            // Prepare save options for macro‑enabled Visio format
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdm);
            // AutoFitPageToDrawingContent expects a plain bool, not Aspose BOOL
            saveOptions.AutoFitPageToDrawingContent = true;

            // Save the updated diagram
            diagram.Save(outputPath, saveOptions);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to validate VBA module syntax
    private static bool ValidateVbaModule(string code, string moduleName)
    {
        // Count occurrences of "Sub " and "End Sub"
        int subCount = CountOccurrences(code, "Sub ");
        int endSubCount = CountOccurrences(code, "End Sub");

        // Count occurrences of "Function " and "End Function"
        int funcCount = CountOccurrences(code, "Function ");
        int endFuncCount = CountOccurrences(code, "End Function");

        // Ensure each Sub has a matching End Sub
        if (subCount != endSubCount)
        {
            Console.Error.WriteLine($"Module '{moduleName}' has mismatched Sub/End Sub count (Sub: {subCount}, End Sub: {endSubCount}).");
            return false;
        }

        // Ensure each Function has a matching End Function
        if (funcCount != endFuncCount)
        {
            Console.Error.WriteLine($"Module '{moduleName}' has mismatched Function/End Function count (Function: {funcCount}, End Function: {endFuncCount}).");
            return false;
        }

        // Ensure the module contains at least one Sub or Function
        if (subCount == 0 && funcCount == 0)
        {
            Console.Error.WriteLine($"Module '{moduleName}' does not contain any Sub or Function definitions.");
            return false;
        }

        return true;
    }

    // Helper method to count non‑overlapping occurrences of a substring
    private static int CountOccurrences(string source, string substring)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(substring))
            return 0;

        int count = 0;
        int index = 0;
        while ((index = source.IndexOf(substring, index, StringComparison.Ordinal)) != -1)
        {
            count++;
            index += substring.Length;
        }
        return count;
    }
}