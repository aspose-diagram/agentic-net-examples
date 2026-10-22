using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input folder argument
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard input folder existence
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                return;
            }
        }

        // Get all diagram files in the input folder (common Visio extensions)
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in diagramFiles)
        {
            // Filter by supported extensions
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" && extension != ".vssx" && extension != ".vss")
            {
                // Skip non‑Visio files
                continue;
            }

            // Guard file existence (redundant but per requirements)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block as required
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Ensure the diagram has at least one page
                    if (diagram.Pages.Count == 0)
                    {
                        Console.Error.WriteLine($"Diagram has no pages, skipping: {filePath}");
                        continue;
                    }

                    // Attempt to add a comment to the first page.
                    // If the format does not support comments, this may throw.
                    Page firstPage = diagram.Pages[0];
                    try
                    {
                        // Add a simple comment at coordinates (1,1)
                        firstPage.AddComment(1.0, 1.0, "Processed by batch operation");
                    }
                    catch (Exception commentEx)
                    {
                        // Comment support missing – skip this diagram gracefully
                        Console.Error.WriteLine($"Comments not supported in diagram '{Path.GetFileName(filePath)}': {commentEx.Message}");
                        continue;
                    }

                    // Save the modified diagram to the output folder preserving the original name
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Successfully processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // General error handling for loading or saving
                Console.Error.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                // Continue with next file
            }
        }
    }
}