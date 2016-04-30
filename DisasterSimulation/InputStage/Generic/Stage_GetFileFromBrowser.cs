using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Get a file from the file browser using the Microsoft API.
/// </summary>
partial class Stage
{
    
    public static bool GetFileFromBrowser(out string fileLocation)
    {
        OpenFileDialog OFD = new OpenFileDialog();
        OFD.Multiselect = false;
        OFD.ValidateNames = true;
        OFD.CheckFileExists = true;

        if (OFD.ShowDialog() == DialogResult.OK)
        {
            fileLocation = OFD.FileName;
            return true;
        } else
        {
            fileLocation = "NULL";
            return false;
        }
    }
}
