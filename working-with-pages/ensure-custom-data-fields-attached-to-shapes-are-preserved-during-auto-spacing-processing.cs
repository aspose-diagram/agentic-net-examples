using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the name, label and value of the custom property to preserve.
                const string customPropName = "MyCustomProp";
                const string customPropLabel = "My Custom Property";
                const string customPropValue = "TestValue";

                // Iterate through all pages and shapes to add the custom property if it does not exist.
                foreach (Page page in diagram.Pages)
                {
                    // Collect shape IDs first because we may modify the collection while iterating.
                    List<long> shapeIds = new List<long>();
                    foreach (Shape shape in page.Shapes)
                    {
                        shapeIds.Add(shape.ID);
                    }

                    foreach (long shapeId in shapeIds)
                    {
                        Shape shape = page.Shapes.GetShape(shapeId);
                        bool propExists = false;
                        foreach (Prop existingProp in shape.Props)
                        {
                            if (existingProp.Name == customPropName)
                            {
                                propExists = true;
                                break;
                            }
                        }

                        if (!propExists)
                        {
                            // Create and configure a new custom property.
                            Prop prop = new Prop();
                            prop.Name = customPropName;
                            prop.Label.Value = customPropLabel;          // Label uses Str2Value.Value
                            prop.Value.Val = customPropValue;            // Value uses StrValue.Val
                            prop.Type.Value = TypePropValue.String;      // Set data type to string
                            shape.Props.Add(prop);
                        }
                    }
                }

                // Prepare auto‑spacing options using the correct property names.
                AutoSpaceOptions options = new AutoSpaceOptions();
                // Set desired horizontal and vertical spacing (in inches).
                options.DistanceInHorizontal = 0.5; // 0.5 inches between shapes horizontally
                options.DistanceInVertical = 0.5;   // 0.5 inches between shapes vertically

                // Apply auto‑spacing to each page.
                foreach (Page page in diagram.Pages)
                {
                    // AutoSpaceShapes expects a ShapeCollection and AutoSpaceOptions.
                    page.AutoSpaceShapes(page.Shapes, options);
                }

                // Verify that the custom property is still present and unchanged after auto‑spacing.
                bool allPreserved = true;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        bool found = false;
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == customPropName)
                            {
                                found = true;
                                if (prop.Value.Val != customPropValue)
                                {
                                    Console.Error.WriteLine($"Shape ID {shape.ID} on page {page.ID} has altered property value: '{prop.Value.Val}'");
                                    allPreserved = false;
                                }
                                break;
                            }
                        }
                        if (!found)
                        {
                            Console.Error.WriteLine($"Shape ID {shape.ID} on page {page.ID} lost the custom property.");
                            allPreserved = false;
                        }
                    }
                }

                if (allPreserved)
                {
                    Console.WriteLine("All custom properties were preserved after auto‑spacing.");
                }
                else
                {
                    Console.Error.WriteLine("One or more custom properties were not preserved.");
                }

                // Save the modified diagram with the required second argument.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or runtime errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}