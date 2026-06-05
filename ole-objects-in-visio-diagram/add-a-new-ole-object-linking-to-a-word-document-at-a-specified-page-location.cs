using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths for the Word document to embed and the output Visio file
                string wordFilePath = "SampleDocument.docx";
                string outputVisioPath = "DiagramWithOle.vsdx";

                // Desired position and size of the OLE object on the page (in inches)
                double pinX = 2.0;   // X coordinate of the shape's pin (center)
                double pinY = 2.0;   // Y coordinate of the shape's pin (center)
                double width = 3.0;  // Width of the OLE shape
                double height = 2.0; // Height of the OLE shape

                // Create a new empty diagram (contains a default page)
                Diagram diagram = new Diagram();

                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Prepare streams:
                // - An empty image stream is required by the overload (used as a placeholder preview).
                // - The Word document stream provides the OLE binary data.
                using (MemoryStream placeholderImage = new MemoryStream())
                using (FileStream wordStream = new FileStream(wordFilePath, FileMode.Open, FileAccess.Read))
                {
                    // Add the OLE shape to the page.
                    // The overload AddShape(pinX, pinY, width, height, imageStream, objectDataStream)
                    // creates a foreign shape with the supplied OLE data.
                    long shapeId = page.AddShape(pinX, pinY, width, height, placeholderImage, wordStream);

                    // Retrieve the shape instance to adjust additional properties.
                    Shape oleShape = page.Shapes.GetShape(shapeId);

                    // Verify that the shape is a foreign (OLE) shape.
                    if (oleShape != null && oleShape.Type == TypeValue.Foreign && oleShape.ForeignData != null)
                    {
                        // Ensure the foreign data represents an embedded object.
                        if (oleShape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Show the OLE object as an icon (optional).
                            oleShape.ForeignData.ShowAsIcon = BOOL.True;
                        }
                    }
                }

                // Save the diagram in VSDX format.
                diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }