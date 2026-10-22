using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect: input diagram path, output diagram path, new page width (inches), new page height (inches)
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <newWidthInches> <newHeightInches>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string widthArg = args[2];
        string heightArg = args[3];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse width and height values
        if (!double.TryParse(widthArg, out double newWidth) || !double.TryParse(heightArg, out double newHeight))
        {
            Console.Error.WriteLine("Invalid width or height values. Provide numeric values.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Update each page's canvas size
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newHeight;
                }

                // Iterate through SolutionXML elements and modify stored page dimensions
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    if (string.IsNullOrEmpty(solXml.XmlValue))
                        continue;

                    // Load the XML content of the SolutionXML element
                    XmlDocument xmlDoc = new XmlDocument();
                    try
                    {
                        xmlDoc.LoadXml(solXml.XmlValue);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to parse SolutionXML '{solXml.Name}': {ex.Message}");
                        continue;
                    }

                    // Update <PageWidth> node if present
                    XmlNode widthNode = xmlDoc.SelectSingleNode("//PageWidth");
                    if (widthNode != null)
                        widthNode.InnerText = newWidth.ToString();

                    // Update <PageHeight> node if present
                    XmlNode heightNode = xmlDoc.SelectSingleNode("//PageHeight");
                    if (heightNode != null)
                        heightNode.InnerText = newHeight.ToString();

                    // Write the modified XML back to the SolutionXML element
                    using (StringWriter sw = new StringWriter())
                    {
                        xmlDoc.Save(sw);
                        solXml.XmlValue = sw.ToString();
                    }
                }

                // Save the modified diagram back to a Visio format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}