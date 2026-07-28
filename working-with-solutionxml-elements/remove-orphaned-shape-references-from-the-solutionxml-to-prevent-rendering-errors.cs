using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the cleaned Visio file
            string outputPath = "output_cleaned.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Build a quick lookup of existing shape IDs across all pages
            var existingShapeIds = new System.Collections.Generic.HashSet<long>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    existingShapeIds.Add(shape.ID);
                }
            }

            // Iterate through each SolutionXML element
            for (int i = diagram.SolutionXMLs.Count - 1; i >= 0; i--)
            {
                SolutionXML solXml = diagram.SolutionXMLs[i];
                if (string.IsNullOrWhiteSpace(solXml.XmlValue))
                    continue;

                // Parse the XML content
                XDocument xDoc;
                try
                {
                    xDoc = XDocument.Parse(solXml.XmlValue);
                }
                catch
                {
                    // If the XML is malformed, skip processing this entry
                    continue;
                }

                bool modified = false;

                // Find all elements that have a "ShapeID" attribute
                foreach (var element in xDoc.Descendants())
                {
                    XAttribute attr = element.Attribute("ShapeID");
                    if (attr == null)
                        continue;

                    // Try to parse the ShapeID value
                    if (long.TryParse(attr.Value, out long shapeId))
                    {
                        // If the shape does not exist, remove the whole element
                        if (!existingShapeIds.Contains(shapeId))
                        {
                            element.Remove();
                            modified = true;
                        }
                    }
                }

                // If any orphaned references were removed, update the XML value
                if (modified)
                {
                    solXml.XmlValue = xDoc.ToString();
                }
            }

            // Save the cleaned diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
