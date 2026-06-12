using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input file, shape name, output file
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: FillInheritanceToggle <input.vsdx> <shapeName> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string shapeName = args[1];
            string outputPath = args[2];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            bool shapeFound = false;

            // Search all pages for the shape with the specified universal name
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals(shapeName, StringComparison.OrdinalIgnoreCase))
                    {
                        shapeFound = true;

                        // Determine whether the shape currently inherits its fill color
                        bool isInherited = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;

                        if (isInherited)
                        {
                            // Disable inheritance by assigning a custom fill color (red)
                            shape.Fill.FillForegnd.Value = "#FF0000";
                            Console.WriteLine($"Fill inheritance disabled for shape '{shapeName}'. Set to red.");
                        }
                        else
                        {
                            // Enable inheritance by restoring the inherited fill color
                            shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                            Console.WriteLine($"Fill inheritance enabled for shape '{shapeName}'. Restored inherited color.");
                        }

                        // No need to continue searching once the shape is processed
                        break;
                    }
                }

                if (shapeFound) break;
            }

            if (!shapeFound)
            {
                throw new Exception($"Shape with name '{shapeName}' not found in the diagram.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }