using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Windows.Forms;

partial class InputStage : Stage
{ 
    public override void Update(Data data)
    {
        if (data.Input.CheckKey(Keyboard.Key.F))
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }
    }
}