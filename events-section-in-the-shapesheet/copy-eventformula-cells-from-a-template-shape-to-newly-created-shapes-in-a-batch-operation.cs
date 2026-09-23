using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the source diagram containing the template shape
                string sourcePath = "TemplateDiagram.vsdx";
                Diagram diagram = new Diagram(sourcePath);

                // Assume the template shape is on the first page and has a unique NameU "TemplateShape"
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
                    throw new Exception("Template shape with NameU 'TemplateShape' not found.");
                }

                // Retrieve the master name of the template shape to reuse for new shapes
                string masterName = templateShape.Master?.Name;
                if (string.IsNullOrEmpty(masterName))
                {
                    throw new Exception("Template shape does not have an associated master.");
                }

                // Batch create new shapes and copy event formulas from the template
                int numberOfNewShapes = 5;
                double startX = 2.0;
                double startY = 2.0;
                double offsetX = 2.0;
                double offsetY = 2.0;

                for (int i = 0; i < numberOfNewShapes; i++)
                {
                    // Add a new shape using the same master as the template
                    double pinX = startX + i * offsetX;
                    double pinY = startY + i * offsetY;
                    long newShapeId = page.AddShape(pinX, pinY, masterName, false);
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Copy all supported event formulas from the template shape
                    CopyEventFormulas(templateShape, newShape);
                }

                // Save the modified diagram
                string outputPath = "OutputDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Copies event formula cells from a source shape to a target shape.
        /// </summary>
        /// <param name="source">Shape containing the original event formulas.</param>
        /// <param name="target">Shape that will receive the copied event formulas.</param>
        static void CopyEventFormulas(Shape source, Shape target)
        {
            // EventXFMod
            target.Event.EventXFMod.Ufe.F = source.Event.EventXFMod.Ufe.F;

            // EventDblClick
            target.Event.EventDblClick.Ufe.F = source.Event.EventDblClick.Ufe.F;

            // EventDrop
            target.Event.EventDrop.Ufe.F = source.Event.EventDrop.Ufe.F;

            // EventMultiDrop
            target.Event.EventMultiDrop.Ufe.F = source.Event.EventMultiDrop.Ufe.F;

            // TheText (shape text change event)
            target.Event.TheText.Ufe.F = source.Event.TheText.Ufe.F;

            // TheData (shape data change event)
            target.Event.TheData.Ufe.F = source.Event.TheData.Ufe.F;
        }
    }