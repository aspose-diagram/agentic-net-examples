using System;
using System.IO;
using Aspose.Diagram;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths from command‑line arguments or use defaults.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "masters.xml";

        // Guard: ensure the input Visio file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create the root XML element.
            XElement root = new XElement("Masters");

            // Iterate over each master in the diagram.
            foreach (Master master in diagram.Masters)
            {
                // Build an XML element for the current master.
                XElement masterElement = new XElement("Master",
                    new XElement("ID", master.ID),
                    new XElement("Name", master.Name),
                    new XElement("UniqueID", master.UniqueID.ToString())
                );

                // Add the master element to the root.
                root.Add(masterElement);
            }

            // Create the XDocument and save it to the specified output path.
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            doc.Save(outputPath);

            Console.WriteLine($"Master definitions exported successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}