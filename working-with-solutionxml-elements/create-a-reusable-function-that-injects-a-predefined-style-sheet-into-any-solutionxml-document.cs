using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Predefined style sheet XML to be injected
            string styleSheetXml = @"<StyleSheet name=""MyCustomStyle"">
            <FillForegnd>#FFCC00</FillForegnd>
            <LinePattern>1</LinePattern>
            <LineWeight>0.02</LineWeight>
            </StyleSheet>";

            // Inject the style sheet into the diagram's SolutionXML collection
            InjectStyleSheet(diagram, "CustomStyleSheet", styleSheetXml);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    /// <summary>
    /// Adds a SolutionXML element containing a style sheet definition to the specified diagram.
    /// </summary>
    /// <param name="diagram">The Aspose.Diagram.Diagram instance to modify.</param>
    /// <param name="name">A unique name for the SolutionXML entry.</param>
    /// <param name="xmlContent">The XML string representing the style sheet.</param>
    public static void InjectStyleSheet(Diagram diagram, string name, string xmlContent)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        if (string.IsNullOrEmpty(xmlContent)) throw new ArgumentException("XML content cannot be null or empty.", nameof(xmlContent));

        // Create a new SolutionXML object and set its properties
        SolutionXML solutionXml = new SolutionXML
        {
            Name = name,
            XmlValue = xmlContent
        };

        // Add the SolutionXML to the diagram's collection
        diagram.SolutionXMLs.Add(solutionXml);
    }
}
