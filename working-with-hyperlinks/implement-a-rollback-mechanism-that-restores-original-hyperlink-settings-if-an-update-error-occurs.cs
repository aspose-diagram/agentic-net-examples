using System;
using Aspose.Diagram;

namespace HyperlinkRollbackDemo
{
    // Simple DTO to store hyperlink data for rollback
    public class HyperlinkInfo
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string SubAddress { get; set; }
        public string Description { get; set; }
    }

    public class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Store original hyperlink settings for each shape
                var originalHyperlinks = new System.Collections.Generic.Dictionary<long, System.Collections.Generic.List<HyperlinkInfo>>();

                // Capture current hyperlink data
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            var list = new System.Collections.Generic.List<HyperlinkInfo>();
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                list.Add(new HyperlinkInfo
                                {
                                    Name = link.Name,
                                    Address = link.Address?.Value,
                                    SubAddress = link.SubAddress?.Value,
                                    Description = link.Description?.Value
                                });
                            }
                            originalHyperlinks[shape.ID] = list;
                        }
                    }
                }

                try
                {
                    // Example update: add or modify a hyperlink on each shape
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Create a new hyperlink
                            Hyperlink newLink = new Hyperlink();
                            newLink.Name = "UpdatedLink";
                            newLink.Address.Value = "https://newexample.com";
                            newLink.Description.Value = "Updated hyperlink";

                            // Add the new hyperlink to the shape
                            shape.Hyperlinks.Add(newLink);
                        }
                    }

                    // Save the updated diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during hyperlink update: {ex.Message}");
                    Console.WriteLine("Rolling back to original hyperlink settings...");

                    // Restore original hyperlinks
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Remove all current hyperlinks if any
                            if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                            {
                                shape.Hyperlinks.Clear();
                            }

                            // Re‑add saved hyperlinks
                            if (originalHyperlinks.TryGetValue(shape.ID, out var savedLinks))
                            {
                                foreach (var info in savedLinks)
                                {
                                    Hyperlink link = new Hyperlink();
                                    link.Name = info.Name;
                                    link.Address.Value = info.Address;
                                    link.SubAddress.Value = info.SubAddress;
                                    link.Description.Value = info.Description;
                                    shape.Hyperlinks.Add(link);
                                }
                            }
                        }
                    }

                    // Attempt to save the rolled‑back diagram
                    try
                    {
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                        Console.WriteLine("Rollback successful. Diagram saved with original hyperlinks.");
                    }
                    catch (Exception saveEx)
                    {
                        Console.WriteLine($"Failed to save after rollback: {saveEx.Message}");
                        throw; // Re‑throw as the operation cannot be recovered
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}