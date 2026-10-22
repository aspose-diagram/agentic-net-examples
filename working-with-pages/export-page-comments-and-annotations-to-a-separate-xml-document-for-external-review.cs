using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Determine output XML file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "comments.xml";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure XML writer settings for pretty output
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineOnAttributes = false
                };

                // Create the XML writer
                using (XmlWriter writer = XmlWriter.Create(outputPath, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Comments"); // Root element

                    // Iterate over each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Use page name if available, otherwise a placeholder
                        string pageName = page.Name ?? "Unnamed";

                        writer.WriteStartElement("Page");
                        writer.WriteAttributeString("Name", pageName);

                        // Iterate over each annotation (comment) on the page
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            writer.WriteStartElement("Annotation");

                            // ShapeID (the shape the comment is attached to, if any)
                            writer.WriteAttributeString("ShapeID", annotation.ShapeID.ToString());

                            // ReviewerID (author identifier)
                            int reviewerId = annotation.ReviewerID?.Value ?? 0;
                            writer.WriteAttributeString("ReviewerID", reviewerId.ToString());

                            // MarkerIndex (unique identifier for the comment)
                            int markerIndex = annotation.MarkerIndex?.Value ?? 0;
                            writer.WriteAttributeString("MarkerIndex", markerIndex.ToString());

                            // Date of the comment, if present
                            if (annotation.Date != null)
                            {
                                DateTime date = annotation.Date.Value;
                                writer.WriteAttributeString("Date", date.ToString("o"));
                            }

                            // Comment text
                            string commentText = annotation.Comment?.Value ?? string.Empty;
                            writer.WriteElementString("Text", commentText);

                            writer.WriteEndElement(); // Annotation
                        }

                        writer.WriteEndElement(); // Page
                    }

                    writer.WriteEndElement(); // Comments
                    writer.WriteEndDocument();
                }

                Console.WriteLine($"Comments exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}