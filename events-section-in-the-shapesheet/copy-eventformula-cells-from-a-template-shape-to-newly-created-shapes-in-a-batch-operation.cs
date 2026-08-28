using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the template diagram that contains the shape with the desired event formulas.
                Diagram diagram = new Diagram("template.vsdx");

                // Assume the template shape is on the first page and has a known universal name.
                Page page = diagram.Pages[0];
                Shape templateShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    if (shp.NameU == "TemplateShape")
                    {
                        templateShape = shp;
                        break;
                    }
                }

                if (templateShape == null)
                {
                    throw new Exception("Template shape 'TemplateShape' not found.");
                }

                // Prepare a list of positions where new shapes will be placed.
                double startX = 2.0;
                double startY = 2.0;
                double offsetX = 2.0;
                int shapeCount = 5;

                // Use the same master as the template shape for new shapes.
                string masterName = templateShape.Master?.Name ?? throw new Exception("Template shape has no master.");

                for (int i = 0; i < shapeCount; i++)
                {
                    double pinX = startX + i * offsetX;
                    double pinY = startY;

                    // Add a new shape based on the master.
                    long newShapeId = page.AddShape(pinX, pinY, masterName);
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Copy event formulas from the template shape to the newly created shape.
                    CopyEventFormulas(templateShape, newShape);
                }

                // Save the modified diagram.
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Copies supported event formulas from a source shape to a target shape.
        /// Only non‑empty formulas are transferred.
        /// </summary>
        static void CopyEventFormulas(Shape source, Shape target)
        {
            // EventDblClick
            if (!string.IsNullOrEmpty(source.Event.EventDblClick.Ufe.F))
                target.Event.EventDblClick.Ufe.F = source.Event.EventDblClick.Ufe.F;

            // EventDrop
            if (!string.IsNullOrEmpty(source.Event.EventDrop.Ufe.F))
                target.Event.EventDrop.Ufe.F = source.Event.EventDrop.Ufe.F;

            // EventXFMod
            if (!string.IsNullOrEmpty(source.Event.EventXFMod.Ufe.F))
                target.Event.EventXFMod.Ufe.F = source.Event.EventXFMod.Ufe.F;

            // EventMultiDrop
            if (!string.IsNullOrEmpty(source.Event.EventMultiDrop.Ufe.F))
                target.Event.EventMultiDrop.Ufe.F = source.Event.EventMultiDrop.Ufe.F;

            // TheText
            if (!string.IsNullOrEmpty(source.Event.TheText.Ufe.F))
                target.Event.TheText.Ufe.F = source.Event.TheText.Ufe.F;

            // TheData
            if (!string.IsNullOrEmpty(source.Event.TheData.Ufe.F))
                target.Event.TheData.Ufe.F = source.Event.TheData.Ufe.F;
        }
    }