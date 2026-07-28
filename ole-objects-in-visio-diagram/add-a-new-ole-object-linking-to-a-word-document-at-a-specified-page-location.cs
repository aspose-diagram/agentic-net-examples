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

                // Paths for the source Visio diagram, the Word document to embed, and the output diagram.
                string visioPath = "input.vsdx";
                string wordPath = "sample.docx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram.
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Access the first page (you can choose any page as needed).
                    Page page = diagram.Pages[0];

                    // Add a rectangle shape that will host the OLE object.
                    // Parameters: PinX, PinY, Width, Height (all in inches).
                    long oleShapeId = page.DrawRectangle(5.0, 5.0, 2.0, 2.0);

                    // Retrieve the shape instance using the returned ID.
                    Shape oleShape = page.Shapes.GetShape(oleShapeId);

                    // Mark the shape as an OLE (Foreign) object.
                    oleShape.Type = TypeValue.Foreign;

                    // Set the source file name (used to identify the OLE type).
                    oleShape.ForeignData.ObjectSourceFullName = Path.GetFileName(wordPath);

                    // Load the Word document bytes and assign them to the OLE data.
                    byte[] oleData = File.ReadAllBytes(wordPath);
                    oleShape.ForeignData.ObjectData = oleData;

                    // Optionally display the OLE object as an icon.
                    oleShape.ForeignData.ShowAsIcon = BOOL.True;

                    // Save the modified diagram to a new file.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("OLE object linked and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }