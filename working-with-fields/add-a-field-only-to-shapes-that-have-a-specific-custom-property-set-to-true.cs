using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (use defaults if not provided)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Name of the custom property that must be true
                const string targetPropName = "MyFlag";

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        bool hasTrueFlag = false;

                        // Check the shape's custom properties (Props collection)
                        if (shape.Props != null)
                        {
                            foreach (Prop prop in shape.Props)
                            {
                                if (prop.Name == targetPropName &&
                                    prop.Value != null &&
                                    prop.Value.Val != null &&
                                    prop.Value.Val.Equals("true", StringComparison.OrdinalIgnoreCase))
                                {
                                    hasTrueFlag = true;
                                    break;
                                }
                            }
                        }

                        // If the custom property is set to true, add a new field to the shape
                        if (hasTrueFlag)
                        {
                            Field newField = new Field();
                            // Set the field's displayed value
                            newField.Value.Val = "Added";
                            shape.Fields.Add(newField);
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }