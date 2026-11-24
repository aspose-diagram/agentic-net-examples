using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: source folder, placeholder image path, replacement image path, output folder
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <sourceFolder> <placeholderImagePath> <replacementImagePath> <outputFolder>");
            return;
        }

        string sourceFolder = args[0];
        string placeholderPath = args[1];
        string replacementPath = args[2];
        string outputFolder = args[3];

        // Validate source folder
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder does not exist: {sourceFolder}");
            return;
        }

        // Validate placeholder image file
        if (!File.Exists(placeholderPath))
        {
            Console.Error.WriteLine($"Placeholder image file not found: {placeholderPath}");
            return;
        }

        // Validate replacement image file
        if (!File.Exists(replacementPath))
        {
            Console.Error.WriteLine($"Replacement image file not found: {replacementPath}");
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

        // Load placeholder and replacement image bytes once
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

        // Process each VSDX file in the source folder
        string[] vsdxFiles = Directory.GetFiles(sourceFolder, "*.vsdx");
        foreach (string inputPath in vsdxFiles)
        {
            // Guard: ensure the file still exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found (skipping): {inputPath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block as required
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate over all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate over all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify foreign (image) shapes
                            if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                            {
                                // Compare the current image bytes with the placeholder bytes
                                byte[] currentBytes = shape.ForeignData.Value;
                                if (AreByteArraysEqual(currentBytes, placeholderBytes))
                                {
                                    // Replace with the new image bytes
                                    shape.ForeignData.Value = replacementBytes;
                                }
                            }
                        }
                    }

                    // Prepare output path (same file name in output folder)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                    // Save the modified diagram back to VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed and saved: {Path.GetFileName(inputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }
    }

    // Helper method to compare two byte arrays for equality
    private static bool AreByteArraysEqual(byte[] a, byte[] b)
    {
        if (ReferenceEquals(a, b))
            return true;
        if (a == null || b == null)
            return false;
        if (a.Length != b.Length)
            return false;
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i])
                return false;
        }
        return true;
    }
}