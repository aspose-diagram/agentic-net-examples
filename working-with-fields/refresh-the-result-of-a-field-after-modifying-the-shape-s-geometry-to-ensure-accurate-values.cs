using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Locate the target shape (example: shape with universal name "Rectangle")
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == "Rectangle")
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    throw new Exception("Target shape not found.");
                }

                // Modify the shape's geometry (e.g., change width and height)
                targetShape.XForm.Width.Value = 2.0;   // width in inches
                targetShape.XForm.Height.Value = 1.0;  // height in inches

                // Refresh the shape to recalculate any fields that depend on geometry
                targetShape.RefreshData();

                // (Optional) Output the value of the first field after refresh
                if (targetShape.Fields.Count > 0)
                {
                    string fieldValue = targetShape.Fields[0].Value.Val;
                    Console.WriteLine($"Field 0 value after refresh: {fieldValue}");
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }