using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        try
        {

            // Assume visioBytes contains the binary content of a Visio file.
            // In a real scenario this could come from a database, network stream, etc.
            byte[] visioBytes = File.ReadAllBytes("input.vsdx"); // placeholder source
            using (MemoryStream inputStream = new MemoryStream(visioBytes))
            {
                // Load the diagram from the memory stream.
                Diagram diagram = new Diagram(inputStream);

                // Iterate through all pages and shapes to find ActiveX controls.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Only shapes that contain an ActiveX control have a non‑null ActiveXControl property.
                        if (shape.ActiveXControl == null)
                            continue;

                        // Identify the control type and cast to the specific control class.
                        switch (shape.ActiveXControl.Type)
                        {
                            case ControlType.CommandButton:
                                var cmdBtn = (CommandButtonActiveXControl)shape.ActiveXControl;
                                // Update properties of the command button.
                                cmdBtn.Caption = "Updated Button";
                                cmdBtn.Width = 2.0;   // inches
                                cmdBtn.Height = 0.5;  // inches
                                break;

                            case ControlType.TextBox:
                                var txtBox = (TextBoxActiveXControl)shape.ActiveXControl;
                                // Update the displayed text.
                                txtBox.Text = "New text content";
                                break;

                            case ControlType.CheckBox:
                                var chkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                                // Set the checkbox to checked.
                                chkBox.Value = CheckValueType.Checked;
                                break;

                            case ControlType.Image:
                                var imgCtrl = (ImageActiveXControl)shape.ActiveXControl;
                                // Replace the image with a new picture loaded from a byte array.
                                byte[] newImage = File.ReadAllBytes("newImage.png"); // placeholder image source
                                imgCtrl.Picture = newImage;
                                break;

                            // Add handling for other control types as needed.
                            default:
                                // For unhandled control types you may log or ignore.
                                break;
                        }
                    }
                }

                // Optionally save the modified diagram back to a memory stream (no disk I/O).
                using (MemoryStream outputStream = new MemoryStream())
                {
                    diagram.Save(outputStream, SaveFileFormat.Vsdx);
                    // At this point outputStream contains the updated Visio file.
                    // It can be returned, sent over a network, or stored as needed.
                    Console.WriteLine($"Diagram updated. Output size: {outputStream.Length} bytes.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
