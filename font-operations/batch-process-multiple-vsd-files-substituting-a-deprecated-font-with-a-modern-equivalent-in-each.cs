using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Define the deprecated font and its modern substitutes
        string deprecatedFont = "OldFontName";
        string[] substituteFonts = new string[] { "NewFontA", "NewFontB" };

        // Register the font substitution globally
        FontConfigs.SetFontSubstitutes(deprecatedFont, substituteFonts);

        // Input directory containing VSD files
        string inputDirectory = @"C:\VisioFiles";
        // Output directory for processed files
        string outputDirectory = @"C:\VisioFiles\Processed";

        Directory.CreateDirectory(outputDirectory);

        // Process each VSD file in the input directory
        foreach (string filePath in Directory.GetFiles(inputDirectory, "*.vsd"))
        {
            // Load the diagram
            using (Diagram diagram = new Diagram(filePath))
            {
                // Save the diagram back (overwrites or writes to output folder)
                string fileName = Path.GetFileName(filePath);
                string outputPath = Path.Combine(outputDirectory, fileName);
                diagram.Save(outputPath, SaveFileFormat.Vsd);
            }
        }
    }
}
