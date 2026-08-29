using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (can be passed as command‑line arguments)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Define the default custom property details
                    const string defaultPropName = "DefaultProp";
                    const string defaultPropLabel = "Default Property";
                    const string defaultPropValue = "DefaultValue";

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Ensure the Props collection exists
                            if (shape.Props == null)
                                continue;

                            // Check whether the shape already contains the default property
                            bool hasDefault = false;
                            foreach (Prop existingProp in shape.Props)
                            {
                                if (existingProp.Name == defaultPropName)
                                {
                                    hasDefault = true;
                                    break;
                                }
                            }

                            // If missing, add the default custom property
                            if (!hasDefault)
                            {
                                Prop newProp = new Prop();
                                newProp.Name = defaultPropName;
                                newProp.Label.Value = defaultPropLabel;
                                newProp.Value.Val = defaultPropValue;
                                newProp.Type.Value = TypePropValue.String; // String type

                                shape.Props.Add(newProp);

                                Console.WriteLine($"Added default property to shape ID {shape.ID} on page \"{page.Name}\".");
                            }
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to \"{outputPath}\".");
                }
                catch (Exception ex)
                {
                    // Report any errors
                    Console.WriteLine("An error occurred:");
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }