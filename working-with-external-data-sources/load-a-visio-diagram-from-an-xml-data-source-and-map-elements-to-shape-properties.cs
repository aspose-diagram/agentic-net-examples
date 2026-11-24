using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input Visio diagram (XML based format such as .vdx or .vsdx)
        string diagramPath = "inputDiagram.vdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Path to the XML data source that contains shape mappings
        string xmlDataPath = "shapeData.xml";
        if (!System.IO.File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"File not found: {xmlDataPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"File not found: {xmlDataPath}");
            return;
        }

        // Path where the modified diagram will be saved
        string outputPath = "outputDiagram.vsdx";

        try
        {
            // Load the XML mapping file
            XDocument xmlDoc = XDocument.Load(xmlDataPath);

            // Load the Visio diagram inside a using block (Diagram implements IDisposable)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate over each <Shape> element defined in the XML
                foreach (XElement shapeElement in xmlDoc.Root.Elements("Shape"))
                {
                    // Retrieve the shape ID from the XML attribute
                    XAttribute idAttr = shapeElement.Attribute("id");
                    if (idAttr == null)
                        continue; // Skip if no ID attribute

                    if (!long.TryParse(idAttr.Value, out long shapeId))
                        continue; // Skip if ID is not a valid number

                    // Find the shape with the specified ID across all pages
                    Shape targetShape = null;
                    foreach (Page page in diagram.Pages)
                    {
                        Shape found = page.Shapes.GetShape(shapeId);
                        if (found != null)
                        {
                            targetShape = found;
                            break;
                        }
                    }

                    if (targetShape == null)
                        continue; // Shape not found in the diagram

                    // ----- Map text attribute -----
                    XAttribute textAttr = shapeElement.Attribute("text");
                    if (textAttr != null)
                    {
                        // Clear existing text runs and add a new text run
                        targetShape.Text.Value.Clear();
                        targetShape.Text.Value.Add(new Txt(textAttr.Value));
                    }

                    // ----- Map fillColor attribute -----
                    XAttribute fillAttr = shapeElement.Attribute("fillColor");
                    if (fillAttr != null)
                    {
                        // Set the foreground fill color (hex string, e.g., "#FF0000")
                        targetShape.Fill.FillForegnd.Value = fillAttr.Value;
                    }

                    // ----- Map position attributes (pinX, pinY) -----
                    XAttribute pinXAttr = shapeElement.Attribute("pinX");
                    if (pinXAttr != null && double.TryParse(pinXAttr.Value, out double pinX))
                    {
                        targetShape.XForm.PinX.Value = pinX;
                    }

                    XAttribute pinYAttr = shapeElement.Attribute("pinY");
                    if (pinYAttr != null && double.TryParse(pinYAttr.Value, out double pinY))
                    {
                        targetShape.XForm.PinY.Value = pinY;
                    }

                    // ----- Map hidden attribute -----
                    XAttribute hiddenAttr = shapeElement.Attribute("hidden");
                    if (hiddenAttr != null && bool.TryParse(hiddenAttr.Value, out bool isHidden))
                    {
                        if (isHidden)
                        {
                            // Hide the shape by setting its width and height to zero
                            targetShape.XForm.Width.Value = 0.0;
                            targetShape.XForm.Height.Value = 0.0;
                        }
                        else
                        {
                            // Restore to a default size (1 inch) if previously hidden
                            targetShape.XForm.Width.Value = 1.0;
                            targetShape.XForm.Height.Value = 1.0;
                        }
                    }
                }

                // Refresh the diagram to apply any pending changes
                diagram.Refresh();

                // Save the modified diagram to the output file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}