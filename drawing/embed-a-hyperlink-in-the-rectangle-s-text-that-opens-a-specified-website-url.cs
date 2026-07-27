using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new empty diagram
                using (Diagram diagram = new Diagram())
                {
                    // Get the first (default) page
                    Page page = diagram.Pages[0];

                    // Add a rectangle shape to the page
                    // Parameters: PinX, PinY, Width, Height, MasterName
                    double pinX = 5.0;   // horizontal position (in inches)
                    double pinY = 5.0;   // vertical position (in inches)
                    double width = 2.0;  // rectangle width (in inches)
                    double height = 1.0; // rectangle height (in inches)
                    long shapeId = page.AddShape(pinX, pinY, width, height, "Rectangle");

                    // Retrieve the shape object using its ID
                    Shape rectangle = page.Shapes.GetShape(shapeId);

                    // Clear any existing text and add new text
                    rectangle.Text.Value.Clear();
                    rectangle.Text.Value.Add(new Txt("Visit Example Site"));

                    // Create a hyperlink that points to the desired URL
                    Hyperlink link = new Hyperlink
                    {
                        Name = "ExampleLink",
                        Address = { Value = "https://www.example.com" },
                        Description = { Value = "Open Example Website" }
                    };

                    // Add the hyperlink to the shape's Hyperlinks collection
                    rectangle.Hyperlinks.Add(link);

                    // Save the diagram to a VSDX file
                    string outputPath = "RectangleWithHyperlink.vsdx";
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }