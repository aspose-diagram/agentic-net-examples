using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Begin a transaction-like block
                try
                {
                    // Example modification: add a line segment to the first shape on the active page
                    // Ensure there is at least one shape
                    if (diagram.ActivePage.Shapes.Count > 0)
                    {
                        // Get the first shape
                        Shape shape = diagram.ActivePage.Shapes[0];

                        // Ensure the shape has at least one geometry section
                        if (shape.Geoms.Count > 0)
                        {
                            // Get the first geometry section
                            Geom geom = (Geom)shape.Geoms[0];

                            // Create a MoveTo segment as the starting point (0,0)
                            MoveTo start = new MoveTo();
                            start.X.Value = 0.0;
                            start.Y.Value = 0.0;
                            geom.CoordinateCol.Add(start);

                            // Create a LineTo segment to (2.0, 2.0)
                            LineTo line = new LineTo();
                            line.X.Value = 2.0;
                            line.Y.Value = 2.0;
                            geom.CoordinateCol.Add(line);
                        }
                        else
                        {
                            throw new InvalidOperationException("Shape does not contain any geometry sections.");
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Diagram contains no shapes to modify.");
                    }

                    // If all modifications succeed, save the diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine("Diagram saved successfully to: " + outputPath);
                }
                catch (Exception ex)
                {
                    // If any error occurs, abort the transaction and do not save changes
                    Console.WriteLine("An error occurred during modifications: " + ex.Message);
                    Console.WriteLine("Changes have been discarded; original diagram remains unchanged.");
                }
                finally
                {
                    // Clean up resources
                    diagram.Dispose();
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }