using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                const string inputPath = "input.vsdx";
                // Path for the exported file after validation
                const string outputPath = "validated_output.vsdx";

                // Load the diagram using a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Ensure the shape has an associated master
                            if (shape.Master == null)
                                continue; // Shapes without a master cannot inherit line formatting

                            // Validate that line formatting is inherited from the master
                            if (!IsLineFormattingInherited(shape))
                            {
                                // If validation fails, throw an exception with details
                                throw new Exception(
                                    $"Shape ID {shape.ID} on page '{page.Name}' does not inherit line formatting from its master '{shape.Master.Name}'.");
                            }
                        }
                    }

                    // All shapes passed validation; export the diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram validation completed and file saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Determines whether the shape's line formatting matches the inherited line values.
        /// </summary>
        /// <param name="shape">The shape to evaluate.</param>
        /// <returns>True if line color, weight, and pattern are inherited; otherwise false.</returns>
        private static bool IsLineFormattingInherited(Shape shape)
        {
            // Compare line color
            bool colorMatches = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;

            // Compare line weight
            bool weightMatches = shape.Line.LineWeight.Value == shape.InheritLine.LineWeight.Value;

            // Compare line pattern (enum comparison)
            bool patternMatches = shape.Line.LinePattern.Value == shape.InheritLine.LinePattern.Value;

            // Return true only if all three properties match the inherited values
            return colorMatches && weightMatches && patternMatches;
        }
    }