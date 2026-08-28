using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the source Visio file
        string visioPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Load the diagram inside a try/catch to capture Aspose errors
        Diagram diagram;
        try
        {
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Prepare an XML document to hold master definitions
        XmlDocument xmlDoc = new XmlDocument();

        // Create XML declaration
        XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        xmlDoc.AppendChild(xmlDecl);

        // Root element <Masters>
        XmlElement root = xmlDoc.CreateElement("Masters");
        xmlDoc.AppendChild(root);

        // Iterate through all masters in the diagram
        try
        {
            foreach (Master master in diagram.Masters)
            {
                // <Master> element
                XmlElement masterElem = xmlDoc.CreateElement("Master");
                root.AppendChild(masterElem);

                // Basic attributes
                masterElem.SetAttribute("ID", master.ID.ToString());
                masterElem.SetAttribute("Name", master.Name ?? string.Empty);
                masterElem.SetAttribute("NameU", master.NameU ?? string.Empty);
                masterElem.SetAttribute("Hidden", master.Hidden == BOOL.True ? "True" : "False");
                masterElem.SetAttribute("MatchByName", master.MatchByName == BOOL.True ? "True" : "False");

                // Optional GUIDs (convert to string)
                if (master.UniqueID != Guid.Empty)
                    masterElem.SetAttribute("UniqueID", master.UniqueID.ToString());
                if (master.BaseID != Guid.Empty)
                    masterElem.SetAttribute("BaseID", master.BaseID.ToString());

                // Icon size (enum value as string)
                masterElem.SetAttribute("IconSize", master.IconSize.ToString());

                // Count of shapes contained in the master
                int shapeCount = master.Shapes.Count;
                masterElem.SetAttribute("ShapeCount", shapeCount.ToString());

                // List each shape's ID and Name within the master
                if (shapeCount > 0)
                {
                    XmlElement shapesElem = xmlDoc.CreateElement("Shapes");
                    masterElem.AppendChild(shapesElem);

                    foreach (Shape shape in master.Shapes)
                    {
                        XmlElement shapeElem = xmlDoc.CreateElement("Shape");
                        shapesElem.AppendChild(shapeElem);

                        shapeElem.SetAttribute("ID", shape.ID.ToString());
                        shapeElem.SetAttribute("Name", shape.Name ?? string.Empty);
                        shapeElem.SetAttribute("NameU", shape.NameU ?? string.Empty);
                        // Type is a non-nullable enum; use ToString directly
                        shapeElem.SetAttribute("Type", shape.Type.ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing masters: {ex.Message}");
            return;
        }

        // Output XML file path
        string outputPath = "masters.xml";

        // Ensure the directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Save the XML document
        try
        {
            xmlDoc.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving XML: {ex.Message}");
            return;
        }

        Console.WriteLine($"Master definitions exported to: {outputPath}");
    }
}