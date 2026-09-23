using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input paths
                string diagramPath = "input.vsdx";          // Path to the source Visio diagram
                string pdfFilePath = "newObject.pdf";       // Path to the external PDF to embed
                string outputPath = "output.vsdx";          // Path for the modified diagram

                // Index (or ID) of the OLE shape to replace
                // Replace this value with the actual shape ID of the OLE object
                long oleShapeId = 5; // example ID

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Locate the shape on the first page (adjust page index if needed)
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The diagram contains no pages.");
                }

                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape oleShape = page.Shapes.GetShape(oleShapeId);
                if (oleShape == null)
                {
                    throw new Exception($"No shape found with ID {oleShapeId}.");
                }

                // Verify that the shape is an OLE (foreign) object
                if (oleShape.Type != TypeValue.Foreign)
                {
                    throw new Exception("The specified shape is not a foreign (OLE) object.");
                }

                // Verify that the foreign data represents an embedded object
                if (oleShape.ForeignData == null || oleShape.ForeignData.ForeignType != ForeignType.Object)
                {
                    throw new Exception("The foreign data is missing or does not represent an embedded OLE object.");
                }

                // Read the external PDF file into a byte array
                if (!File.Exists(pdfFilePath))
                {
                    throw new Exception($"PDF file not found: {pdfFilePath}");
                }

                byte[] pdfBytes = File.ReadAllBytes(pdfFilePath);
                if (pdfBytes == null || pdfBytes.Length == 0)
                {
                    throw new Exception("Failed to read PDF file or file is empty.");
                }

                // Replace the OLE object's binary data with the new PDF bytes
                oleShape.ForeignData.ObjectData = pdfBytes;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("OLE object replaced and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }