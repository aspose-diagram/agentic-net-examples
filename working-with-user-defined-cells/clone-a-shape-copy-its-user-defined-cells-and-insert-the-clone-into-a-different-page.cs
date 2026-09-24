using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure at least two pages exist (source + target)
            if (diagram.Pages.Count < 2)
            {
                // Create a new blank page
                Page newPage = new Page();

                // Determine a unique page ID
                int maxId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxId) maxId = p.ID;
                }
                newPage.ID = maxId + 1;

                diagram.Pages.Add(newPage);
            }

            // Source page (original shape location)
            Page sourcePage = diagram.Pages[0];
            // Target page (where the cloned shape will be placed)
            Page targetPage = diagram.Pages[1];

            // Find the first non‑deleted shape on the source page
            Shape originalShape = null;
            foreach (Shape s in sourcePage.Shapes)
            {
                if (s.Del == BOOL.False)
                {
                    originalShape = s;
                    break;
                }
            }

            if (originalShape == null)
            {
                Console.Error.WriteLine("No suitable shape found to clone.");
                return;
            }

            // ----- Clone the shape -----
            // Retrieve master name (fallback to a generic master if null)
            string masterName = originalShape.Master?.Name ?? "Rectangle";

            // Preserve geometry of the original shape
            double pinX = originalShape.XForm.PinX.Value;
            double pinY = originalShape.XForm.PinY.Value;
            double width = originalShape.XForm.Width.Value;
            double height = originalShape.XForm.Height.Value;

            // Add a new shape on the target page using the same master and geometry
            long newShapeId = targetPage.AddShape(pinX, pinY, width, height, masterName, false);

            // Retrieve the newly added shape instance
            Shape clonedShape = targetPage.Shapes.GetShape(newShapeId);

            // Copy all user‑defined cells from the original shape to the clone
            foreach (User user in originalShape.Users)
            {
                User newUser = new User
                {
                    Name = user.Name,
                    NameU = user.NameU,
                    Value = { Val = user.Value.Val },
                    Prompt = { Value = user.Prompt.Value }
                };
                clonedShape.Users.Add(newUser);
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}