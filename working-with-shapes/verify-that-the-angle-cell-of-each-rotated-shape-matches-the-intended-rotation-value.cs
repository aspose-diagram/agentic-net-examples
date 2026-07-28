using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (provide as first argument or modify the literal)
                string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Define the intended rotation angles (in degrees) for shapes identified by their universal name.
                // Adjust this dictionary to match your expected values.
                var expectedAnglesDeg = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Rectangle", 45.0 },
                    { "Process", 90.0 },
                    { "Decision", 0.0 } // example entries
                };

                // Tolerance for floating‑point comparison (in radians)
                const double toleranceRad = 0.0001;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that have no expected entry
                        if (!expectedAnglesDeg.TryGetValue(shape.NameU, out double expectedDeg))
                            continue;

                        // Retrieve the actual rotation angle (stored in radians)
                        double actualRad = shape.XForm.Angle.Value;

                        // Convert expected degrees to radians for comparison
                        double expectedRad = expectedDeg * Math.PI / 180.0;

                        // Compare with tolerance
                        if (Math.Abs(actualRad - expectedRad) > toleranceRad)
                        {
                            // Report mismatch and abort
                            throw new Exception(
                                $"Shape '{shape.NameU}' (ID={shape.ID}) has angle {actualRad * 180.0 / Math.PI:F2}°, " +
                                $"expected {expectedDeg:F2}°.");
                        }
                        else
                        {
                            Console.WriteLine(
                                $"Shape '{shape.NameU}' (ID={shape.ID}) rotation verified: {expectedDeg:F2}°.");
                        }
                    }
                }

                Console.WriteLine("All specified shape rotations match the intended values.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }