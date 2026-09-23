using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Name of the custom property to check
            const string targetPropName = "MyFlag";

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Props collection
                    if (shape.Props == null)
                        continue;

                    // Look for the custom property with the specified name
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == targetPropName && 
                            string.Equals(prop.Value.Val, "TRUE", StringComparison.OrdinalIgnoreCase))
                        {
                            // Create a new text field and set its value
                            Field field = new Field();
                            field.Value.Val = "AddedField";

                            // Add the field to the shape
                            shape.Fields.Add(field);

                            // No need to check other properties for this shape
                            break;
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
