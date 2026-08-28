using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a VSDX file
            var diagram = new Diagram("input.vsdx", LoadFileFormat.Vsdx);

            // Create save options and set a custom default font for rendering
            var saveOptions = new DiagramSaveOptions();
            saveOptions.DefaultFont = "MS Gothic"; // replace with any installed font name

            // Save the diagram using the options (saving to VDX as an example)
            diagram.Save("output.vdx", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
