using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (ensures proper disposal).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify a group shape. A group shape has Type == TypeValue.Group.
                        if (shape.Type == TypeValue.Group)
                        {
                            // Rotate each sub‑shape inside the group by 20 degrees.
                            foreach (Shape child in shape.Shapes)
                            {
                                // Skip deleted child shapes.
                                if (child.Del == BOOL.True)
                                    continue;

                                // Add 20 degrees to the existing rotation angle.
                                double newAngle = child.XForm.Angle.Value + 20.0;

                                // Apply the new rotation using the SetAngle method.
                                child.SetAngle(newAngle);
                            }

                            // If only the first group should be processed, uncomment the break statements below.
                            // break; // Exit shape loop after handling the first group.
                        }
                    }

                    // If only the first group on the first page should be processed, uncomment the break below.
                    // break; // Exit page loop after handling the first group.
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}