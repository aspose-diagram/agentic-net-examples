using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination diagram files
            string sourcePath = "input.vsdx";
            string destinationPath = "output.vsdx";

            // Font to be replaced and its substitute
            string originalFont = "Arial";
            string[] substituteFonts = new[] { "Calibri" };

            // Register the substitute font globally
            FontConfigs.SetFontSubstitutes(originalFont, substituteFonts);

            // Load the diagram using the provided constructor (lifecycle rule)
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Optional: ensure the default font for saving is the substitute
                DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);
                saveOptions.DefaultFont = "Calibri";

                // Save the diagram with the specified options (lifecycle rule)
                diagram.Save(destinationPath, saveOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
