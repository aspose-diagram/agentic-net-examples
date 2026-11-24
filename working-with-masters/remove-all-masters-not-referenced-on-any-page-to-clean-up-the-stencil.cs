using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Determine output path (same directory with suffix "_cleaned.vsdx")
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_cleaned.vsdx");

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect names of masters that are actually used by shapes on any page
                HashSet<string> usedMasterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // If the shape references a master, add the master's name to the set
                        if (shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                        {
                            usedMasterNames.Add(shape.Master.Name);
                        }
                    }
                }

                // Build a list of all masters to avoid modifying the collection during enumeration
                List<Master> allMasters = new List<Master>();
                foreach (Master master in diagram.Masters)
                {
                    allMasters.Add(master);
                }

                // Remove masters that are not referenced by any shape
                foreach (Master master in allMasters)
                {
                    // If the master name is not in the used set, remove it
                    if (!usedMasterNames.Contains(master.Name))
                    {
                        // Attempt to remove the master from the diagram
                        diagram.Masters.Remove(master);
                        Console.WriteLine($"Removed unused master: {master.Name}");
                    }
                }

                // Save the cleaned diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Cleaned diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}