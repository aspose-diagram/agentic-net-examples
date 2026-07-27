using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new empty diagram (contains a default page)
                Diagram diagram = new Diagram();

                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Add a pentagon shape at position (5,5) inches
                // The AddShape method returns a long shape ID
                long shapeId = page.AddShape(5.0, 5.0, "Pentagon");

                // Retrieve the shape object using the ID (cast to int as required)
                Shape pentagon = page.Shapes.GetShape((int)shapeId);

                // Store original dimensions
                double originalWidth = pentagon.XForm.Width.Value;
                double originalHeight = pentagon.XForm.Height.Value;

                // Scale uniformly to double the size
                pentagon.XForm.Width.Value = originalWidth * 2.0;
                pentagon.XForm.Height.Value = originalHeight * 2.0;

                // Save the diagram to a VSDX file
                diagram.Save("ScaledPentagon.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }