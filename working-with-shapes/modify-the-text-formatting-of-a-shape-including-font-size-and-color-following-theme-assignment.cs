using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Find a shape to modify (example: first shape with NameU "Rectangle")
                Shape targetShape = null;
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    if (shape.NameU != null && shape.NameU.Equals("Rectangle", StringComparison.OrdinalIgnoreCase))
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("Target shape not found.");
                    return;
                }

                // Ensure the shape has a text collection
                if (targetShape.Text == null)
                    targetShape.Text = new Text();

                // Clear existing text runs (optional)
                targetShape.Text.Value.Clear();

                // Add new text content
                targetShape.Text.Value.Add(new Txt("Sample Text"));

                // Create a character formatting entry for the first character run (IX = 0)
                Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
                charFormat.IX = 0; // Index of the character run
                charFormat.FontName.Value = "Calibri";               // Font name
                charFormat.Size.Value = 12.0 / 72.0;                 // Font size in inches (12 pt)
                charFormat.Color.Value = "#FF0000";                 // Font color (red) in HEX

                // Add the character formatting to the shape
                targetShape.Chars.Add(charFormat);

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }