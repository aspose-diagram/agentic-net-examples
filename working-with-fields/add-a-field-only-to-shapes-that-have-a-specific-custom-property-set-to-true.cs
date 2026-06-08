using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Props collection
                    if (shape.Props == null)
                        continue;

                    // Look for the custom property named "IsSpecial"
                    foreach (Prop prop in shape.Props)
                    {
                        // Check property name and its value (true as string)
                        if (prop.NameU == "IsSpecial" && prop.Value != null && prop.Value.Val == "True")
                        {
                            // Add a new field to the shape
                            Field field = new Field();
                            field.Value.Val = "Added"; // example field value
                            shape.Fields.Add(field);
                            break; // field added, move to next shape
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
