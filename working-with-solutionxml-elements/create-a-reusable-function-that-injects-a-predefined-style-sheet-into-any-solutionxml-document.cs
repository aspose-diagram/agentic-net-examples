using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    // Predefined style sheet XML content
    private const string PredefinedStyleSheetXml = @"
<StyleSheet>
    <Name>MyPredefinedStyle</Name>
    <FillForegnd>#FFCC00</FillForegnd>
    <LinePattern>1</LinePattern>
    <LineWeight>0.02</LineWeight>
</StyleSheet>";

    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Inject the predefined style sheet into the diagram's SolutionXML collection
            InjectPredefinedStyleSheet(diagram, "MyPredefinedStyle", PredefinedStyleSheetXml);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    /// <summary>
    /// Adds a SolutionXML entry containing a predefined style sheet.
    /// </summary>
    /// <param name="diagram">The diagram to modify.</param>
    /// <param name="name">The identifier name for the SolutionXML element.</param>
    /// <param name="xmlContent">The XML string representing the style sheet.</param>
    private static void InjectPredefinedStyleSheet(Diagram diagram, string name, string xmlContent)
    {
        // Create a new SolutionXML instance
        SolutionXML styleSheetSolution = new SolutionXML();
        styleSheetSolution.Name = name;
        styleSheetSolution.XmlValue = xmlContent.Trim();

        // Add the SolutionXML to the diagram's collection
        diagram.SolutionXMLs.Add(styleSheetSolution);
    }
}
