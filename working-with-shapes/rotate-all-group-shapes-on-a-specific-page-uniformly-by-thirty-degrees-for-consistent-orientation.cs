using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, output Visio file, and target page name.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <pageName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pageName = args[2];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the specified page by name.
                Page page = diagram.Pages.GetPage(pageName);
                if (page == null)
                {
                    Console.Error.WriteLine($"Page \"{pageName}\" not found in the diagram.");
                    return;
                }

                // Iterate over all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify group shapes. Use Type == TypeValue.Group as a reliable check.
                    if (shape.Type == TypeValue.Group)
                    {
                        // Rotate the group shape by 30 degrees using SetAngle.
                        shape.SetAngle(30.0);
                    }
                }

                // Save the modified diagram to the output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}