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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Define custom font substitutes for fonts that may be missing
            // Example: if "Arial" is not available, try "Liberation Sans" then "Helvetica"
            FontConfigs.SetFontSubstitutes("Arial", new string[] { "Liberation Sans", "Helvetica" });

            // Optionally, control whether system substitutes are preferred
            FontConfigs.PreferSystemFontSubstitutes = false;

            // Set a default font to be used during rendering/saving (fallback)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.DefaultFont = "Liberation Sans";

            // Save the diagram with the specified options
            diagram.Save("output.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
