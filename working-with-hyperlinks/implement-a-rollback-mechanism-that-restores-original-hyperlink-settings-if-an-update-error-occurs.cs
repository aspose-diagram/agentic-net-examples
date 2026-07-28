using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace HyperlinkRollbackExample
{
    // Simple DTO to store hyperlink properties for rollback
    public class HyperlinkInfo
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string SubAddress { get; set; }
        public string Description { get; set; }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Backup original hyperlink settings per shape
                var hyperlinkBackup = new Dictionary<long, List<HyperlinkInfo>>();

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            var list = new List<HyperlinkInfo>();
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                list.Add(new HyperlinkInfo
                                {
                                    Name = link.Name,
                                    Address = link.Address.Value,
                                    SubAddress = link.SubAddress.Value,
                                    Description = link.Description.Value
                                });
                            }
                            hyperlinkBackup[shape.ID] = list;
                        }
                    }
                }

                try
                {
                    // Example update: append a query string to each hyperlink address
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                            {
                                foreach (Hyperlink link in shape.Hyperlinks)
                                {
                                    // Simulate an update that could throw an exception
                                    if (string.IsNullOrWhiteSpace(link.Address.Value))
                                        throw new InvalidOperationException("Invalid hyperlink address.");

                                    link.Address.Value = link.Address.Value + "?updated";
                                }
                            }
                        }
                    }

                    // Save the updated diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine("Diagram saved successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    Console.WriteLine("Rolling back to original hyperlink settings...");

                    // Restore original hyperlink values from backup
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (hyperlinkBackup.TryGetValue(shape.ID, out List<HyperlinkInfo> originalLinks))
                            {
                                // Ensure the shape still has the same number of hyperlinks
                                if (shape.Hyperlinks != null && shape.Hyperlinks.Count == originalLinks.Count)
                                {
                                    for (int i = 0; i < shape.Hyperlinks.Count; i++)
                                    {
                                        Hyperlink link = shape.Hyperlinks[i];
                                        HyperlinkInfo info = originalLinks[i];
                                        link.Name = info.Name;
                                        link.Address.Value = info.Address;
                                        link.SubAddress.Value = info.SubAddress;
                                        link.Description.Value = info.Description;
                                    }
                                }
                            }
                        }
                    }

                    // Save the rolled‑back diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine("Rollback completed and diagram saved.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}