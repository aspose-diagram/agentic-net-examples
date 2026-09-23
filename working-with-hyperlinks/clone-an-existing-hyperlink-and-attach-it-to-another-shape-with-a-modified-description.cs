using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Paths to the source Visio file and the output file
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // IDs of the source shape (containing the hyperlink) and the target shape
            long sourceShapeId = 1; // replace with actual ID
            long targetShapeId = 2; // replace with actual ID

            // Retrieve the source shape
            Shape sourceShape = diagram.Pages[0].Shapes.GetShape(sourceShapeId);
            if (sourceShape == null)
            {
                Console.WriteLine("Source shape not found.");
                return;
            }

            // Ensure the source shape has at least one hyperlink
            if (sourceShape.Hyperlinks == null || sourceShape.Hyperlinks.Count == 0)
            {
                Console.WriteLine("Source shape does not contain any hyperlinks.");
                return;
            }

            // Clone the first hyperlink from the source shape
            Hyperlink originalLink = sourceShape.Hyperlinks[0];
            Hyperlink clonedLink = new Hyperlink();

            // Copy hyperlink properties
            clonedLink.Address.Value = originalLink.Address.Value;
            clonedLink.SubAddress.Value = originalLink.SubAddress.Value;
            clonedLink.Name = originalLink.Name; // optional: preserve the name

            // Modify the description as required
            clonedLink.Description.Value = "Modified description";

            // Retrieve the target shape
            Shape targetShape = diagram.Pages[0].Shapes.GetShape(targetShapeId);
            if (targetShape == null)
            {
                Console.WriteLine("Target shape not found.");
                return;
            }

            // Attach the cloned hyperlink to the target shape
            targetShape.Hyperlinks.Add(clonedLink);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Hyperlink cloned and attached successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
