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

                // Path to the Word document to be linked/embedded
                string wordFilePath = @"C:\Docs\SampleDocument.docx";

                // Verify the Word file exists
                if (!File.Exists(wordFilePath))
                    throw new FileNotFoundException("Word document not found.", wordFilePath);

                // Create a new empty Visio diagram
                Diagram diagram = new Diagram();

                // Add a new blank page to the diagram
                diagram.Pages.Add(new Page());
                Page page = diagram.Pages[0];

                // Define position and size for the OLE placeholder shape (in inches)
                double pinX = 5.0;   // horizontal center
                double pinY = 5.0;   // vertical center
                double width = 4.0;  // shape width
                double height = 3.0; // shape height

                // Draw a rectangle that will host the OLE object
                long shapeId = page.DrawRectangle(pinX, pinY, width, height);
                Shape oleShape = page.Shapes.GetShape(shapeId);

                // Mark the shape as a foreign (OLE) shape
                oleShape.Type = TypeValue.Foreign;

                // Ensure the foreign data is treated as an embedded object
                oleShape.ForeignData.ForeignType = ForeignType.Object;

                // Load the Word document binary data
                byte[] wordData = File.ReadAllBytes(wordFilePath);

                // Assign the binary data to the shape's OLE object data
                oleShape.ForeignData.ObjectData = wordData;

                // Optionally display the OLE object as an icon
                oleShape.ForeignData.ShowAsIcon = BOOL.True;

                // Save the diagram to VSDX format
                string outputPath = @"C:\Output\DiagramWithOle.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }