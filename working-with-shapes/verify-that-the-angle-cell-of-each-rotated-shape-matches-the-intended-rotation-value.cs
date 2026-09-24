using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be verified
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Define intended rotation angles for shapes (shape ID -> angle in degrees)
                // In a real scenario this could be loaded from a config file or database.
                var intendedAngles = new Dictionary<long, double>
                {
                    // Example entries:
                    // { 1, 45.0 },
                    // { 2, 90.0 }
                };

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the current rotation angle (in degrees)
                        double currentAngle = shape.XForm.Angle.Value;

                        // Consider only shapes that have a non‑zero rotation (i.e., rotated)
                        if (Math.Abs(currentAngle) > 0.0001)
                        {
                            // Check if we have an intended angle for this shape
                            if (intendedAngles.TryGetValue(shape.ID, out double expectedAngle))
                            {
                                // Compare with a tolerance to account for floating‑point precision
                                if (Math.Abs(currentAngle - expectedAngle) > 0.01)
                                {
                                    // Report mismatch
                                    Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' has angle {currentAngle}°, expected {expectedAngle}°.");
                                    // Optionally, throw to halt execution
                                    // throw new Exception($"Angle mismatch for shape ID {shape.ID}");
                                }
                            }
                            else
                            {
                                // No intended angle defined for this shape
                                Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' is rotated to {currentAngle}° but no intended angle is defined.");
                            }
                        }
                    }
                }

                // Optionally, save the diagram after verification (no changes made here)
                // diagram.Save("output_verified.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }