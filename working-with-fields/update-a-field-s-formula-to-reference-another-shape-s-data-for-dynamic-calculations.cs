using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Identify the source shape (the shape whose data we want to reference)
                // For this example we assume the source shape has the universal name "SourceShape"
                Shape sourceShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU != null && shape.NameU.Equals("SourceShape", StringComparison.OrdinalIgnoreCase))
                        {
                            sourceShape = shape;
                            break;
                        }
                    }
                    if (sourceShape != null) break;
                }

                if (sourceShape == null)
                    throw new Exception("Source shape 'SourceShape' not found.");

                // Identify the target shape (the shape containing the field to update)
                // For this example we assume the target shape has the universal name "TargetShape"
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU != null && shape.NameU.Equals("TargetShape", StringComparison.OrdinalIgnoreCase))
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null) break;
                }

                if (targetShape == null)
                    throw new Exception("Target shape 'TargetShape' not found.");

                // Ensure the target shape has at least one field
                if (targetShape.Fields == null || targetShape.Fields.Count == 0)
                    throw new Exception("Target shape does not contain any fields to update.");

                // Update the first field's formula to reference the source shape's Data1 cell
                // Visio formula syntax: "Sheet.<ID>!Data1"
                // Use the source shape's ID to build the reference
                string formula = $"Sheet.{sourceShape.ID}!Data1";
                Field fieldToUpdate = targetShape.Fields[0];
                fieldToUpdate.Value.Ufev.F = formula;

                // Optionally refresh the shape to apply changes immediately
                targetShape.RefreshData();

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Field formula updated and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }