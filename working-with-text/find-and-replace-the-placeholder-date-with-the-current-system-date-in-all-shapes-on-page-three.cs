using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (use the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the third page (index 2 because collection is zero‑based)
            Page pageThree = diagram.Pages[2];

            // Current system date as a string (adjust format as needed)
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            // Iterate through all shapes on page three
            foreach (Shape shape in pageThree.Shapes)
            {
                // Replace the placeholder "[Date]" with the current date
                shape.ReplaceText("[Date]", currentDate);

                // Refresh shape data after text change
                shape.RefreshData();
            }

            // Save the modified diagram (use the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
