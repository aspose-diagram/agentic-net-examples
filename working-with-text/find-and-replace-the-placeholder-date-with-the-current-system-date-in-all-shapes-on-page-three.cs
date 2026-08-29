using System.IO;
using System;
using Aspose.Diagram;

class ReplaceDatePlaceholder
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the third page (pages are zero‑based indexed)
            Page pageThree = diagram.Pages[2];

            // Current system date as string (default format)
            string currentDate = DateTime.Now.ToString();

            // Iterate through all shapes on page three
            foreach (Shape shape in pageThree.Shapes)
            {
                // Replace the placeholder "[Date]" with the current date
                shape.ReplaceText("[Date]", currentDate);

                // Refresh shape data after text change
                shape.RefreshData();
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
