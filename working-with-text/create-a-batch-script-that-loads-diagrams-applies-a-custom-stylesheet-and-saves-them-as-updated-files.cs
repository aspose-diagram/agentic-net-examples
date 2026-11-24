using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: BatchApplyStyle <inputFolder> [outputFolder]");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args.Length > 1 ? args[1] : inputFolder;

        // Guard: input folder must exist
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Guard: output folder must exist (create if missing)
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

        // Get all files in the input folder
        string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            // Process only supported Visio extensions
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" && ext != ".vsx" && ext != ".vtx")
            {
                continue;
            }

            // Guard: file must exist (redundant but per requirements)
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
                    // Create a custom stylesheet and assign a unique ID
                    StyleSheet customStyle = new StyleSheet();
                    customStyle.Name = "CustomStyle";
                    customStyle.ID = diagram.StyleSheets.Count + 1; // IDs are 1‑based

                    // Add the stylesheet to the diagram's collection
                    diagram.StyleSheets.Add(customStyle);

                    // Apply the stylesheet to each page (text, line, fill styles)
                    foreach (Page page in diagram.Pages)
                    {
                        page.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);
                    }

                    // Build output file path (append "_updated" before extension)
                    string outputFileName = Path.GetFileNameWithoutExtension(filePath) + "_updated" + ext;
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Determine the appropriate SaveFileFormat based on the original extension
                    SaveFileFormat format = GetSaveFormat(ext);

                    // Save the updated diagram using the original format
                    diagram.Save(outputPath, format);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Maps file extensions to the corresponding SaveFileFormat enum values
    private static SaveFileFormat GetSaveFormat(string extension)
    {
        switch (extension)
        {
            case ".vsdx": return SaveFileFormat.Vsdx;
            case ".vsd":  return SaveFileFormat.Vsd;
            case ".vdx":  return SaveFileFormat.Vdx;
            case ".vsx":  return SaveFileFormat.Vsx;
            case ".vtx":  return SaveFileFormat.Vtx;
            default:      return SaveFileFormat.Vsdx; // Fallback to VSDX
        }
    }
}