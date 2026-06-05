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

            // Path to the source VSDX file
            string sourcePath = "input.vsdx";

            // Load the Visio diagram specifying the VSDX format
            Diagram diagram = new Diagram(sourcePath, LoadFileFormat.Vsdx);

            // Create save options and set a custom default font.
            // This font will be used when characters are missing or Unicode fonts are not installed.
            DiagramSaveOptions saveOptions = new DiagramSaveOptions();
            saveOptions.DefaultFont = "MS Gothic";   // custom default font

            // Save the diagram (any supported format, here VDX) using the options with the custom font.
            string outputPath = "output.vdx";
            diagram.Save(outputPath, saveOptions);

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
