using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Name of the custom property (field) to remove from shapes
                string targetFieldName = "MyCustomProp";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Search for the property with the specified name
                        Prop propToRemove = null;
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == targetFieldName)
                            {
                                propToRemove = prop;
                                break;
                            }
                        }

                        // If found, remove it from the shape's Props collection
                        if (propToRemove != null)
                        {
                            shape.Props.Remove(propToRemove);
                            Console.WriteLine($"Removed property '{targetFieldName}' from shape ID {shape.ID} on page ID {page.ID}.");
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved to " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }