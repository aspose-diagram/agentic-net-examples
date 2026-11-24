using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the first shape whose master name is "Diamond".
                Shape diamondShape = null;
                Page diamondPage = null;

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master != null && shape.Master.Name == "Diamond")
                        {
                            diamondShape = shape;
                            diamondPage = page;
                            break;
                        }
                    }
                    if (diamondShape != null)
                        break;
                }

                if (diamondShape == null || diamondPage == null)
                {
                    Console.Error.WriteLine("No diamond shape was found in the diagram.");
                    return;
                }

                // Store original geometry.
                double originalPinX = diamondShape.XForm.PinX.Value;
                double originalPinY = diamondShape.XForm.PinY.Value;
                double originalWidth = diamondShape.XForm.Width.Value;
                double originalHeight = diamondShape.XForm.Height.Value;
                string masterName = diamondShape.Master.Name;

                // Calculate new position (0.5 inches to the right).
                double offsetPinX = originalPinX + 0.5;
                double offsetPinY = originalPinY; // same vertical position

                // Add a duplicate shape on the same page.
                long newShapeId = diamondPage.AddShape(offsetPinX, offsetPinY, masterName, false);
                Shape newShape = diamondPage.Shapes.GetShape(newShapeId);

                // Copy size.
                newShape.XForm.Width.Value = originalWidth;
                newShape.XForm.Height.Value = originalHeight;

                // Copy text (if any).
                string originalText = diamondShape.Text.Value.Text;
                if (!string.IsNullOrWhiteSpace(originalText))
                {
                    newShape.Text.Value.Clear();
                    newShape.Text.Value.Add(new Txt(originalText));
                }

                // Copy fill formatting.
                newShape.Fill.FillForegnd.Value = diamondShape.Fill.FillForegnd.Value;
                newShape.Fill.FillBkgnd.Value = diamondShape.Fill.FillBkgnd.Value;
                newShape.Fill.FillPattern.Value = diamondShape.Fill.FillPattern.Value;

                // Copy line formatting.
                newShape.Line.LineColor.Value = diamondShape.Line.LineColor.Value;
                newShape.Line.LineWeight.Value = diamondShape.Line.LineWeight.Value;

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}