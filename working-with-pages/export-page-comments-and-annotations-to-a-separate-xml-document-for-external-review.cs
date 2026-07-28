using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string visioPath = "input.vsdx";
        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath)) { Console.Error.WriteLine($"File not found: {visioPath}"); return; }

        // Output XML file path
        string xmlOutputPath = "CommentsExport.xml";

        try
        {
            // Load the diagram within a using block to ensure disposal
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Build a map of reviewer indices to reviewer names (Reviewer has no ID property)
                Dictionary<int, string> reviewerMap = new Dictionary<int, string>();
                int reviewerIndex = 0;
                foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
                {
                    // Reviewer.Name is a Str2Value; use .Value to get the string
                    reviewerMap[reviewerIndex] = reviewer.Name.Value;
                    reviewerIndex++;
                }

                // Configure XML writer settings for pretty output
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  "
                };

                // Create the XML writer
                using (XmlWriter writer = XmlWriter.Create(xmlOutputPath, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Comments");

                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Access annotations (comments) on the page
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            writer.WriteStartElement("Comment");

                            // Write page name attribute
                            writer.WriteAttributeString("PageName", page.NameU);

                            // Write associated shape ID (0 if not linked to a shape)
                            writer.WriteAttributeString("ShapeID", annotation.ShapeID.ToString());

                            // Resolve reviewer name using the reviewer index from the annotation
                            string reviewerName = "Unknown";
                            if (reviewerMap.TryGetValue(annotation.ReviewerID.Value, out string name))
                            {
                                reviewerName = name;
                            }
                            writer.WriteAttributeString("Reviewer", reviewerName);

                            // Write comment text
                            writer.WriteAttributeString("Text", annotation.Comment.Value);

                            writer.WriteEndElement(); // </Comment>
                        }
                    }

                    writer.WriteEndElement(); // </Comments>
                    writer.WriteEndDocument();
                }

                Console.WriteLine($"Comments exported to '{xmlOutputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}