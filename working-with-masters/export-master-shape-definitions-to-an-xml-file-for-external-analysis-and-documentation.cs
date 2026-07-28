using System.IO;
using System;
using Aspose.Diagram;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Get input Visio file path
        Console.WriteLine("Enter the path to the Visio file:");
        string inputPath = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputPath))
        {
            Console.WriteLine("Invalid input path.");
            return;
        }

        // Get output XML file path
        Console.WriteLine("Enter the desired output XML file path:");
        string outputPath = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            Console.WriteLine("Invalid output path.");
            return;
        }

        // Load the diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Build XML representation of masters
        XElement mastersElement = new XElement("Masters");
        foreach (Master master in diagram.Masters)
        {
            XElement masterElement = new XElement("Master",
                new XAttribute("ID", master.ID),
                new XAttribute("Name", master.Name ?? string.Empty),
                new XAttribute("NameU", master.NameU ?? string.Empty),
                new XAttribute("UniqueID", master.UniqueID.ToString()),
                new XAttribute("Hidden", master.Hidden == BOOL.True ? "True" : "False")
            );

            mastersElement.Add(masterElement);
        }

        XDocument xmlDoc = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            mastersElement
        );

        // Save XML to file
        try
        {
            xmlDoc.Save(outputPath);
            Console.WriteLine($"Master definitions exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving XML: {ex.Message}");
        }
    }
}
