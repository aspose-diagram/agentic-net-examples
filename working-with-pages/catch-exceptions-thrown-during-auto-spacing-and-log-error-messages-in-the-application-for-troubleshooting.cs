using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Configure auto-space options
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 2;
            options.DistanceInVertical = 2;

            // Apply auto-spacing to each page with exception handling
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    page.AutoSpaceShapes(page.Shapes, options);
                    Console.WriteLine($"Auto-spacing applied successfully on page '{page.Name}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during auto-spacing on page '{page.Name}': {ex.Message}");
                }
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
