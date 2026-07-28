using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // -------------------------------------------------
                // 1. Add a new CommandButton ActiveX control
                // -------------------------------------------------
                // Parameters: ControlType, PinX, PinY, Width, Height (in inches)
                long commandButtonId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);
                Shape commandButtonShape = page.Shapes.GetShape(commandButtonId);

                // Cast the ActiveXControl to the specific type
                CommandButtonActiveXControl commandButton = (CommandButtonActiveXControl)commandButtonShape.ActiveXControl;

                // Set visual properties
                commandButton.Caption = "Click Me";
                commandButton.Width = 1.5;   // width in inches
                commandButton.Height = 0.5;  // height in inches

                Console.WriteLine($"Added CommandButton ActiveX control with ID {commandButtonId}.");

                // -------------------------------------------------
                // 2. Retrieve and update existing ActiveX controls
                // -------------------------------------------------
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ActiveXControl != null)
                    {
                        // Identify the control type
                        if (shape.ActiveXControl.Type == ControlType.CommandButton)
                        {
                            var btn = (CommandButtonActiveXControl)shape.ActiveXControl;
                            Console.WriteLine($"Found CommandButton (ID {shape.ID}) with caption: {btn.Caption}");
                            // Example update: prepend text
                            btn.Caption = "Updated: " + btn.Caption;
                        }
                        else if (shape.ActiveXControl.Type == ControlType.CheckBox)
                        {
                            var chk = (CheckBoxActiveXControl)shape.ActiveXControl;
                            Console.WriteLine($"Found CheckBox (ID {shape.ID}) with value: {chk.Value}");
                            // Toggle the check state
                            chk.Value = chk.Value == CheckValueType.Checked ? (CheckValueType)0 : CheckValueType.Checked;
                        }
                        else if (shape.ActiveXControl.Type == ControlType.Image)
                        {
                            var img = (ImageActiveXControl)shape.ActiveXControl;
                            Console.WriteLine($"Found Image control (ID {shape.ID}).");
                            // Assign a new picture (example assumes image file exists)
                            string imagePath = "sample.png";
                            if (System.IO.File.Exists(imagePath))
                            {
                                img.Picture = System.IO.File.ReadAllBytes(imagePath);
                            }
                        }
                    }
                }

                // -------------------------------------------------
                // 3. Save the updated diagram
                // -------------------------------------------------
                DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx)
                {
                    AutoFitPageToDrawingContent = true,
                    DefaultFont = "Arial"
                };

                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }