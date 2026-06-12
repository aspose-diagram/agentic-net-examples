using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            string filePath = "input.vsdx"; // replace with your diagram path
            Diagram diagram = new Diagram(filePath);

            // Locate a connector by its unique ID
            long connectorId = 5; // replace with the actual connector ID
            Shape connectorById = FindConnectorById(diagram, connectorId);
            if (connectorById != null)
            {
                Console.WriteLine($"Connector found by ID {connectorId}: NameU = {connectorById.NameU}");
            }
            else
            {
                Console.WriteLine($"Connector with ID {connectorId} not found.");
            }

            // Locate a connector by its universal name (NameU)
            string connectorNameU = "Dynamic connector"; // replace with the actual name
            Shape connectorByName = FindConnectorByName(diagram, connectorNameU);
            if (connectorByName != null)
            {
                Console.WriteLine($"Connector found by NameU \"{connectorNameU}\": ID = {connectorByName.ID}");
            }
            else
            {
                Console.WriteLine($"Connector with NameU \"{connectorNameU}\" not found.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Searches all pages for a connector shape with the specified ID
    static Shape FindConnectorById(Diagram diagram, long id)
    {
        foreach (Page page in diagram.Pages)
        {
            try
            {
                Shape shape = page.Shapes.GetShape(id);
                if (shape != null && shape.OneD) // OneD indicates a connector
                {
                    return shape;
                }
            }
            catch
            {
                // Shape with this ID is not on the current page; continue searching
            }
        }
        return null;
    }

    // Searches all pages for a connector shape with the specified universal name (NameU)
    static Shape FindConnectorByName(Diagram diagram, string nameU)
    {
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.OneD && string.Equals(shape.NameU, nameU, StringComparison.OrdinalIgnoreCase))
                {
                    return shape;
                }
            }
        }
        return null;
    }
}
