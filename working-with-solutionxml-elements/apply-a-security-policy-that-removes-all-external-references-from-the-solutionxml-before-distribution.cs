using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all SolutionXML elements and remove external references
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                if (solXml == null || string.IsNullOrEmpty(solXml.XmlValue))
                    continue;

                string cleanedXml = solXml.XmlValue;

                // Remove HTTP/HTTPS URLs
                cleanedXml = Regex.Replace(
                    cleanedXml,
                    @"\bhttps?://\S+",
                    string.Empty,
                    RegexOptions.IgnoreCase);

                // Remove file:// URLs
                cleanedXml = Regex.Replace(
                    cleanedXml,
                    @"\bfile://\S+",
                    string.Empty,
                    RegexOptions.IgnoreCase);

                // Optionally remove Windows absolute paths (e.g., C:\folder\file)
                cleanedXml = Regex.Replace(
                    cleanedXml,
                    @"[A-Za-z]:\\[^\s""'>]*",
                    string.Empty,
                    RegexOptions.IgnoreCase);

                // Update the SolutionXML with the cleaned content
                solXml.XmlValue = cleanedXml;
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}