using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect at least two arguments: input file path and shape name.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: FillInheritanceToggle <input.vsdx> <shapeName> [output.vsdx]");
                return;
            }

            string inputPath = args[0];
            string shapeName = args[1];
            string outputPath = args.Length >= 3 ? args[2] : inputPath; // Overwrite if no output specified.

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape by its universal name (NameU) across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals(shapeName, StringComparison.OrdinalIgnoreCase))
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                throw new Exception($"Shape with name '{shapeName}' not found in the diagram.");
            }

            // Determine if the fill foreground color is currently inherited.
            bool isInherited = targetShape.Fill.FillForegnd.Value == targetShape.InheritFill.FillForegnd.Value;

            if (isInherited)
            {
                // Break inheritance by assigning a distinct fill color (red).
                targetShape.Fill.FillForegnd.Value = "#FF0000";
                Console.WriteLine($"Fill inheritance broken for shape '{shapeName}'. Set FillForegnd to red.");
            }
            else
            {
                // Restore inheritance by copying the inherited fill value.
                targetShape.Fill.FillForegnd.Value = targetShape.InheritFill.FillForegnd.Value;
                Console.WriteLine($"Fill inheritance restored for shape '{shapeName}'.");
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }