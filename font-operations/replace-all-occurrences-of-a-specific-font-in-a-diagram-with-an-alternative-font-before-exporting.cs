using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class FontReplacementExample
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string sourcePath = @"C:\Diagrams\input.vsdx";

            // Path where the modified diagram will be saved
            string destinationPath = @"C:\Diagrams\output.vsdx";

            // Font to be replaced and its replacement
            string originalFontName = "Arial";
            string[] substituteFontNames = new[] { "Calibri" };

            // Load the diagram from file
            Diagram diagram = new Diagram(sourcePath);

            // Register the substitute font(s) for the original font
            FontConfigs.SetFontSubstitutes(originalFontName, substituteFontNames);

            // Create save options for the desired format (VDX in this case)
            SaveOptions saveOptions = SaveOptions.CreateSaveOptions(SaveFileFormat.Vdx);
            // Ensure the default font is also set to the replacement (optional but helpful)
            saveOptions.DefaultFont = "Calibri";

            // Save the diagram with the new font configuration
            diagram.Save(destinationPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
