using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Locate a shape that is based on a master (the source master shape)
                Shape masterShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    if (shp.Master != null)
                    {
                        masterShape = shp;
                        break;
                    }
                }

                if (masterShape == null)
                {
                    Console.WriteLine("No master-based shape found in the diagram.");
                    return;
                }

                // Create a few duplicate shapes based on the same master
                const int duplicateCount = 5;
                double offsetX = 2.0; // inches offset for each duplicate
                double offsetY = 0.0;

                for (int i = 0; i < duplicateCount; i++)
                {
                    // Position the duplicate shape
                    double pinX = masterShape.XForm.PinX.Value + (i + 1) * offsetX;
                    double pinY = masterShape.XForm.PinY.Value + offsetY;

                    // Add a new shape using the same master name
                    long dupId = page.AddShape(pinX, pinY, masterShape.Master.Name);
                    Shape dupShape = page.Shapes.GetShape(dupId);

                    // Clone the EventSection cells from the master shape to the duplicate
                    CloneEventSection(masterShape, dupShape);
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Copies all supported event formulas from a source shape to a target shape.
        /// </summary>
        /// <param name="source">Shape containing the original EventSection.</param>
        /// <param name="target">Shape that will receive the copied event formulas.</param>
        static void CloneEventSection(Shape source, Shape target)
        {
            if (source.Event == null || target.Event == null)
                return;

            // EventDblClick
            if (source.Event.EventDblClick != null && source.Event.EventDblClick.Ufe != null)
                target.Event.EventDblClick.Ufe.F = source.Event.EventDblClick.Ufe.F;

            // EventDrop
            if (source.Event.EventDrop != null && source.Event.EventDrop.Ufe != null)
                target.Event.EventDrop.Ufe.F = source.Event.EventDrop.Ufe.F;

            // EventMultiDrop
            if (source.Event.EventMultiDrop != null && source.Event.EventMultiDrop.Ufe != null)
                target.Event.EventMultiDrop.Ufe.F = source.Event.EventMultiDrop.Ufe.F;

            // EventXFMod
            if (source.Event.EventXFMod != null && source.Event.EventXFMod.Ufe != null)
                target.Event.EventXFMod.Ufe.F = source.Event.EventXFMod.Ufe.F;

            // TheText
            if (source.Event.TheText != null && source.Event.TheText.Ufe != null)
                target.Event.TheText.Ufe.F = source.Event.TheText.Ufe.F;

            // TheData
            if (source.Event.TheData != null && source.Event.TheData.Ufe != null)
                target.Event.TheData.Ufe.F = source.Event.TheData.Ufe.F;
        }
    }