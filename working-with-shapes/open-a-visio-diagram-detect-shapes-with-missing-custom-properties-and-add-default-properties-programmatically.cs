using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the required custom properties and their default values
                var requiredProperties = new (string Name, string Label, string DefaultValue)[]
                {
                    ("Prop1", "Property One", "Default1"),
                    ("Prop2", "Property Two", "Default2")
                };

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the Props collection is available
                        if (shape.Props == null)
                            continue;

                        // Check each required property
                        foreach (var (Name, Label, DefaultValue) in requiredProperties)
                        {
                            // Try to get the property by name
                            Prop existingProp = shape.Props.GetProp(Name);

                            // If the property is missing, add it with default values
                            if (existingProp == null)
                            {
                                Prop newProp = new Prop();
                                newProp.Name = Name;
                                newProp.Label.Value = Label;
                                // Set the property type to String
                                newProp.Type.Value = TypePropValue.String;
                                // Assign the default value
                                newProp.Value.Val = DefaultValue;

                                shape.Props.Add(newProp);
                                Console.WriteLine($"Added missing property '{Name}' to shape ID {shape.ID} on page '{page.Name}'.");
                            }
                        }
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved with updated custom properties.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }