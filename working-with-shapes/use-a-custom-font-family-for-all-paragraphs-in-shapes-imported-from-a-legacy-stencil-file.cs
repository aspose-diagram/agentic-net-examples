using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the legacy stencil file (e.g., .vss or .vssx)
        string stencilPath = "legacyStencil.vssx";

        // Verify that the stencil file exists before proceeding
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"File not found: {stencilPath}");
            return;
        }

        try
        {
            // Load the stencil diagram which contains the masters
            Diagram stencilDiagram = new Diagram(stencilPath);

            // Create a new empty diagram where shapes will be placed
            Diagram diagram = new Diagram();

            // Import all masters from the stencil into the new diagram
            foreach (Master master in stencilDiagram.Masters)
            {
                // Add master by name; this copies the master definition into the target diagram
                diagram.AddMaster(stencilDiagram, master.Name);
            }

            // Example: add one shape for each imported master onto the first page
            Page page = diagram.Pages[0];
            double startX = 2.0;
            double startY = 2.0;
            double offset = 2.0;

            foreach (Master master in diagram.Masters)
            {
                // Place the shape on the page using the master name
                long shapeId = page.AddShape(startX, startY, master.Name);

                // Retrieve the concrete Shape object for further modifications
                Shape shape = page.Shapes.GetShape(shapeId);

                // Apply the custom font to all characters within the shape
                if (shape.Chars != null)
                {
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        // Set the desired font family (must use .Value)
                        ch.FontName.Value = "MyCustomFontFamily";
                    }
                }

                // Move the next shape position to avoid overlap
                startX += offset;
                startY += offset;
            }

            // Save the resulting diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}