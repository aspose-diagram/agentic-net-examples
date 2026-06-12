using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // If the shape has no custom properties, add a default one
                        if (shape.Props.Count == 0)
                        {
                            // Create a new custom property (Prop)
                            Prop defaultProp = new Prop();

                            // Set the property name (identifier)
                            defaultProp.Name = "DefaultProp";

                            // Set the label shown in the ShapeSheet UI
                            defaultProp.Label.Value = "Default Property";

                            // Set the prompt (tooltip) for the property
                            defaultProp.Prompt.Value = "Enter value";

                            // Define the property type as string
                            defaultProp.Type.Value = TypePropValue.String;

                            // Set the default value for the property
                            defaultProp.Value.Val = "DefaultValue";

                            // Add the property to the shape's Props collection
                            shape.Props.Add(defaultProp);
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