using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing diagrams
        string inputFolder = args.Length > 0 ? args[0] : "Diagrams";
        // Output folder for processed diagrams
        string outputFolder = args.Length > 1 ? args[1] : "Processed";
        // Regular expression pattern to match custom property names
        string pattern = args.Length > 2 ? args[2] : "^Temp.*";

        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);
        Regex regex;
        try
        {
            regex = new Regex(pattern, RegexOptions.Compiled);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Invalid regex pattern: {pattern}");
            Console.WriteLine(ex.Message);
            return;
        }

        // Supported Visio file extensions
        string[] extensions = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vsx", "*.vtx" };
        List<string> files = new List<string>();
        foreach (string ext in extensions)
        {
            files.AddRange(Directory.GetFiles(inputFolder, ext, SearchOption.TopDirectoryOnly));
        }

        if (files.Count == 0)
        {
            Console.WriteLine("No diagram files found.");
            return;
        }

        foreach (string filePath in files)
        {
            try
            {
                // Load diagram
                Diagram diagram = new Diagram(filePath);

                // Collect custom properties that match the pattern
                List<CustomProp> toRemove = new List<CustomProp>();
                foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
                {
                    if (regex.IsMatch(prop.Name))
                    {
                        toRemove.Add(prop);
                    }
                }

                // Remove matching custom properties
                foreach (CustomProp prop in toRemove)
                {
                    diagram.DocumentProps.CustomProps.Remove(prop);
                }

                // Save the modified diagram
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Processed: {Path.GetFileName(filePath)} -> {Path.GetFileName(outputPath)} (removed {toRemove.Count} properties)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Bulk removal completed.");
    }
}
