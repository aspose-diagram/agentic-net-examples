using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define oval geometry (center at (5,5), width 2 inches, height 1 inch)
            double pinX = 5.0;
            double pinY = 5.0;
            double width = 2.0;
            double height = 1.0;

            // Draw the oval; returns the shape ID (long)
            long ovalId = page.DrawEllipse(pinX, pinY, width, height);

            // Retrieve the Shape object using the ID
            Shape oval = page.Shapes.GetShape((int)ovalId);

            // Set line cap style to round (smooth line ends)
            // BOOL.True creates rounded caps; BOOL.False would be square caps
            oval.Line.LineCap.Value = BOOL.True;

            // Save the diagram to a VSDX file
            diagram.Save("OvalRoundedLineCap.vsdx", SaveFileFormat.Vsdx);
        }
    }