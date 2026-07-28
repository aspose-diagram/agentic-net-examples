using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (lifecycle: load)
            var diagram = new Diagram("input.vsdx");

            // Prepare save options for VDX (XML) format
            var saveOptions = new DiagramSaveOptions
            {
                SaveFormat = SaveFileFormat.Vdx // XML based Visio format
            };

            // Save the diagram to a memory stream (lifecycle: save)
            using (var ms = new MemoryStream())
            {
                diagram.Save(ms, saveOptions);
                ms.Position = 0; // Reset stream position for reading

                // Load the saved XML into XDocument for manipulation
                var xDoc = XDocument.Load(ms);

                // Define the custom namespace prefix you want to apply
                const string customPrefix = "MyPrefix";

                // Find all <Cell> elements whose Name attribute starts with "User."
                var userCells = xDoc.Descendants()
                                    .Where(e => e.Name.LocalName == "Cell")
                                    .Select(e => e.Attribute("N"))
                                    .Where(attr => attr != null && attr.Value.StartsWith("User.", StringComparison.Ordinal));

                // Replace the "User." prefix with the custom namespace prefix
                foreach (var nameAttr in userCells)
                {
                    var original = nameAttr.Value; // e.g., "User.MyCell"
                    var newName = customPrefix + "." + original.Substring("User.".Length);
                    nameAttr.Value = newName;
                }

                // Save the modified XML to the final file
                xDoc.Save("output.vdx");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
