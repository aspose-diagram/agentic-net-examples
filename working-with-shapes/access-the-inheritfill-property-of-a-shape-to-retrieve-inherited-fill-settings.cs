using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard against missing file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Retrieve the first page.
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page.
            Shape shape = page.Shapes[0];

            // Access the inherited fill information.
            Fill inheritFill = shape.InheritFill;

            // Output inherited fill properties.
            Console.WriteLine("Inherited Fill Settings:");
            Console.WriteLine($"  FillForegnd (Foreground Color): {inheritFill.FillForegnd.Value}");
            Console.WriteLine($"  FillBkgnd (Background Color): {inheritFill.FillBkgnd.Value}");
            Console.WriteLine($"  FillPattern (Pattern Index): {inheritFill.FillPattern.Value}");
            Console.WriteLine($"  FillForegndTrans (Foreground Transparency): {inheritFill.FillForegndTrans.Value}");
            Console.WriteLine($"  FillBkgndTrans (Background Transparency): {inheritFill.FillBkgndTrans.Value}");
            Console.WriteLine($"  ShdwForegnd (Shadow Foreground Color): {inheritFill.ShdwForegnd.Value}");
            Console.WriteLine($"  ShdwBkgnd (Shadow Background Color): {inheritFill.ShdwBkgnd.Value}");
            Console.WriteLine($"  ShdwPattern (Shadow Pattern): {inheritFill.ShdwPattern.Value}");
            Console.WriteLine($"  ShapeShdwType (Shadow Type): {inheritFill.ShapeShdwType.Value}");
            Console.WriteLine($"  ShapeShdwObliqueAngle (Oblique Angle): {inheritFill.ShapeShdwObliqueAngle.Value}");
            Console.WriteLine($"  ShapeShdwOffsetX (Offset X): {inheritFill.ShapeShdwOffsetX.Value}");
            Console.WriteLine($"  ShapeShdwOffsetY (Offset Y): {inheritFill.ShapeShdwOffsetY.Value}");
            Console.WriteLine($"  ShapeShdwScaleFactor (Scale Factor): {inheritFill.ShapeShdwScaleFactor.Value}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}