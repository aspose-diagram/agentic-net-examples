using System;
using System.IO;
using System.Xml;
using System.Globalization;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <pageWidthInInches> <pageHeightInInches>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string widthArg = args[2];
        string heightArg = args[3];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse width and height values.
        if (!double.TryParse(widthArg, NumberStyles.Float, CultureInfo.InvariantCulture, out double newWidth) ||
            !double.TryParse(heightArg, NumberStyles.Float, CultureInfo.InvariantCulture, out double newHeight))
        {
            Console.Error.WriteLine("Invalid page width or height. Provide numeric values (in inches).");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool anySolutionXmlUpdated = false;

                // Iterate over all SolutionXML elements in the document.
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    if (string.IsNullOrEmpty(solXml.XmlValue))
                        continue; // Skip empty entries.

                    // Load the XML content into an XmlDocument for manipulation.
                    XmlDocument xmlDoc = new XmlDocument();
                    try
                    {
                        xmlDoc.LoadXml(solXml.XmlValue);
                    }
                    catch (XmlException)
                    {
                        // If the XML is malformed, skip this element.
                        continue;
                    }

                    // Attempt to locate <PageWidth> and <PageHeight> nodes.
                    XmlNode widthNode = xmlDoc.SelectSingleNode("//PageWidth");
                    XmlNode heightNode = xmlDoc.SelectSingleNode("//PageHeight");

                    // Update nodes if they exist.
                    if (widthNode != null)
                    {
                        widthNode.InnerText = newWidth.ToString(CultureInfo.InvariantCulture);
                        anySolutionXmlUpdated = true;
                    }

                    if (heightNode != null)
                    {
                        heightNode.InnerText = newHeight.ToString(CultureInfo.InvariantCulture);
                        anySolutionXmlUpdated = true;
                    }

                    // If any changes were made, write the modified XML back to the SolutionXML element.
                    if (anySolutionXmlUpdated)
                    {
                        solXml.XmlValue = xmlDoc.OuterXml;
                    }
                }

                // Save the modified diagram only if we updated at least one SolutionXML entry.
                if (anySolutionXmlUpdated)
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved with updated page dimensions to: {outputPath}");
                }
                else
                {
                    Console.WriteLine("No SolutionXML elements containing page dimensions were found. Diagram not modified.");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO related exceptions.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}