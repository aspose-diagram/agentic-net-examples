using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class DiagramThemeProcessor
{
    /// <summary>
    /// Loads a Visio diagram from a byte array, applies a preset theme variant to the first page,
    /// and returns the modified diagram as a byte array in VSDX format.
    /// </summary>
    /// <param name="diagramBytes">Input diagram bytes (any supported Visio format).</param>
    /// <returns>Modified diagram bytes saved as VSDX.</returns>
    public static byte[] ApplyThemeVariant(byte[] diagramBytes)
    {
        // Create temporary file paths for input and output.
        string tempInputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".vsdx");
        string tempOutputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".vsdx");

        try
        {
            // Write the input byte array to the temporary input file.
            File.WriteAllBytes(tempInputPath, diagramBytes);

            // Load the diagram from the temporary file.
            Diagram diagram = new Diagram(tempInputPath);

            // Ensure there is at least one page to apply the theme.
            if (diagram.Pages.Count > 0)
            {
                // Apply a preset theme and variant to the first page.
                Page firstPage = diagram.Pages[0];
                firstPage.PresetTheme = PresetThemeValue.Bubble;               // Choose a valid preset theme.
                firstPage.PresetThemeVariant = PresetThemeVariantValue.Variant3; // Choose a variant.
            }

            // Save the modified diagram to the temporary output file in VSDX format.
            diagram.Save(tempOutputPath, SaveFileFormat.Vsdx);

            // Read the modified file back into a byte array.
            return File.ReadAllBytes(tempOutputPath);
        }
        finally
        {
            // Clean up temporary files regardless of success or failure.
            if (File.Exists(tempInputPath))
                File.Delete(tempInputPath);
            if (File.Exists(tempOutputPath))
                File.Delete(tempOutputPath);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
