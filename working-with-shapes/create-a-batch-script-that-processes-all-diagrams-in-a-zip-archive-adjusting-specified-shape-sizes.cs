using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect arguments: zipPath shapeName newWidth newHeight [outputDir]
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <zipPath> <shapeName> <newWidth> <newHeight> [outputDir]");
            return;
        }

        // Parse and validate the zip archive path
        string zipPath = args[0];
        if (!File.Exists(zipPath))
        {
            Console.Error.WriteLine($"File not found: {zipPath}");
            return;
        }

        // Shape name to locate (case‑insensitive)
        string targetShapeName = args[1];

        // Parse new width and height values (in inches)
        if (!double.TryParse(args[2], out double newWidth))
        {
            Console.Error.WriteLine($"Invalid width: {args[2]}");
            return;
        }
        if (!double.TryParse(args[3], out double newHeight))
        {
            Console.Error.WriteLine($"Invalid height: {args[3]}");
            return;
        }

        // Determine output directory (default to current directory)
        string outputDir = args.Length >= 5 ? args[4] : Directory.GetCurrentDirectory();
        if (!Directory.Exists(outputDir))
        {
            // Create the output directory if it does not exist
            Directory.CreateDirectory(outputDir);
        }

        // Create a temporary folder for extracting the zip contents
        string tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempFolder);

        try
        {
            // Extract all files from the zip archive into the temporary folder
            ZipFile.ExtractToDirectory(zipPath, tempFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting zip: {ex.Message}");
            return;
        }

        // Define Visio file extensions that Aspose.Diagram can load
        HashSet<string> visioExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vss", ".vstx", ".vst", ".vstm", ".vsdm", ".vssm"
        };

        // Collect all diagram files from the extracted folder (recursive)
        List<string> diagramFiles = new List<string>();
        foreach (string file in Directory.GetFiles(tempFolder, "*.*", SearchOption.AllDirectories))
        {
            if (visioExtensions.Contains(Path.GetExtension(file)))
            {
                diagramFiles.Add(file);
            }
        }

        // Process each diagram file individually
        foreach (string diagramPath in diagramFiles)
        {
            try
            {
                // Load the Visio diagram from the file
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through every page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through every shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape's universal name matches the target (ignore case)
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                        {
                            // Update the shape's width and height (values are in inches)
                            shape.XForm.Width.Value = newWidth;
                            shape.XForm.Height.Value = newHeight;
                        }
                    }
                }

                // Compute the relative path of the diagram inside the temp folder
                string relativePath = Path.GetRelativePath(tempFolder, diagramPath);

                // Build the full output path preserving the original folder structure
                string outputPath = Path.Combine(outputDir, relativePath);
                string outputFolder = Path.GetDirectoryName(outputPath);
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Save the modified diagram as VSDX (preserves all features)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Log any errors that occur while processing an individual diagram
                Console.Error.WriteLine($"Error processing '{diagramPath}': {ex.Message}");
            }
        }

        // Clean up the temporary extraction folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            // Non‑critical cleanup failure; report but do not abort
            Console.Error.WriteLine($"Warning: could not delete temporary folder: {ex.Message}");
        }
    }
}