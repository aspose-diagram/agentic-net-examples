using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_compressed.vsdm";

        // If command‑line arguments are provided, use them instead of the defaults
        if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
        {
            inputPath = args[0];
        }
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the VBA project (read‑only property)
                VbaProject vbaProject = diagram.VbaProject;

                if (vbaProject == null)
                {
                    Console.WriteLine("No VBA project found in the diagram. No compression needed.");
                }
                else
                {
                    // Iterate over each VBA module and compress its source code
                    foreach (VbaModule module in vbaProject.Modules)
                    {
                        string originalCode = module.Codes ?? string.Empty;
                        string compressedCode = CompressVbaCode(originalCode);
                        module.Codes = compressedCode;
                    }

                    Console.WriteLine($"Compressed {vbaProject.Modules.Count} VBA module(s).");
                }

                // Save the modified diagram in a macro‑enabled format to preserve VBA
                diagram.Save(outputPath, SaveFileFormat.Vsdm);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes unnecessary whitespace and empty lines from VBA code to reduce size.
    /// </summary>
    /// <param name="code">Original VBA source code.</param>
    /// <returns>Compressed VBA source code.</returns>
    private static string CompressVbaCode(string code)
    {
        if (string.IsNullOrEmpty(code))
            return code;

        // Split into lines, trim each line, and discard empty lines
        string[] lines = code.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        var compressedLines = new System.Collections.Generic.List<string>();

        foreach (string line in lines)
        {
            string trimmed = line.Trim();
            if (trimmed.Length == 0)
                continue; // Skip empty lines

            // Collapse multiple spaces within the line to a single space
            string collapsed = System.Text.RegularExpressions.Regex.Replace(trimmed, @"\s+", " ");
            compressedLines.Add(collapsed);
        }

        // Rejoin lines using a single newline character
        return string.Join("\n", compressedLines);
    }
}