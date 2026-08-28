using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Name of the custom property to check
                const string targetPropName = "MyFlag";

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        bool addField = false;

                        // Check if the shape has the custom property set to true
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == targetPropName &&
                                prop.Value.Val.Equals("true", StringComparison.OrdinalIgnoreCase))
                            {
                                addField = true;
                                break;
                            }
                        }

                        // If condition met, add a new field to the shape
                        if (addField)
                        {
                            Field field = new Field();
                            // Set the field's value (e.g., a simple text)
                            field.Value.Val = "AddedField";
                            // Add the field to the shape's Fields collection
                            shape.Fields.Add(field);
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