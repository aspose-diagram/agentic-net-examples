using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Ensure the diagram has at least five pages
            while (diagram.Pages.Count < 5)
            {
                diagram.Pages.Add(new Page());
            }

            // Base position for the first circle (in inches)
            double basePinX = 2.0;
            double basePinY = 2.0;
            // Incremental offset applied to each subsequent page
            double offsetStep = 1.0;

            // Duplicate a circle (ellipse with equal width/height) on each page
            for (int i = 0; i < 5; i++)
            {
                Page page = diagram.Pages[i];
                double pinX = basePinX + i * offsetStep;
                double pinY = basePinY + i * offsetStep;

                // Add a circle using the built‑in "Ellipse" master.
                // Width and height are taken from the master defaults.
                page.AddShape(pinX, pinY, "Ellipse");
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
