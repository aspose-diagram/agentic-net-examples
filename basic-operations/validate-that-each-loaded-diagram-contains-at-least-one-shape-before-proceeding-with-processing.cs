using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure at least one file path is provided.
        if (args == null || args.Length == 0)
        {
            Console.Error.WriteLine("Please provide at least one diagram file path as an argument.");
            return;
        }

        // Process each diagram file supplied via command line.
        foreach (string filePath in args)
        {
            // Guard: verify the file exists before attempting to load.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Determine whether the diagram contains at least one shape.
                bool hasShape = false;
                foreach (Page page in diagram.Pages)
                {
                    // Guard against null Shape collection (should not occur, but safe).
                    if (page.Shapes != null && page.Shapes.Count > 0)
                    {
                        hasShape = true;
                        break;
                    }
                }

                if (!hasShape)
                {
                    Console.WriteLine($"Diagram '{filePath}' contains no shapes. Skipping processing.");
                    continue;
                }

                // At this point the diagram has at least one shape.
                Console.WriteLine($"Diagram '{filePath}' contains shapes. Proceeding with processing...");

                // TODO: Insert further processing logic here as needed.
                // Example placeholder: save the diagram unchanged to a new file.
                // string outputPath = Path.Combine(Path.GetDirectoryName(filePath) ?? "", 
                //     Path.GetFileNameWithoutExtension(filePath) + "_processed.vsdx");
                // diagram.Save(outputPath, SaveFileFormat.Vsdx);
                // Console.WriteLine($"Saved processed diagram to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during loading or validation.
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}