using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputXmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Define a temporary VDX file path.
        string tempVdxPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".vdx");

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Save the diagram to VDX (XML) format.
                diagram.Save(tempVdxPath, SaveFileFormat.Vdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram loading or saving: {ex.Message}");
            return;
        }

        try
        {
            // Load the generated VDX XML.
            XDocument doc = XDocument.Load(tempVdxPath);

            // Define custom namespace prefix and URI.
            const string prefix = "custom";
            const string nsUri = "http://example.com/custom";
            XNamespace customNs = nsUri;

            // Ensure the root element declares the custom namespace.
            XElement root = doc.Root;
            if (root != null)
            {
                XAttribute xmlnsAttr = root.Attribute(XNamespace.Xmlns + prefix);
                if (xmlnsAttr == null)
                {
                    root.SetAttributeValue(XNamespace.Xmlns + prefix, nsUri);
                }
            }

            // Apply the custom namespace to all User-defined cell elements.
            foreach (XElement userElem in doc.Descendants())
            {
                if (userElem.Name.LocalName.Equals("User", StringComparison.OrdinalIgnoreCase))
                {
                    userElem.Name = customNs + "User";
                }
            }

            // Save the modified XML to the desired output path.
            doc.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during XML processing: {ex.Message}");
            return;
        }
        finally
        {
            // Clean up the temporary VDX file.
            try
            {
                if (File.Exists(tempVdxPath))
                {
                    File.Delete(tempVdxPath);
                }
            }
            catch
            {
                // Suppress any cleanup errors.
            }
        }

        Console.WriteLine($"Diagram exported with custom namespace to: {outputPath}");
    }
}