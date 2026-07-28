using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path (saved unchanged after audit)
                string outputPath = "output_audit.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Retrieve the source file name of the embedded OLE object
                            string sourceFileName = shape.ForeignData.ObjectSourceFullName ?? "N/A";

                            // Output audit information
                            Console.WriteLine($"Shape ID: {shape.ID}");
                            Console.WriteLine($"  OLE Source File: {sourceFileName}");

                            // Note: Creation date of the OLE object is not directly exposed via Aspose.Diagram.
                            // If needed, further processing of shape.ForeignData.ObjectData would be required
                            // using the appropriate Aspose product (e.g., Aspose.Words, Aspose.Cells) to extract
                            // metadata from the embedded file.
                        }
                    }
                }

                // Save the diagram (unchanged) to preserve any modifications if added later
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }