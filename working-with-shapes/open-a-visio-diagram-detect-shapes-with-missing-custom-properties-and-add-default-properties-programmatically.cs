using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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

                // Define required custom properties and their default values
                var requiredProperties = new Dictionary<string, string>
                {
                    { "PropA", "DefaultA" },
                    { "PropB", "DefaultB" },
                    { "PropC", "DefaultC" }
                };

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Props collection is not null
                        if (shape.Props == null)
                            continue;

                        // Track existing property names for quick lookup
                        var existingNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        foreach (Prop existingProp in shape.Props)
                        {
                            if (!string.IsNullOrEmpty(existingProp.Name))
                                existingNames.Add(existingProp.Name);
                        }

                        // Add missing properties with default values
                        foreach (var kvp in requiredProperties)
                        {
                            string propName = kvp.Key;
                            string defaultValue = kvp.Value;

                            if (!existingNames.Contains(propName))
                            {
                                // Create a new custom property (Prop)
                                Prop newProp = new Prop();

                                // Set the property name (identifier)
                                newProp.Name = propName;

                                // Set a user-friendly label (optional)
                                newProp.Label.Value = propName;

                                // Set the default value
                                newProp.Value.Val = defaultValue;

                                // Define the property type as string
                                newProp.Type.Value = TypePropValue.String;

                                // Add the new property to the shape's Props collection
                                shape.Props.Add(newProp);
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram processing completed. Saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }