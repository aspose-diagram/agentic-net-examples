using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect: inputFilePath shapeName [outputFilePath]
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: FillInheritanceToggle <input.vsdx> <shapeName> [output.vsdx]");
                return;
            }

            string inputPath = args[0];
            string targetName = args[1];
            string outputPath = args.Length >= 3 ? args[2] : System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(inputPath) ?? "",
                System.IO.Path.GetFileNameWithoutExtension(inputPath) + "_toggled.vsdx");

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            Shape foundShape = null;

            // Search all pages for the shape with the specified universal name
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (!string.IsNullOrEmpty(shape.NameU) &&
                        shape.NameU.Equals(targetName, StringComparison.OrdinalIgnoreCase))
                    {
                        foundShape = shape;
                        break;
                    }
                }
                if (foundShape != null) break;
            }

            if (foundShape == null)
            {
                throw new Exception($"Shape with name \"{targetName}\" not found in the diagram.");
            }

            // Determine if the shape is currently inheriting its fill foreground color
            bool isInheriting = 
                foundShape.Fill.FillForegnd.Value == foundShape.InheritFill.FillForegnd.Value &&
                foundShape.Fill.FillPattern.Value == foundShape.InheritFill.FillPattern.Value;

            if (isInheriting)
            {
                // Switch to a custom solid red fill
                foundShape.Fill.FillForegnd.Value = "#FF0000"; // Red color
                foundShape.Fill.FillPattern.Value = 1; // Solid fill pattern
                Console.WriteLine($"Shape \"{targetName}\" was inheriting fill. Applied solid red fill.");
            }
            else
            {
                // Revert to inherited fill values
                foundShape.Fill.FillForegnd.Value = foundShape.InheritFill.FillForegnd.Value;
                foundShape.Fill.FillPattern.Value = foundShape.InheritFill.FillPattern.Value;
                Console.WriteLine($"Shape \"{targetName}\" had custom fill. Restored inherited fill.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to \"{outputPath}\".");
        }
    }