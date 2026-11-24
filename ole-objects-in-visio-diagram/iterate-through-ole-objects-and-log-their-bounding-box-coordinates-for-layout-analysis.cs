using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify OLE (foreign) objects via ForeignData
                    if (shape.ForeignData != null &&
                        shape.ForeignData.Value != null &&
                        shape.ForeignData.Value.Length > 0)
                    {
                        // Ensure XForm is available
                        if (shape.XForm != null)
                        {
                            // Retrieve bounding box values (in inches)
                            double pinX = shape.XForm.PinX.Value;
                            double pinY = shape.XForm.PinY.Value;
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;

                            // Log the bounding box information
                            Console.WriteLine($"Page {page.NameU} - Shape ID {shape.ID}:");
                            Console.WriteLine($"  PinX = {pinX}");
                            Console.WriteLine($"  PinY = {pinY}");
                            Console.WriteLine($"  Width = {width}");
                            Console.WriteLine($"  Height = {height}");
                        }
                        else
                        {
                            Console.WriteLine($"Page {page.NameU} - Shape ID {shape.ID} has no XForm data.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}