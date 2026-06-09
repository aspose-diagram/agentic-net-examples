using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Create a new geometry definition (rectangle matching the shape size)
                        Geom geom = new Geom();

                        // Move to the origin (0,0)
                        MoveTo move = new MoveTo();
                        move.X.Value = 0;
                        move.Y.Value = 0;
                        geom.CoordinateCol.Add(move);

                        // Line to top‑right corner
                        LineTo line1 = new LineTo();
                        line1.X.Value = shape.XForm.Width.Value;
                        line1.Y.Value = 0;
                        geom.CoordinateCol.Add(line1);

                        // Line to bottom‑right corner
                        LineTo line2 = new LineTo();
                        line2.X.Value = shape.XForm.Width.Value;
                        line2.Y.Value = shape.XForm.Height.Value;
                        geom.CoordinateCol.Add(line2);

                        // Line to bottom‑left corner
                        LineTo line3 = new LineTo();
                        line3.X.Value = 0;
                        line3.Y.Value = shape.XForm.Height.Value;
                        geom.CoordinateCol.Add(line3);

                        // Close the rectangle back to the origin
                        LineTo line4 = new LineTo();
                        line4.X.Value = 0;
                        line4.Y.Value = 0;
                        geom.CoordinateCol.Add(line4);

                        // Assign the new geometry to the shape (replace existing geometry)
                        shape.Geoms.Clear();          // Remove any previous geometry
                        shape.Geoms.Add(geom);        // Add the standard template
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }