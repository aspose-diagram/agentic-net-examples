using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";
                // Path for the modified diagram
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify dynamic connectors (1‑D shapes with master name "Dynamic connector")
                        if (shape.OneD && shape.Master != null && shape.Master.Name == "Dynamic connector")
                        {
                            // Set ConFixedCode to Undefined (default) to enable rerouting
                            shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;

                            // Validate that reroute is enabled (ConFixedCode should be Undefined)
                            if (shape.Layout.ConFixedCode.Value != ConFixedCodeValue.Undefined)
                            {
                                throw new Exception($"Connector ID {shape.ID} does not have reroute enabled.");
                            }
                        }
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Dynamic connectors validated and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }