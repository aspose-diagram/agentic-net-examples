using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths – adjust as needed
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Ensure there are at least two pages (source and target)
                    if (diagram.Pages.Count < 2)
                    {
                        // Create a new target page if it does not exist
                        Page newPage = new Page();
                        newPage.Name = "ClonedPage";
                        diagram.Pages.Add(newPage);
                    }

                    // Source page (first page) and target page (second page)
                    Page sourcePage = diagram.Pages[0];
                    Page targetPage = diagram.Pages[1];

                    // Validate that the source page has at least one shape
                    if (sourcePage.Shapes.Count == 0)
                    {
                        Console.WriteLine("Source page contains no shapes to clone.");
                        return;
                    }

                    // Get the first shape on the source page as the shape to clone
                    Shape originalShape = sourcePage.Shapes[0];

                    // Retrieve master name – required to create a shape of the same type
                    string masterName = originalShape.Master?.Name;
                    if (string.IsNullOrEmpty(masterName))
                    {
                        Console.WriteLine("The original shape does not have an associated master.");
                        return;
                    }

                    // Capture geometry of the original shape
                    double pinX = originalShape.XForm.PinX.Value;
                    double pinY = originalShape.XForm.PinY.Value;
                    double width = originalShape.XForm.Width.Value;
                    double height = originalShape.XForm.Height.Value;

                    // Add a new shape to the target page using the same master and geometry
                    long newShapeId = targetPage.AddShape(pinX, pinY, width, height, masterName);
                    Shape clonedShape = targetPage.Shapes.GetShape(newShapeId);

                    // Copy all user‑defined cells from the original shape to the cloned shape
                    foreach (User userCell in originalShape.Users)
                    {
                        User newUser = new User
                        {
                            Name = userCell.Name,
                            NameU = userCell.NameU,
                            Value = { Val = userCell.Value.Val },
                            Prompt = { Value = userCell.Prompt.Value }
                        };
                        clonedShape.Users.Add(newUser);
                    }

                    // Optional: copy other properties (e.g., text) if desired
                    // clonedShape.Text.Value.Clear();
                    // clonedShape.Text.Value.Add(new Txt(originalShape.Text.Value.Text));

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Shape cloned and saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }