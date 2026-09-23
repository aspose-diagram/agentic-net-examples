using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Create a backup of the original diagram in memory
            MemoryStream backupStream = new MemoryStream();
            diagram.Save(backupStream, SaveFileFormat.Vsdx);
            backupStream.Position = 0;

            try
            {
                // Example modification: add a line segment to the first shape on the first page
                if (diagram.Pages.Count > 0 && diagram.Pages[0].Shapes.Count > 0)
                {
                    Shape shape = diagram.Pages[0].Shapes[0];

                    // Ensure the shape has at least one geometry section
                    if (shape.Geoms.Count == 0)
                    {
                        Geom newGeom = new Geom();
                        shape.Geoms.Add(newGeom);
                    }

                    // Work with the first geometry section
                    Geom targetGeom = (Geom)shape.Geoms[0];

                    // If the geometry has no commands, start with a MoveTo command
                    if (targetGeom.CoordinateCol.Count == 0)
                    {
                        MoveTo move = new MoveTo();
                        move.X.Value = 0.0;
                        move.Y.Value = 0.0;
                        targetGeom.CoordinateCol.Add(move);
                    }

                    // Append a LineTo command to extend the shape
                    LineTo line = new LineTo();
                    line.X.Value = 2.0;
                    line.Y.Value = 2.0;
                    targetGeom.CoordinateCol.Add(line);
                }

                // All modifications succeeded; save the updated diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved successfully.");
            }
            catch (Exception ex)
            {
                // An error occurred; restore the original diagram from the backup
                backupStream.Position = 0;
                diagram = new Diagram(backupStream);
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("All changes have been rolled back.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
