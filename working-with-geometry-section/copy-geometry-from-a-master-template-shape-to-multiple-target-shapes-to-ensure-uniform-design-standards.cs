using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) – validate existence.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output file path – same folder with suffix "_updated.vsdx".
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_updated.vsdx");

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Assume the template shape is identified by its NameU property "TemplateMaster".
            // Adjust this name as needed for your specific template.
            const string templateShapeNameU = "TemplateMaster";

            // Locate the template shape on the first page.
            Shape? templateShape = null;
            if (diagram.Pages.Count > 0)
            {
                Page firstPage = diagram.Pages[0];
                foreach (Shape shape in firstPage.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals(templateShapeNameU, StringComparison.OrdinalIgnoreCase))
                    {
                        templateShape = shape;
                        break;
                    }
                }
            }

            if (templateShape == null)
            {
                Console.Error.WriteLine($"Template shape with NameU \"{templateShapeNameU}\" not found.");
                return;
            }

            // Copy geometry from the template shape to all other shapes on the first page.
            if (diagram.Pages.Count > 0)
            {
                Page firstPage = diagram.Pages[0];
                foreach (Shape targetShape in firstPage.Shapes)
                {
                    // Skip the template shape itself.
                    if (targetShape.ID == templateShape.ID)
                        continue;

                    // ----- Copy Position and Size (XForm) -----
                    targetShape.XForm.PinX.Value = templateShape.XForm.PinX.Value;
                    targetShape.XForm.PinY.Value = templateShape.XForm.PinY.Value;
                    targetShape.XForm.Width.Value = templateShape.XForm.Width.Value;
                    targetShape.XForm.Height.Value = templateShape.XForm.Height.Value;
                    targetShape.XForm.Angle.Value = templateShape.XForm.Angle.Value;

                    // ----- Copy Fill Properties -----
                    targetShape.Fill.FillForegnd.Value = templateShape.Fill.FillForegnd.Value;
                    targetShape.Fill.FillForegndTrans.Value = templateShape.Fill.FillForegndTrans.Value;
                    targetShape.Fill.FillBkgnd.Value = templateShape.Fill.FillBkgnd.Value;
                    targetShape.Fill.FillPattern.Value = templateShape.Fill.FillPattern.Value;
                    targetShape.Fill.ShdwForegnd.Value = templateShape.Fill.ShdwForegnd.Value;
                    targetShape.Fill.ShdwBkgnd.Value = templateShape.Fill.ShdwBkgnd.Value;
                    targetShape.Fill.ShdwPattern.Value = templateShape.Fill.ShdwPattern.Value;
                    targetShape.Fill.ShapeShdwType.Value = templateShape.Fill.ShapeShdwType.Value;
                    targetShape.Fill.ShapeShdwObliqueAngle.Value = templateShape.Fill.ShapeShdwObliqueAngle.Value;
                    targetShape.Fill.ShapeShdwOffsetX.Value = templateShape.Fill.ShapeShdwOffsetX.Value;
                    targetShape.Fill.ShapeShdwOffsetY.Value = templateShape.Fill.ShapeShdwOffsetY.Value;

                    // ----- Copy Line (Border) Properties -----
                    targetShape.Line.LineWeight.Value = templateShape.Line.LineWeight.Value;
                    targetShape.Line.LineColor.Value = templateShape.Line.LineColor.Value;
                    targetShape.Line.BeginArrow.Value = templateShape.Line.BeginArrow.Value;
                    targetShape.Line.BeginArrowSize.Value = templateShape.Line.BeginArrowSize.Value;
                    targetShape.Line.EndArrow.Value = templateShape.Line.EndArrow.Value;
                    targetShape.Line.EndArrowSize.Value = templateShape.Line.EndArrowSize.Value;
                    targetShape.Line.LinePattern.Value = templateShape.Line.LinePattern.Value;
                }
            }

            // Save the updated diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with updated geometry to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}