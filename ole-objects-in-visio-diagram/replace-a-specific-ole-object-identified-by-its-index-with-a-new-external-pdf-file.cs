using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file, external PDF file, and output Visio file paths.
                string visioInputPath = "input.vsdx";
                string pdfReplacementPath = "newDocument.pdf";
                string visioOutputPath = "output.vsdx";

                // Index of the shape (0‑based) that contains the OLE object to replace.
                int oleShapeIndex = 2; // Adjust as needed.

                // Load the Visio diagram.
                Diagram diagram = new Diagram(visioInputPath);

                // Assume the OLE object is on the first page.
                Page page = diagram.Pages[0];

                // Locate the shape by its index in the Shapes collection.
                Shape oleShape = null;
                int currentIndex = 0;
                foreach (Shape shape in page.Shapes)
                {
                    if (currentIndex == oleShapeIndex)
                    {
                        oleShape = shape;
                        break;
                    }
                    currentIndex++;
                }

                if (oleShape == null)
                    throw new Exception($"No shape found at index {oleShapeIndex}.");

                // Verify that the shape is an OLE (Foreign) object.
                if (oleShape.Type != TypeValue.Foreign)
                    throw new Exception("The selected shape is not a Foreign (OLE) object.");

                if (oleShape.ForeignData == null)
                    throw new Exception("The selected shape does not contain ForeignData.");

                if (oleShape.ForeignData.ForeignType != ForeignType.Object)
                    throw new Exception("The ForeignData does not represent an embedded OLE object.");

                // Read the external PDF file into a byte array.
                if (!File.Exists(pdfReplacementPath))
                    throw new FileNotFoundException("PDF file not found.", pdfReplacementPath);

                byte[] pdfBytes = File.ReadAllBytes(pdfReplacementPath);

                // Replace the embedded OLE data with the new PDF bytes.
                oleShape.ForeignData.ObjectData = pdfBytes;

                // Optionally update the source name (e.g., file name) for reference.
                oleShape.ForeignData.ObjectSourceFullName = Path.GetFileName(pdfReplacementPath);

                // Save the modified diagram.
                diagram.Save(visioOutputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("OLE object replaced and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }