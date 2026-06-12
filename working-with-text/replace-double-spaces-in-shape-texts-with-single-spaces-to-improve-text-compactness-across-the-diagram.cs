using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram(@"C:\Path\To\InputDiagram.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Replace every occurrence of double spaces with a single space
                    // Shape.ReplaceText replaces all matching substrings within the shape's text
                    shape.ReplaceText("  ", " ");

                    // Refresh shape geometry so that any size adjustments due to text changes are applied
                    shape.RefreshData();
                }
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save(@"C:\Path\To\OutputDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
