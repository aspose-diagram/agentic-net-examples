using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to find a dynamic connector
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify dynamic connectors: 1‑D shape with master name "Dynamic connector"
                        if (shape.OneD && shape.Master != null && shape.Master.Name == "Dynamic connector")
                        {
                            // Enable reroute by setting ConFixedCode to Undefined (default routing behavior)
                            shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;

                            // Optionally, you can trigger a layout pass to apply routing changes
                            // page.Layout(new LayoutOptions());

                            Console.WriteLine($"Connector ID {shape.ID} reroute option enabled.");
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved to " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }