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

            // Load an existing Visio diagram (VSD/VSDX)
            // (Assumes the create/load rule is used internally by Diagram.Load)
            Diagram diagram = new Diagram(@"C:\Input\sample.vsd");

            // Create ImageSaveOptions for PNG format
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Set custom resolution (e.g., 300 DPI)
            pngOptions.Resolution = 300f;

            // Save the diagram to PNG using the specified options
            // (Assumes the save rule is used internally by Diagram.Save)
            diagram.Save(@"C:\Output\sample.png", pngOptions);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
