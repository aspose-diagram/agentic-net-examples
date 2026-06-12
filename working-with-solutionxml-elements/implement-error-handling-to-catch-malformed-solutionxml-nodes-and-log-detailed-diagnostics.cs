using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Determine input and output file paths
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

            // Load the diagram with error handling
            Diagram diagram;
            try
            {
                if (!File.Exists(inputPath))
                {
                    Console.WriteLine($"Error: Input file '{inputPath}' does not exist.");
                    return;
                }

                diagram = new Diagram(inputPath);
                Console.WriteLine($"Diagram loaded successfully from '{inputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Process each SolutionXML element
            ProcessSolutionXml(diagram);

            // Save the diagram (optional, demonstrates a valid Save overload)
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }

        private static void ProcessSolutionXml(Diagram diagram)
        {
            if (diagram.SolutionXMLs == null || diagram.SolutionXMLs.Count == 0)
            {
                Console.WriteLine("No SolutionXML elements found in the diagram.");
                return;
            }

            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                Console.WriteLine($"--- Processing SolutionXML: Name = '{solXml.Name}' ---");
                if (string.IsNullOrWhiteSpace(solXml.XmlValue))
                {
                    Console.WriteLine("Warning: XmlValue is empty or whitespace.");
                    continue;
                }

                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(solXml.XmlValue);
                    Console.WriteLine("XML is well-formed.");
                }
                catch (XmlException xmlEx)
                {
                    Console.WriteLine($"Malformed XML detected in SolutionXML '{solXml.Name}': {xmlEx.Message}");
                    Console.WriteLine($"Location: Line {xmlEx.LineNumber}, Position {xmlEx.LinePosition}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error while processing SolutionXML '{solXml.Name}': {ex.Message}");
                }
            }
        }
    }