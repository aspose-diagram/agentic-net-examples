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

            // Load an existing Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Define custom font substitution:
            // If the diagram contains "Comic Sans MS" and it is not installed,
            // Aspose.Diagram will try "Arial" first, then "Times New Roman".
            FontConfigs.SetFontSubstitutes(
                "Comic Sans MS",
                new string[] { "Arial", "Times New Roman" }
            );

            // Optionally set a default font for rendering when no substitute is found.
            // Here we use PNG image output as an example.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                DefaultFont = "Arial"
            };

            // Save the diagram with the specified options (uses the provided save rule)
            diagram.Save("output.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
