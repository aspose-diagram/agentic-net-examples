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

            // Path to the source Visio diagram
            string sourceFile = "input.vsdx";

            // Path for the exported diagram
            string outputFile = "output.vdx";

            // Original font name to be replaced
            string originalFont = "Calibri";

            // Substitute font name that will replace the original font
            string[] substituteFonts = new[] { "Arial" };

            // Register the font substitution globally
            FontConfigs.SetFontSubstitutes(originalFont, substituteFonts);

            // Load the diagram using the provided constructor (lifecycle rule)
            Diagram diagram = new Diagram(sourceFile);

            // Save the diagram using the provided Save method (lifecycle rule)
            diagram.Save(outputFile, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
