using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through pages to find a dynamic connector shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify dynamic connector by its master name
                        if (shape.Master != null && shape.Master.Name == "Dynamic connector")
                        {
                            // Enable reroute by setting ConFixedCode to Undefined (default allows rerouting)
                            shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;

                            // Optionally, you can break after the first connector is processed
                            // break;
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