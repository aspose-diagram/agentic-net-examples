using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Ensure the Props collection is available
                        if (shape.Props != null)
                        {
                            // Create a new custom property (Prop) for the timestamp
                            Prop timestampProp = new Prop();
                            timestampProp.Name = "Timestamp";
                            timestampProp.Value.Val = DateTime.Now.ToString("o"); // ISO 8601 format

                            // Add the property to the shape
                            shape.Props.Add(timestampProp);
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