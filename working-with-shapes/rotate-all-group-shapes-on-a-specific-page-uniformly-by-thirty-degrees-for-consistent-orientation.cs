using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, page name, output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <pageName> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string pageName = args[1];
        string outputPath = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the specified page; GetPage returns null if not found
                Page page = diagram.Pages.GetPage(pageName);
                if (page == null)
                {
                    Console.Error.WriteLine($"Page \"{pageName}\" not found in the diagram.");
                    return;
                }

                // Iterate over all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify group shapes using the Type property (IsGroup does not exist)
                    if (shape.Type == TypeValue.Group)
                    {
                        // Rotate the group by adding 30 degrees to its current angle
                        double currentAngle = shape.XForm.Angle.Value;
                        shape.XForm.Angle.Value = currentAngle + 30.0;
                    }
                }

                // Save the modified diagram to the output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
