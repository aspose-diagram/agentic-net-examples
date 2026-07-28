using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths – adjust as needed
                string stencilPath = "basic.vssx"; // stencil containing the master (e.g., Rectangle)
                string masterName = "Rectangle";
                string outputPdf = "output.pdf";

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                    diagram.Pages.Add(new Page());

                Page page = diagram.Pages[0];

                // Import the master from the stencil
                int masterId = diagram.AddMaster(stencilPath, masterName);
                Master master = diagram.Masters.GetMaster(masterId);

                if (master == null)
                    throw new Exception($"Master '{masterName}' could not be loaded from stencil '{stencilPath}'.");

                // Retrieve the shape inside the master (the first shape usually has ID = 1)
                Shape masterShape = master.Shapes.GetShape(1);
                if (masterShape == null)
                    throw new Exception("Master shape not found.");

                // Configure a gradient fill on the master shape
                masterShape.Fill.FillPattern.Value = 25; // Gradient pattern
                masterShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                masterShape.Fill.GradientFill.GradientDir.Value = 0; // Left‑to‑right
                masterShape.Fill.GradientFill.GradientStops.Clear();
                masterShape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#FF0000", MeasureConst.Undefined)); // Red at start
                masterShape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#00FF00", MeasureConst.Undefined)); // Green at end

                // Add a shape that uses the master (inherits the gradient)
                long shapeId = page.AddShape(2.0, 2.0, masterName);
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                    throw new Exception("Failed to retrieve the shape created from the master.");

                // Validate inheritance: compare gradient stops between shape and master
                var masterStops = masterShape.Fill.GradientFill.GradientStops;
                var shapeStops = shape.InheritFill.GradientFill.GradientStops;

                if (masterStops.Count != shapeStops.Count)
                    throw new Exception("Gradient stop count mismatch between master and shape.");

                for (int i = 0; i < masterStops.Count; i++)
                {
                    GradientStop masterStop = masterStops[i];
                    GradientStop shapeStop = shapeStops[i];

                    if (masterStop.Position.Value != shapeStop.Position.Value ||
                        masterStop.Color.Value != shapeStop.Color.Value)
                    {
                        throw new Exception($"Gradient stop {i} does not match between master and shape.");
                    }
                }

                // Export the diagram to PDF
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                diagram.Save(outputPdf, pdfOptions);

                Console.WriteLine("Gradient inheritance validated and PDF exported successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }