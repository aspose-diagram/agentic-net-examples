using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be validated
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Define the intended rotation angles for specific shapes (shape ID -> expected angle in degrees)
                // Adjust this dictionary according to your expected values.
                var expectedAngles = new Dictionary<long, double>
                {
                    // Example entries:
                    // { 5, 45.0 },
                    // { 12, 90.0 }
                };

                // Tolerance for floating‑point comparison (degrees)
                const double tolerance = 0.001;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are not in the expected list
                        if (!expectedAngles.ContainsKey(shape.ID))
                            continue;

                        // Retrieve the actual rotation angle from the Angle cell (in degrees)
                        double actualAngle = shape.XForm.Angle.Value;

                        // Retrieve the intended angle
                        double intendedAngle = expectedAngles[shape.ID];

                        // Compare with tolerance
                        if (Math.Abs(actualAngle - intendedAngle) > tolerance)
                        {
                            // Report mismatch
                            string message = $"Shape ID {shape.ID} on page '{page.Name}' has angle {actualAngle}°, " +
                                             $"but expected {intendedAngle}°.";
                            // Throw exception to indicate verification failure
                            throw new Exception(message);
                        }
                    }
                }

                Console.WriteLine("All shape angles match the intended values.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }