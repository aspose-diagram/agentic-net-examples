using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the first diagram
            Diagram diagram1 = new Diagram("diagram1.vdx");

            // Load the second diagram that contains identical shapes
            Diagram diagram2 = new Diagram("diagram2.vdx");

            // Combine the second diagram into the first one
            diagram1.Combine(diagram2);

            // Validate that no duplicate shape names exist after the combine operation
            ValidateUniqueShapeNames(diagram1);

            // Save the combined diagram
            diagram1.Save("combined_output.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static void ValidateUniqueShapeNames(Diagram diagram)
    {
        var seenNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Prefer the universal name; fall back to the local name if needed
                string shapeName = shape.NameU ?? shape.Name;

                if (!string.IsNullOrEmpty(shapeName))
                {
                    if (!seenNames.Add(shapeName))
                    {
                        // Duplicate name detected – raise an exception or handle as required
                        throw new InvalidOperationException($"Duplicate shape name detected: {shapeName}");
                    }
                }
            }
        }
    }
}
