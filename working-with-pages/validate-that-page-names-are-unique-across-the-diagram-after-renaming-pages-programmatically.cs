using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input file path as first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Optional output path (second argument); if not provided, create a default one
        string outputPath;
        if (args.Length > 1)
        {
            outputPath = args[1];
        }
        else
        {
            string directory = Path.GetDirectoryName(inputPath);
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(directory, $"{fileName}_Renamed.vsdx");
        }

        // Load diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                try
                {
                    // Rename each page to a unique name based on its order
                    int index = 1;
                    foreach (Page page in diagram.Pages)
                    {
                        string newName = $"Page_{index}";
                        page.Name = newName;
                        index++;
                    }

                    // Validate that all page names are unique
                    HashSet<string> nameSet = new HashSet<string>();
                    bool duplicateFound = false;

                    foreach (Page page in diagram.Pages)
                    {
                        if (!nameSet.Add(page.Name))
                        {
                            duplicateFound = true;
                            Console.Error.WriteLine($"Duplicate page name detected: {page.Name}");
                        }
                    }

                    if (!duplicateFound)
                    {
                        Console.WriteLine("All page names are unique after renaming.");
                    }
                    else
                    {
                        // If duplicates exist, abort further processing
                        Console.Error.WriteLine("Validation failed due to duplicate page names.");
                        return;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error during diagram manipulation: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
        }
    }
}