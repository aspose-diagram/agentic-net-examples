using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (or any specific page)
            Page page = diagram.Pages[0];

            // Configure autospace options
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // inches
                DistanceInVertical = 0.5    // inches
            };

            // Attempt to auto‑space the shapes and handle any runtime errors gracefully
            try
            {
                page.AutoSpaceShapes(page.Shapes, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Auto‑spacing failed: " + ex.Message);
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
