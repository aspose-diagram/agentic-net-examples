using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with your actual file path)
            Diagram diagram = new Diagram(@"C:\Diagrams\input.vsdx");

            // Export the diagram to VDX (XML) into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                diagram.Save(ms, SaveFileFormat.Vdx);
                ms.Position = 0; // Reset stream position for reading

                // Load the XML from the memory stream
                XDocument xmlDoc = XDocument.Load(ms);

                // Define the custom namespace prefix to apply
                const string customPrefix = "custom:";

                // Find all <Cell> elements whose N attribute starts with "User."
                var userCells = xmlDoc.Descendants("Cell")
                                      .Where(c => (string)c.Attribute("N") != null &&
                                                  ((string)c.Attribute("N")).StartsWith("User.", StringComparison.Ordinal));

                // Apply the custom prefix to each matching cell's N attribute
                foreach (var cell in userCells)
                {
                    string originalName = (string)cell.Attribute("N");
                    cell.SetAttributeValue("N", customPrefix + originalName);
                }

                // Save the modified XML to the desired output file
                string outputPath = @"C:\Diagrams\output_custom_namespace.vdx";
                xmlDoc.Save(outputPath);
            }

            Console.WriteLine("Diagram exported with custom namespace prefix applied to user-defined cells.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
