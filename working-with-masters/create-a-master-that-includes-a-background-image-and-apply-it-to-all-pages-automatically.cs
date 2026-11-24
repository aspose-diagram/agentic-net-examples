using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <backgroundImagePath> <outputVisioPath>");
            return;
        }

        string inputVisioPath = args[0];
        string backgroundImagePath = args[1];
        string outputVisioPath = args[2];

        // Guard against missing files.
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputVisioPath}");
            return;
        }
        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image file not found: {backgroundImagePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // ------------------------------------------------------------
                // 1. Create a new master that will hold the background image.
                // ------------------------------------------------------------
                Master bgMaster = new Master();

                // Assign a unique ID and name for the master.
                bgMaster.ID = diagram.Masters.Count + 1;
                bgMaster.Name = "BackgroundMaster";
                bgMaster.UniqueID = Guid.NewGuid();
                bgMaster.BaseID = Guid.NewGuid();
                bgMaster.Hidden = BOOL.False;

                // Create a shape that contains the image as foreign data.
                Shape imgShape = new Shape();
                imgShape.Type = TypeValue.Foreign;                     // Mark as foreign (image) shape.
                imgShape.ForeignData.ForeignType = ForeignType.Bitmap; // Specify bitmap image type.
                imgShape.ForeignData.Value = File.ReadAllBytes(backgroundImagePath); // Embed image bytes.

                // Set placeholder geometry; actual size will be defined when the shape is placed on a page.
                imgShape.XForm.Width.Value = 1.0;
                imgShape.XForm.Height.Value = 1.0;
                imgShape.XForm.PinX.Value = 0.0;
                imgShape.XForm.PinY.Value = 0.0;

                // Add the image shape to the master.
                bgMaster.Shapes.Add(imgShape);

                // Add the master to the diagram's master collection.
                diagram.Masters.Add(bgMaster);

                // ------------------------------------------------------------
                // 2. Apply the background master to every page.
                // ------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center coordinates for the shape.
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add the master shape to the page with the page size.
                    long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, bgMaster.Name, false); // isCalculate expects bool

                    // Retrieve the newly added shape to adjust its properties.
                    Shape bgShape = page.Shapes.GetShape(shapeId);
                    bgShape.SendToBack();                                 // Ensure it is behind other content.
                    bgShape.Protection.LockSelect.Value = BOOL.True;     // Make the background non‑selectable.
                }

                // ------------------------------------------------------------
                // 3. Save the modified diagram.
                // ------------------------------------------------------------
                diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Background master applied to all pages successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
