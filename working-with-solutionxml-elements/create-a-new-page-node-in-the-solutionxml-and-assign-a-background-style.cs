using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                using (Diagram diagram = new Diagram())
                {
                    // Determine the next available page ID
                    int maxPageId = 0;
                    foreach (Page p in diagram.Pages)
                    {
                        if (p.ID > maxPageId)
                            maxPageId = p.ID;
                    }

                    // Create a new background page
                    Page backgroundPage = new Page();
                    backgroundPage.ID = maxPageId + 1;
                    backgroundPage.Name = "BackgroundPage";
                    backgroundPage.Background = BOOL.True; // Mark as a background page

                    // Add the background page to the diagram
                    diagram.Pages.Add(backgroundPage);

                    // Retrieve page dimensions (default is 8.5 x 11 inches if not set)
                    double pageWidth = backgroundPage.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = backgroundPage.PageSheet.PageProps.PageHeight.Value;

                    // Add a rectangle shape that spans the entire page to serve as the background
                    // PinX and PinY are the center of the shape; set them to half of width/height
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;
                    long bgShapeId = diagram.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle", backgroundPage.ID);

                    // Retrieve the shape object to apply styling
                    Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);

                    // Apply solid fill with a light blue color
                    bgShape.Fill.FillPattern.Value = 1;               // Solid fill
                    bgShape.Fill.FillForegnd.Value = "#ADD8E6";       // Light blue

                    // Remove any border line
                    bgShape.Line.LinePattern.Value = 0;               // No line

                    // Send the shape to the back so other content appears above it
                    bgShape.SendToBack();

                    // Make the background shape non‑selectable
                    bgShape.Protection.LockSelect.Value = BOOL.True;

                    // Add a SolutionXML entry describing the background page
                    SolutionXML solXml = new SolutionXML();
                    solXml.Name = "PageBackgroundInfo";
                    solXml.XmlValue = $"<Page ID=\"{backgroundPage.ID}\" Name=\"{backgroundPage.Name}\" />";
                    diagram.SolutionXMLs.Add(solXml);

                    // Save the diagram to a VSDX file
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }