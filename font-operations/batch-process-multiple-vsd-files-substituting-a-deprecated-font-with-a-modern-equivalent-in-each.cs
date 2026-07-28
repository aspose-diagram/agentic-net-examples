using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class FontSubstitutionBatchProcessor
{
    // Path to the folder containing VSD files
    private const string InputFolder = @"C:\VisioFiles";

    // Original (deprecated) font name to replace
    private const string DeprecatedFont = "OldFontName";

    // Modern substitute fonts (ordered by preference)
    private static readonly string[] SubstituteFonts = new[] { "NewFont1", "NewFont2" };

    static void Main()
    {
        // Get all VSD files in the specified folder
        string[] vsdFiles = Directory.GetFiles(InputFolder, "*.vsd", SearchOption.TopDirectoryOnly);

        foreach (string filePath in vsdFiles)
        {
            // Load the Visio diagram from file
            using (Diagram diagram = new Diagram(filePath))
            {
                // Register font substitutes for the deprecated font
                FontConfigs.SetFontSubstitutes(DeprecatedFont, SubstituteFonts);

                // Save the diagram back, overwriting the original file
                diagram.Save(filePath, SaveFileFormat.Vsd);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
        }

        Console.WriteLine("Batch font substitution completed.");
    }
}
