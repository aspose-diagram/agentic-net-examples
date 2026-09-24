using System.IO;
using System;
using Aspose.Diagram;

class MoveShapesOnDetailsPage
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Find the page named "Details"
            Page detailsPage = null;
            foreach (Page page in diagram.Pages)
            {
                if (page.Name == "Details")
                {
                    detailsPage = page;
                    break;
                }
            }

            // If the page exists, move each shape on it
            if (detailsPage != null)
            {
                foreach (Shape shape in detailsPage.Shapes)
                {
                    // Add 20 units to the X coordinate (PinX)
                    shape.XForm.PinX.Value += 20;
                }
            }
            else
            {
                Console.WriteLine("Page named \"Details\" not found.");
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
