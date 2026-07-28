using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Loop through every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Loop through every shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Replace the word "Confidential" with "Public" wherever it appears in the shape's text
                    shape.ReplaceText("Confidential", "Public");

                    // Refresh the shape to recalculate its geometry after the text change
                    shape.RefreshData();
                }
            }

            // Save the updated diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
