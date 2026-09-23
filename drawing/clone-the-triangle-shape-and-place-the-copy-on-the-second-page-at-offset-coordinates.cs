using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths – replace with actual file locations as needed
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is a second page; create one if necessary
            if (diagram.Pages.Count < 2)
            {
                // Determine the next page ID
                int maxId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxId) maxId = p.ID;
                }

                Page newPage = new Page();
                newPage.ID = maxId + 1;
                diagram.Pages.Add(newPage);
            }

            // References to the source (first) and target (second) pages
            Page sourcePage = diagram.Pages[0];
            Page targetPage = diagram.Pages[1];

            // Find the first triangle shape on the source page
            Shape triangleShape = null;
            foreach (Shape shp in sourcePage.Shapes)
            {
                // Identify by master name "Triangle" (adjust if your diagram uses a different master)
                if (shp.Master != null && shp.Master.Name == "Triangle")
                {
                    triangleShape = shp;
                    break;
                }
            }

            if (triangleShape == null)
            {
                throw new Exception("Triangle shape not found on the first page.");
            }

            // Offset to apply for the cloned shape
            double offsetX = 2.0; // inches
            double offsetY = 2.0; // inches

            // Compute new position based on original shape's PinX/PinY
            double newPinX = triangleShape.XForm.PinX.Value + offsetX;
            double newPinY = triangleShape.XForm.PinY.Value + offsetY;

            // Preserve original size
            double width = triangleShape.XForm.Width.Value;
            double height = triangleShape.XForm.Height.Value;

            // Add a new shape on the target page using the same master
            long newShapeId = targetPage.AddShape(newPinX, newPinY, width, height, triangleShape.Master.Name, false);
            Shape clonedShape = targetPage.Shapes.GetShape(newShapeId);

            // Copy visual properties (fill, line)
            clonedShape.Fill.FillForegnd.Value = triangleShape.Fill.FillForegnd.Value;
            clonedShape.Fill.FillPattern.Value = triangleShape.Fill.FillPattern.Value;
            clonedShape.Line.LineColor.Value = triangleShape.Line.LineColor.Value;
            clonedShape.Line.LineWeight.Value = triangleShape.Line.LineWeight.Value;
            clonedShape.Line.LinePattern.Value = triangleShape.Line.LinePattern.Value;

            // Copy text (if any)
            clonedShape.Text.Value.Clear();
            foreach (var txtItem in triangleShape.Text.Value)
            {
                if (txtItem is Txt txt)
                {
                    clonedShape.Text.Value.Add(new Txt(txt.Text));
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
