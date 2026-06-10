using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
         try
         {

             // Load an existing Visio diagram (replace with your file path)
             Diagram diagram = new Diagram("input.vsdx");

             // Get the first page (or any specific page you want to work with)
             Page page = diagram.Pages[0];

             // Create AutoSpaceOptions and set vertical distance to zero
             AutoSpaceOptions options = new AutoSpaceOptions();
             options.DistanceInVertical = 0.0;          // No vertical gap between shapes
             options.DistanceInHorizontal = 0.375;    // Keep default horizontal spacing (optional)

             // Apply auto‑spacing to all shapes on the page
             page.AutoSpaceShapes(page.Shapes, options);

             // Save the modified diagram (replace with your desired output path)
             diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

         }
         catch (System.IO.FileNotFoundException ex)
         {
             Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
         }
    }
}
