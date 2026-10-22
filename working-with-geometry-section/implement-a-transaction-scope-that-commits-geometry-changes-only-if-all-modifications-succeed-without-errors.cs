using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Create an in‑memory backup of the original diagram (acts as a transaction rollback source)
            using (MemoryStream backupStream = new MemoryStream())
            {
                diagram.Save(backupStream, SaveFileFormat.Vsdx);
                backupStream.Position = 0; // Reset for potential reload

                bool modificationsSucceeded = false;

                try
                {
                    // ------------------------------------------------------------
                    // Begin geometry modifications (the "transaction")
                    // ------------------------------------------------------------

                    // Ensure there is at least one page and one shape to work with
                    if (diagram.Pages.Count > 0 && diagram.Pages[0].Shapes.Count > 0)
                    {
                        // Retrieve the first shape on the first page
                        Shape shape = diagram.Pages[0].Shapes[0];

                        // Check if the shape already contains a geometry collection
                        if (shape.Geoms.Count > 0)
                        {
                            // Work with the first geometry object
                            Geom geom = (Geom)shape.Geoms[0];

                            // Add a MoveTo segment at (0,0) if needed
                            MoveTo move = new MoveTo();
                            move.X.Value = 0.0;
                            move.Y.Value = 0.0;
                            geom.CoordinateCol.Add(move);

                            // Append a LineTo segment to (2.0, 2.0)
                            LineTo line = new LineTo();
                            line.X.Value = 2.0;
                            line.Y.Value = 2.0;
                            geom.CoordinateCol.Add(line);
                        }
                        else
                        {
                            // No geometry exists – create a new one and add it to the shape
                            Geom newGeom = new Geom();

                            // MoveTo (0,0)
                            MoveTo move = new MoveTo();
                            move.X.Value = 0.0;
                            move.Y.Value = 0.0;
                            newGeom.CoordinateCol.Add(move);

                            // LineTo (2.0, 2.0)
                            LineTo line = new LineTo();
                            line.X.Value = 2.0;
                            line.Y.Value = 2.0;
                            newGeom.CoordinateCol.Add(line);

                            // Add the newly created geometry to the shape
                            shape.Geoms.Add(newGeom);
                        }

                        // If we reach this point, all geometry operations succeeded
                        modificationsSucceeded = true;
                    }
                    else
                    {
                        Console.Error.WriteLine("Diagram does not contain any pages or shapes to modify.");
                        throw new Exception("No shape available for modification.");
                    }
                }
                catch (Exception modEx)
                {
                    // Log any errors that occurred during the geometry changes
                    Console.Error.WriteLine($"Error during geometry modifications: {modEx.Message}");
                }

                // ------------------------------------------------------------
                // Commit or rollback based on the success flag
                // ------------------------------------------------------------
                if (modificationsSucceeded)
                {
                    // All changes succeeded – save the modified diagram to the output path
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to {outputPath}");
                }
                else
                {
                    // Something failed – revert to the original state by reloading from the backup
                    diagram = new Diagram(backupStream);
                    Console.WriteLine("Modifications failed. Original diagram remains unchanged.");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., loading failures)
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}