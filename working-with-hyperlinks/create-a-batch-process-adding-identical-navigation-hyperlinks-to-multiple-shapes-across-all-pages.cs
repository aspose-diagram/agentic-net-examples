using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder> <hyperlinkAddress> [shapeName]");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];
        string hyperlinkAddress = args[2];
        string targetShapeName = args.Length >= 4 ? args[3] : null;

        // Verify input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Verify (or create) output folder.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Get all Visio files in the input folder (common extensions).
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" && extension != ".vssx" && extension != ".vstx")
            {
                // Skip non‑Visio files.
                continue;
            }

            // Guard against missing file (should not happen after GetFiles, but keep per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through all pages.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // If a specific shape name is provided, filter by NameU.
                            if (!string.IsNullOrEmpty(targetShapeName))
                            {
                                if (shape.NameU == null || !shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                                    continue;
                            }

                            // Create a new hyperlink instance.
                            Hyperlink link = new Hyperlink();

                            // Set hyperlink properties using the cell‑based .Value accessors.
                            link.Description.Value = "Navigation Link";
                            link.Address.Value = hyperlinkAddress;
                            link.NewWindow.Value = BOOL.True;

                            // Add the hyperlink to the shape's collection.
                            shape.Hyperlinks.Add(link);
                        }
                    }

                    // Prepare output file path.
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                    // Save the modified diagram preserving the original format.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}