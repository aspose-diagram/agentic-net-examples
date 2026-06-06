using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Obtain the Visio file bytes from a source (e.g., database, network).
        // For this example a placeholder method returns an empty array.
        byte[] visioBytes = GetVisioFileBytes();

        try
        {
            using (var inputStream = new MemoryStream(visioBytes))
            {
                Diagram diagram = new Diagram(inputStream);

                // Iterate through all pages and shapes to find ActiveX controls.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.ActiveXControl != null)
                        {
                            // Update properties based on the specific control type.
                            switch (shape.ActiveXControl.Type)
                            {
                                case ControlType.CommandButton:
                                    var button = (CommandButtonActiveXControl)shape.ActiveXControl;
                                    button.Caption = "Updated Caption";
                                    break;

                                case ControlType.TextBox:
                                    var textBox = (TextBoxActiveXControl)shape.ActiveXControl;
                                    textBox.Text = "Updated Text";
                                    break;

                                case ControlType.CheckBox:
                                    var checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                                    checkBox.Value = CheckValueType.Checked;
                                    break;

                                case ControlType.Image:
                                    var imageCtrl = (ImageActiveXControl)shape.ActiveXControl;
                                    imageCtrl.Width = 2.0;   // Width in inches
                                    imageCtrl.Height = 1.0;  // Height in inches
                                    break;

                                // Add handling for other control types as needed.
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Placeholder method – replace with actual implementation to retrieve Visio file bytes.
    private static byte[] GetVisioFileBytes()
    {
        return new byte[0];
    }
}