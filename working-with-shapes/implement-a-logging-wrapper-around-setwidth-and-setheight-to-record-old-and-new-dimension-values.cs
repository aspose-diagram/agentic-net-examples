using System;
using Aspose.Diagram;

public static class ShapeLogger
{
    // Wrapper for SetWidth that logs old and new values
    public static void SetWidthLogged(this Shape shape, double newWidth)
    {
        double oldWidth = shape.XForm.Width.Value;
        Console.WriteLine($"SetWidth: Old Width = {oldWidth}, New Width = {newWidth}");
        shape.SetWidth(newWidth);
    }

    // Wrapper for SetHeight that logs old and new values
    public static void SetHeightLogged(this Shape shape, double newHeight)
    {
        double oldHeight = shape.XForm.Height.Value;
        Console.WriteLine($"SetHeight: Old Height = {oldHeight}, New Height = {newHeight}");
        shape.SetHeight(newHeight);
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Ensure the diagram has at least one page and one shape
            if (diagram.Pages.Count > 0)
            {
                var page = diagram.Pages[0];
                if (page.Shapes.Count > 0)
                {
                    // Retrieve the first shape on the page
                    Shape shape = page.Shapes[0];

                    // Apply the logging wrappers
                    shape.SetWidthLogged(5.0);   // Set new width to 5 inches
                    shape.SetHeightLogged(3.0); // Set new height to 3 inches
                }
                else
                {
                    Console.WriteLine("No shapes found on the first page.");
                }
            }
            else
            {
                Console.WriteLine("The diagram contains no pages.");
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