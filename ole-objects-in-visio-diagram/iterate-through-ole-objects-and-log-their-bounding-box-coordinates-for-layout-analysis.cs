using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string filePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Identify OLE objects:
                        // - Shape type must be Foreign
                        // - ForeignData must exist and contain ObjectData
                        // - ObjectType must indicate an embedded OLE object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ObjectData != null &&
                            shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                        {
                            // Retrieve bounding box data (PinX/PinY are the shape's center)
                            double pinX = shape.XForm.PinX.Value;
                            double pinY = shape.XForm.PinY.Value;
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;

                            // Calculate corner coordinates
                            double left = pinX - width / 2;
                            double right = pinX + width / 2;
                            double top = pinY + height / 2;
                            double bottom = pinY - height / 2;

                            // Log the information
                            Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}");
                            Console.WriteLine($"  Center: ({pinX}, {pinY})");
                            Console.WriteLine($"  Size: Width={width}, Height={height}");
                            Console.WriteLine($"  Bounding Box: Left={left}, Right={right}, Top={top}, Bottom={bottom}");
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }