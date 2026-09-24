using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramMasterReplace <inputPath> <outputPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Names of the deprecated master and the updated master
            const string deprecatedMasterName = "OldMaster";
            const string updatedMasterName = "NewMaster";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Collect IDs of shapes that use the deprecated master
                List<long> shapesToReplace = new List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == deprecatedMasterName)
                    {
                        shapesToReplace.Add(shape.ID);
                    }
                }

                // Replace each identified shape
                foreach (long shapeId in shapesToReplace)
                {
                    // Retrieve the original shape
                    Shape oldShape = page.Shapes.GetShape(shapeId);

                    // Preserve geometry
                    double pinX = oldShape.XForm.PinX.Value;
                    double pinY = oldShape.XForm.PinY.Value;
                    double width = oldShape.XForm.Width.Value;
                    double height = oldShape.XForm.Height.Value;

                    // Preserve plain text (if any)
                    string plainText = oldShape.Text.Value.ToString();

                    // Remove the old shape
                    page.Shapes.Remove(oldShape);

                    // Add a new shape using the updated master
                    long newShapeId = page.AddShape(pinX, pinY, width, height, updatedMasterName, false);
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Restore the text content
                    if (!string.IsNullOrWhiteSpace(plainText))
                    {
                        newShape.Text.Value.Clear();
                        newShape.Text.Value.Add(new Txt(plainText));
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
    }