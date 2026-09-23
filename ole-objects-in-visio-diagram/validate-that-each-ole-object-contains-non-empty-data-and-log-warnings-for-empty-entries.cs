using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape is a foreign (OLE) shape
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null)
                        {
                            // Verify the foreign type is an embedded object
                            if (shape.ForeignData.ForeignType == ForeignType.Object)
                            {
                                byte[] oleData = shape.ForeignData.ObjectData;

                                // Validate that the OLE object contains data
                                if (oleData == null || oleData.Length == 0)
                                {
                                    Console.WriteLine($"Warning: OLE object in shape ID {shape.ID} (NameU: {shape.NameU}) has empty data.");
                                }
                            }
                        }
                    }
                }

                // Save the diagram (unchanged) to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }