using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output CSV file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramCsvExport <inputVisioPath> <outputCsvPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to add a custom property
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Create a new custom property (Prop) if it does not already exist
                        bool propExists = false;
                        foreach (Prop existingProp in shape.Props)
                        {
                            if (existingProp.Name == "MyCustomProperty")
                            {
                                propExists = true;
                                break;
                            }
                        }

                        if (!propExists)
                        {
                            Prop customProp = new Prop();
                            customProp.Name = "MyCustomProperty";
                            customProp.Label.Value = "My Custom Property";
                            customProp.Value.Val = "DefaultValue";
                            customProp.Type.Value = TypePropValue.String;
                            shape.Props.Add(customProp);
                        }
                    }
                }

                // Export the diagram to CSV; custom properties become additional columns
                diagram.Save(outputPath, SaveFileFormat.Csv);
                Console.WriteLine($"Diagram exported successfully to CSV at: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }