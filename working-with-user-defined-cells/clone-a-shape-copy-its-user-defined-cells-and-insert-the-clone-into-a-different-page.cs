using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <sourceVisioPath> <outputVisioPath> <sourcePageName> <targetPageName>");
            return;
        }

        string sourcePath = args[0];
        string outputPath = args[1];
        string sourcePageName = args[2];
        string targetPageName = args[3];

        // Guard against missing source file.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the source diagram.
            Diagram diagram = new Diagram(sourcePath);

            // Retrieve the source page by name.
            Page sourcePage = null;
            foreach (Page pg in diagram.Pages)
            {
                if (pg.Name == sourcePageName)
                {
                    sourcePage = pg;
                    break;
                }
            }

            if (sourcePage == null)
            {
                Console.Error.WriteLine($"Source page \"{sourcePageName}\" not found.");
                return;
            }

            // Retrieve (or create) the target page.
            Page targetPage = null;
            foreach (Page pg in diagram.Pages)
            {
                if (pg.Name == targetPageName)
                {
                    targetPage = pg;
                    break;
                }
            }

            if (targetPage == null)
            {
                // Create a new page and add it to the diagram.
                targetPage = new Page();
                // Assign a unique ID (max existing ID + 1).
                int maxId = 0;
                foreach (Page pg in diagram.Pages)
                {
                    if (pg.ID > maxId) maxId = pg.ID;
                }
                targetPage.ID = maxId + 1;
                targetPage.Name = targetPageName;
                diagram.Pages.Add(targetPage);
            }

            // Choose a shape to clone – here we take the first shape on the source page.
            if (sourcePage.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Source page contains no shapes to clone.");
                return;
            }

            Shape originalShape = sourcePage.Shapes[0];

            // Determine master name for the shape (fallback to a basic rectangle if missing).
            string masterName = (originalShape.Master != null) ? originalShape.Master.Name : "Rectangle";

            // Retrieve original position.
            double pinX = originalShape.XForm.PinX.Value;
            double pinY = originalShape.XForm.PinY.Value;

            // Add a new shape to the target page using the same master and position.
            long newShapeId = targetPage.AddShape(pinX, pinY, masterName, false);
            Shape clonedShape = targetPage.Shapes.GetShape((int)newShapeId);

            // Copy user‑defined cells from the original shape to the cloned shape.
            foreach (User userField in originalShape.Users)
            {
                // Create a new User object and copy properties.
                User newUser = new User();
                newUser.NameU = userField.NameU;
                newUser.Prompt.Value = userField.Prompt.Value;
                newUser.Value.Val = userField.Value.Val;

                // Add the copied user‑defined cell to the cloned shape.
                clonedShape.Users.Add(newUser);
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape cloned and saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}