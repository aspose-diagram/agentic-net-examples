using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace HyperlinkRollbackDemo
{
    // Simple DTO to store hyperlink properties
    public class HyperlinkData
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string SubAddress { get; set; }
        public string Description { get; set; }
    }

    // Holds backup info for a shape on a specific page
    public class ShapeLinkInfo
    {
        public int PageIndex { get; set; }
        public long ShapeId { get; set; }
        public List<HyperlinkData> Links { get; set; } = new();
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Backup original hyperlink settings
                List<ShapeLinkInfo> backup = new List<ShapeLinkInfo>();

                for (int p = 0; p < diagram.Pages.Count; p++)
                {
                    Page page = diagram.Pages[p];
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            var linkList = new List<HyperlinkData>();
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                linkList.Add(new HyperlinkData
                                {
                                    Name = link.Name,
                                    Address = link.Address.Value,
                                    SubAddress = link.SubAddress.Value,
                                    Description = link.Description.Value
                                });
                            }

                            backup.Add(new ShapeLinkInfo
                            {
                                PageIndex = p,
                                ShapeId = shape.ID,
                                Links = linkList
                            });
                        }
                    }
                }

                try
                {
                    // Example update: change every hyperlink address to a new URL
                    for (int p = 0; p < diagram.Pages.Count; p++)
                    {
                        Page page = diagram.Pages[p];
                        foreach (Shape shape in page.Shapes)
                        {
                            if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                            {
                                foreach (Hyperlink link in shape.Hyperlinks)
                                {
                                    // Simulate an update that could throw an exception
                                    link.Address.Value = "https://newexample.com";
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
                    Console.WriteLine($"Error during update: {ex.Message}");
                    Console.WriteLine("Restoring original hyperlink settings...");

                    // Rollback: restore each shape's hyperlinks from backup
                    foreach (ShapeLinkInfo info in backup)
                    {
                        Page page = diagram.Pages[info.PageIndex];
                        Shape shape = page.Shapes.GetShape(info.ShapeId);
                        if (shape == null || shape.Hyperlinks == null)
                            continue;

                        int existingCount = shape.Hyperlinks.Count;
                        int originalCount = info.Links.Count;
                        int minCount = Math.Min(existingCount, originalCount);

                        // Restore values for existing hyperlinks
                        for (int i = 0; i < minCount; i++)
                        {
                            Hyperlink link = shape.Hyperlinks[i];
                            HyperlinkData data = info.Links[i];
                            link.Name = data.Name;
                            link.Address.Value = data.Address;
                            link.SubAddress.Value = data.SubAddress;
                            link.Description.Value = data.Description;
                        }

                        // Add missing original hyperlinks if any
                        for (int i = existingCount; i < originalCount; i++)
                        {
                            HyperlinkData data = info.Links[i];
                            Hyperlink newLink = new Hyperlink
                            {
                                Name = data.Name
                            };
                            newLink.Address.Value = data.Address;
                            newLink.SubAddress.Value = data.SubAddress;
                            newLink.Description.Value = data.Description;
                            shape.Hyperlinks.Add(newLink);
                        }
                    }

                    // Attempt to save the rolled-back diagram
                    try
                    {
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                        Console.WriteLine("Diagram saved after rollback.");
                    }
                    catch (Exception saveEx)
                    {
                        Console.WriteLine($"Failed to save after rollback: {saveEx.Message}");
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