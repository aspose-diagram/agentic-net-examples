using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for the folder containing diagram files.
        Console.Write("Enter the full path to the folder with Visio diagrams: ");
        string folderPath = Console.ReadLine();

        // Guard: ensure the folder exists.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("The specified folder does not exist.");
            return;
        }

        // Prompt user for the field value that should be removed.
        Console.Write("Enter the exact field value to remove from all shapes (e.g., \"MyCustomField\"): ");
        string targetFieldValue = Console.ReadLine();

        if (string.IsNullOrEmpty(targetFieldValue))
        {
            Console.Error.WriteLine("No field value provided.");
            return;
        }

        // Gather all Visio files in the folder (common extensions).
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        List<string> supportedFiles = new List<string>();
        foreach (string file in diagramFiles)
        {
            string ext = Path.GetExtension(file).ToLowerInvariant();
            if (ext == ".vsdx" || ext == ".vsd" || ext == ".vsx" || ext == ".vtx" ||
                ext == ".vssx" || ext == ".vss" || ext == ".vstx" || ext == ".vst")
            {
                supportedFiles.Add(file);
            }
        }

        if (supportedFiles.Count == 0)
        {
            Console.WriteLine("No supported Visio diagram files found in the specified folder.");
            return;
        }

        // Process each diagram file.
        foreach (string diagramPath in supportedFiles)
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Fields collection.
                        if (shape.Fields == null)
                            continue;

                        // Collect fields that match the target value.
                        List<Field> fieldsToRemove = new List<Field>();
                        foreach (Field field in shape.Fields)
                        {
                            // Guard against null Value.
                            if (field.Value != null && field.Value.Val == targetFieldValue)
                            {
                                fieldsToRemove.Add(field);
                            }
                        }

                        // Remove the matching fields.
                        foreach (Field field in fieldsToRemove)
                        {
                            shape.Fields.Remove(field);
                        }
                    }
                }

                // Determine the appropriate SaveFileFormat based on original extension.
                SaveFileFormat saveFormat = GetSaveFormatFromExtension(Path.GetExtension(diagramPath));

                // Save the modified diagram, overwriting the original file.
                diagram.Save(diagramPath, saveFormat);

                Console.WriteLine($"Processed and saved: {diagramPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of this file.
                Console.Error.WriteLine($"Error processing file '{diagramPath}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Maps a file extension to the corresponding SaveFileFormat enum value.
    /// Defaults to Vsdx if the extension is unrecognized.
    /// </summary>
    private static SaveFileFormat GetSaveFormatFromExtension(string extension)
    {
        switch (extension.ToLowerInvariant())
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vsx":
                return SaveFileFormat.Vsx;
            case ".vtx":
                return SaveFileFormat.Vtx;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vst":
                return SaveFileFormat.Vst;
            default:
                // Fallback to Vsdx if unknown.
                return SaveFileFormat.Vsdx;
        }
    }
}