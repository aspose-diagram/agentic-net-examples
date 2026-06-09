using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume the shapes are on the first page
            Page page = diagram.Pages[0];

            // Locate the source shape (the one that already has the hyperlink)
            Shape sourceShape = null;
            // Locate the target shape (the shape to receive the cloned hyperlink)
            Shape targetShape = null;

            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == "SourceShape")
                    sourceShape = shape;
                if (shape.NameU == "TargetShape")
                    targetShape = shape;
            }

            if (sourceShape == null)
            {
                Console.WriteLine("Source shape not found.");
                return;
            }

            if (targetShape == null)
            {
                Console.WriteLine("Target shape not found.");
                return;
            }

            // Ensure the source shape actually has at least one hyperlink
            if (sourceShape.Hyperlinks == null || sourceShape.Hyperlinks.Count == 0)
            {
                Console.WriteLine("Source shape does not contain any hyperlinks.");
                return;
            }

            // Clone the first hyperlink from the source shape
            Hyperlink originalLink = sourceShape.Hyperlinks[0];
            Hyperlink clonedLink = new Hyperlink();

            // Copy the essential fields
            clonedLink.Name = originalLink.Name;                     // optional identifier
            clonedLink.Address.Value = originalLink.Address.Value;   // external URL or file path
            clonedLink.SubAddress.Value = originalLink.SubAddress.Value; // internal target (if any)

            // Modify the description for the cloned hyperlink
            clonedLink.Description.Value = "Modified description";

            // Attach the cloned hyperlink to the target shape
            targetShape.Hyperlinks.Add(clonedLink);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Hyperlink cloned and attached to the target shape successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
