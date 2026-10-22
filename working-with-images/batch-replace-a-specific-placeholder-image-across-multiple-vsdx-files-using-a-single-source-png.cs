using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: <folderPath> <placeholderImagePath> <replacementImagePath>
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <folderPath> <placeholderImagePath> <replacementImagePath>");
            return;
        }

        string folderPath = args[0];
        string placeholderPath = args[1];
        string replacementPath = args[2];

        // Guard folder existence
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Guard placeholder image existence
        if (!File.Exists(placeholderPath))
        {
            Console.Error.WriteLine($"Placeholder image file not found: {placeholderPath}");
            return;
        }

        // Guard replacement image existence
        if (!File.Exists(replacementPath))
        {
            Console.Error.WriteLine($"Replacement image file not found: {replacementPath}");
            return;
        }

        // Load image bytes once for comparison and replacement
        byte[] placeholderBytes;
        byte[] replacementBytes;
        try
        {
            placeholderBytes = File.ReadAllBytes(placeholderPath);
            replacementBytes = File.ReadAllBytes(replacementPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading image files: {ex.Message}");
            return;
        }

        // Get all VSDX files in the specified folder
        string[] vsdxFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);
        if (vsdxFiles.Length == 0)
        {
            Console.WriteLine("No VSDX files found in the specified folder.");
            return;
        }

        foreach (string filePath in vsdxFiles)
        {
            // Guard each file (should exist from GetFiles, but double‑check)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found (skipping): {filePath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block as required
                using (Diagram diagram = new Diagram(filePath))
                {
                    bool modified = false;

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify picture shapes (foreign images)
                            if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                            {
                                // Compare the current image bytes with the placeholder bytes
                                if (ByteArraysEqual(shape.ForeignData.Value, placeholderBytes))
                                {
                                    // Replace the image data with the new PNG bytes
                                    shape.ForeignData.Value = replacementBytes;

                                    // Ensure the image stays behind other content
                                    shape.SendToBack();

                                    modified = true;
                                }
                            }
                        }
                    }

                    // Save only if any replacement was performed
                    if (modified)
                    {
                        diagram.Save(filePath, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Replaced placeholder image in: {Path.GetFileName(filePath)}");
                    }
                    else
                    {
                        Console.WriteLine($"No matching placeholder found in: {Path.GetFileName(filePath)}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Helper method to compare two byte arrays for equality
    private static bool ByteArraysEqual(byte[] a1, byte[] a2)
    {
        if (ReferenceEquals(a1, a2))
            return true;
        if (a1 == null || a2 == null)
            return false;
        if (a1.Length != a2.Length)
            return false;
        for (int i = 0; i < a1.Length; i++)
        {
            if (a1[i] != a2[i])
                return false;
        }
        return true;
    }
}