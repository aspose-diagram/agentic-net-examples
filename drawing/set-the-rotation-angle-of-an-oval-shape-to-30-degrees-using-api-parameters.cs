using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Find the first oval shape on the page.
                // Oval shapes typically have a master named "Oval".
                Shape? ovalShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Oval")
                    {
                        ovalShape = shape;
                        break;
                    }
                }

                if (ovalShape == null)
                {
                    throw new Exception("No oval shape found on the first page.");
                }

                // Set the rotation angle of the oval shape to 30 degrees.
                // According to Aspose.Diagram API, the Angle cell expects the angle value directly.
                ovalShape.XForm.Angle.Value = 30.0;

                // Save the modified diagram to a new file.
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }