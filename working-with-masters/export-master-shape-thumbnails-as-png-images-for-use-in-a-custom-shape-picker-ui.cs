using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing.Imaging; // Required for ImageFormat

public class Program
{
    public static void Main(string[] args)
    {
        // Expect two arguments: the Visio file (stencil) and the output folder for thumbnails.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <visioFilePath> <outputFolder>");
            return;
        }

        string visioPath = args[0];
        // Guard to ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        string outputFolder = args[1];
        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram (stencil) containing masters.
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Iterate through each master shape in the stencil.
        foreach (Master master in diagram.Masters)
        {
            // The master icon is stored as a byte array. Skip if missing.
            if (master.Icon == null || master.Icon.Length == 0)
            {
                Console.WriteLine($"Master '{master.Name}' has no icon data.");
                continue;
            }

            // Build a safe file name for the PNG thumbnail.
            string safeName = SanitizeFileName(master.Name);
            string outputPath = Path.Combine(outputFolder, safeName + ".png");

            try
            {
                // Convert the icon byte array to an Aspose.Drawing.Image and save as PNG.
                using (MemoryStream ms = new MemoryStream(master.Icon))
                using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                {
                    img.Save(outputPath, ImageFormat.Png);
                }

                Console.WriteLine($"Exported thumbnail for master '{master.Name}' to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to export thumbnail for master '{master.Name}': {ex.Message}");
            }
        }
    }

    // Helper to replace invalid filename characters with an underscore.
    private static string SanitizeFileName(string name)
    {
        char[] invalidChars = Path.GetInvalidFileNameChars();
        foreach (char c in invalidChars)
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}