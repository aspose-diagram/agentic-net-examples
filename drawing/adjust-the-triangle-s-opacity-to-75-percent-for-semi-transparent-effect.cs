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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            bool triangleFound = false;

            // Iterate through all pages and shapes to locate a triangle shape.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape uses the "Triangle" master.
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        // Set fill transparency to 75 percent (semi‑transparent).
                        // The FillForegndTrans cell expects a percentage value.
                        shape.Fill.FillForegndTrans.Value = 75;

                        triangleFound = true;
                        // If only the first triangle should be modified, break out.
                        break;
                    }
                }

                if (triangleFound)
                    break;
            }

            if (!triangleFound)
            {
                Console.Error.WriteLine("No triangle shape found in the diagram.");
                return;
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}