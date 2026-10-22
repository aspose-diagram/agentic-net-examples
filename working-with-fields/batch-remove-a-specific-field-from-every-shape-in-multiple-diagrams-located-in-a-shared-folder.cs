using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: folder path and the field value to remove
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <folderPath> <fieldValueToRemove>");
            return;
        }

        string folderPath = args[0];
        string targetFieldValue = args[1];

        // Guard: ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Get all Visio files in the folder (common extensions)
        string[] visioExtensions = new[] { ".vsdx", ".vsd", ".vsx", ".vtx", ".vssx", ".vss", ".vstm", ".vstx", ".vst", ".vdx" };
        List<string> files = new List<string>();
        foreach (string ext in visioExtensions)
        {
            files.AddRange(Directory.GetFiles(folderPath, "*" + ext, SearchOption.TopDirectoryOnly));
        }

        foreach (string filePath in files)
        {
            // Guard: ensure the file exists (redundant but per requirements)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Fields collection
                        if (shape.Fields == null)
                            continue;

                        // Collect fields that match the target value
                        List<Field> fieldsToRemove = new List<Field>();
                        foreach (Field field in shape.Fields)
                        {
                            // Compare the field's current value
                            if (field.Value != null && field.Value.Val == targetFieldValue)
                            {
                                fieldsToRemove.Add(field);
                            }
                        }

                        // Remove the collected fields from the shape
                        foreach (Field field in fieldsToRemove)
                        {
                            shape.Fields.Remove(field);
                        }
                    }
                }

                // Determine appropriate save format based on file extension
                SaveFileFormat format = GetSaveFileFormat(Path.GetExtension(filePath));

                // Save the modified diagram, overwriting the original file
                diagram.Save(filePath, format);

                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Helper method to map file extensions to SaveFileFormat enum values
    private static SaveFileFormat GetSaveFileFormat(string extension)
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
            case ".vstm":
                return SaveFileFormat.Vstm;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vst":
                return SaveFileFormat.Vst;
            case ".vdx":
                return SaveFileFormat.Vdx;
            default:
                // Default to Vsdx if unknown extension
                return SaveFileFormat.Vsdx;
        }
    }
}