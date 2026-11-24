using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <masterName> [outputFolder]");
            return;
        }

        string inputPath = args[0];
        string masterName = args[1];
        string outputFolder = args.Length >= 3 ? args[2] : Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output folder exists or create it.
        try
        {
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                        {
                            continue;
                        }

                        // Ensure the shape has an associated master.
                        if (shape.Master == null)
                        {
                            continue;
                        }

                        // Export only shapes that use the specified master.
                        if (string.Equals(shape.Master.Name, masterName, StringComparison.OrdinalIgnoreCase))
                        {
                            // Build a unique HTML file name for the shape.
                            string baseFileName = Path.GetFileNameWithoutExtension(inputPath);
                            string htmlFileName = $"{baseFileName}_shape_{shape.ID}.html";
                            string htmlFilePath = Path.Combine(outputFolder, htmlFileName);

                            try
                            {
                                // Export the shape to HTML while preserving its formatting.
                                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                                shape.ToHTML(htmlFilePath, htmlOptions);
                                Console.WriteLine($"Exported shape ID {shape.ID} to '{htmlFilePath}'.");
                            }
                            catch (Exception exportEx)
                            {
                                Console.Error.WriteLine($"Failed to export shape ID {shape.ID}: {exportEx.Message}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}