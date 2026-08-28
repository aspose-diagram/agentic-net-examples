using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Ensure there is at least one window (required for proper rendering)
                if (diagram.Windows.Count == 0)
                {
                    Window w = new Window();
                    w.WindowType = WindowTypeValue.Drawing;
                    w.WindowState = WindowStateValue.Maximized;
                    w.WindowWidth = 1100;
                    w.WindowHeight = 700;
                    diagram.Windows.Add(w);
                }

                // Add a simple rectangle shape to the active page
                // Parameters: pinX, pinY, master name, page index (0 = first page)
                long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                // Create a new hyperlink
                Hyperlink link = new Hyperlink();
                link.Name = "WebLink";
                link.Address.Value = "https://example.com";

                // Set the hyperlink to open in the same window
                // NewWindow = FALSE means the link will not open a new window
                link.NewWindow.Value = BOOL.False;

                // Optionally clear the Frame property (default is empty string)
                link.Frame.Value = "";

                // Add the hyperlink to the shape's Hyperlinks collection
                shape.Hyperlinks.Add(link);

                // Save the diagram to a VSDX file
                string outputPath = "HyperlinkDemo.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'. Hyperlink set to open in the same window.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }