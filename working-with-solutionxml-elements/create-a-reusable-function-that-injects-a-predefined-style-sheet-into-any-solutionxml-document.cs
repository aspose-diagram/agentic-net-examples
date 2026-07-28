using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Example XML representing a predefined style sheet
                string styleXml = "<StyleSheet><Name>CustomStyle</Name></StyleSheet>";

                // Inject the style sheet and its XML representation into the diagram
                InjectStyleSheet(diagram, "CustomStyleXML", styleXml);

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Injects a predefined style sheet into the diagram and stores its XML in a SolutionXML element.
        /// </summary>
        /// <param name="diagram">The Aspose.Diagram.Diagram instance to modify.</param>
        /// <param name="solutionXmlName">The name identifier for the SolutionXML entry.</param>
        /// <param name="xmlContent">The XML content representing the style sheet.</param>
        static void InjectStyleSheet(Diagram diagram, string solutionXmlName, string xmlContent)
        {
            // Create a new StyleSheet and assign a name
            StyleSheet styleSheet = new StyleSheet();
            styleSheet.Name = "CustomStyle";

            // Add the style sheet to the diagram's collection
            diagram.StyleSheets.Add(styleSheet);

            // Create a SolutionXML element containing the style sheet XML
            SolutionXML solutionXml = new SolutionXML();
            solutionXml.Name = solutionXmlName;
            solutionXml.XmlValue = xmlContent;

            // Add the SolutionXML element to the diagram
            diagram.SolutionXMLs.Add(solutionXml);
        }
    }