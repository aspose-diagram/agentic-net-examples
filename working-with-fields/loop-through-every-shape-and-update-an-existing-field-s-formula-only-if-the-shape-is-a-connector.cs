using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramConnectorFieldUpdater <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (1‑D shapes).
                    if (shape.OneD)
                    {
                        // Ensure the shape has at least one field.
                        if (shape.Fields != null && shape.Fields.Count > 0)
                        {
                            // Update the formula of the first field.
                            Field field = shape.Fields[0];
                            // Example formula – replace with the desired expression.
                            field.Value.Ufev.F = "Width*Height";
                        }
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
    }