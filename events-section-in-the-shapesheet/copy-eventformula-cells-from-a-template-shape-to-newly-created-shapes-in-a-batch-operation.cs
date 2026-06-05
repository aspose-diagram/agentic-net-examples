using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths – replace with actual file locations
                string templatePath = "template.vsdx";
                string outputPath = "result.vsdx";

                // Load the template diagram
                Diagram diagram = new Diagram(templatePath);

                // Assume the template shape is the first shape on the first page
                Page templatePage = diagram.Pages[0];
                Shape templateShape = templatePage.Shapes.GetShape(1); // shape ID 1

                // Ensure the template shape has a master (required for adding new shapes)
                if (templateShape.Master == null)
                {
                    throw new Exception("Template shape does not have an associated master.");
                }

                // Number of new shapes to create
                int shapeCount = 5;
                double startX = 2.0;
                double startY = 2.0;
                double offsetX = 2.0; // spacing between shapes

                for (int i = 0; i < shapeCount; i++)
                {
                    // Add a new shape using the same master as the template shape
                    double pinX = startX + i * offsetX;
                    double pinY = startY;
                    long newShapeId = templatePage.AddShape(pinX, pinY, templateShape.Master.Name);
                    Shape newShape = templatePage.Shapes.GetShape(newShapeId);

                    // Copy all event formulas from the template shape to the new shape
                    CopyEventFormulas(templateShape, newShape);
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Copies event formulas from a source shape to a target shape.
        /// Only non‑empty formulas are transferred.
        /// </summary>
        static void CopyEventFormulas(Shape source, Shape target)
        {
            // EventXFMod
            if (!string.IsNullOrWhiteSpace(source.Event.EventXFMod.Ufe.F))
                target.Event.EventXFMod.Ufe.F = source.Event.EventXFMod.Ufe.F;

            // EventDblClick
            if (!string.IsNullOrWhiteSpace(source.Event.EventDblClick.Ufe.F))
                target.Event.EventDblClick.Ufe.F = source.Event.EventDblClick.Ufe.F;

            // EventDrop
            if (!string.IsNullOrWhiteSpace(source.Event.EventDrop.Ufe.F))
                target.Event.EventDrop.Ufe.F = source.Event.EventDrop.Ufe.F;

            // EventMultiDrop
            if (!string.IsNullOrWhiteSpace(source.Event.EventMultiDrop.Ufe.F))
                target.Event.EventMultiDrop.Ufe.F = source.Event.EventMultiDrop.Ufe.F;

            // TheText
            if (!string.IsNullOrWhiteSpace(source.Event.TheText.Ufe.F))
                target.Event.TheText.Ufe.F = source.Event.TheText.Ufe.F;

            // TheData
            if (!string.IsNullOrWhiteSpace(source.Event.TheData.Ufe.F))
                target.Event.TheData.Ufe.F = source.Event.TheData.Ufe.F;
        }
    }