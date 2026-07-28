using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the source diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there are at least two pages (source and target)
                if (diagram.Pages.Count < 2)
                {
                    // Add a new blank page as the target page
                    Page newPage = new Page();
                    diagram.Pages.Add(newPage);
                }

                // Source page (first page)
                Page sourcePage = diagram.Pages[0];
                // Target page (second page)
                Page targetPage = diagram.Pages[1];

                // Retrieve the first shape on the source page to clone
                Shape sourceShape = null;
                foreach (Shape s in sourcePage.Shapes)
                {
                    sourceShape = s;
                    break;
                }

                if (sourceShape == null)
                {
                    Console.WriteLine("No shape found on the source page to clone.");
                    return;
                }

                // Get master name of the source shape
                string masterName = sourceShape.Master != null ? sourceShape.Master.Name : null;
                if (string.IsNullOrEmpty(masterName))
                {
                    Console.WriteLine("Source shape does not have an associated master.");
                    return;
                }

                // Retrieve geometry and position values
                double pinX = sourceShape.XForm.PinX.Value;
                double pinY = sourceShape.XForm.PinY.Value;
                double width = sourceShape.XForm.Width.Value;
                double height = sourceShape.XForm.Height.Value;

                // Add a new shape on the target page using the same master and geometry
                long newShapeId = targetPage.AddShape(pinX, pinY, width, height, masterName);
                Shape clonedShape = targetPage.Shapes.GetShape(newShapeId);

                // Copy user-defined cells (Users collection) from source to cloned shape
                foreach (User user in sourceShape.Users)
                {
                    User newUser = new User();
                    newUser.Name = user.Name;
                    newUser.NameU = user.NameU;
                    newUser.Value.Val = user.Value.Val;
                    newUser.Prompt.Value = user.Prompt.Value;
                    clonedShape.Users.Add(newUser);
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Shape cloned and user-defined cells copied successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }