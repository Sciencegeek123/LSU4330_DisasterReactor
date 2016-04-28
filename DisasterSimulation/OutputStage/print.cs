using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SFML.System;


class print
{
        public bool printfunction(List<string> ImageTitles, List<string> ImagePaths)
    {
        Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
        PdfWriter wri;
        try
        {
            wri = PdfWriter.GetInstance(doc, new FileStream("output.pdf", FileMode.Create));
        }
        catch (Exception)
        {

            return false;
        }
            
        doc.Open();
        Paragraph title = new Paragraph("Output from Simulations");
        doc.Add(title);
        for (int i = 0; i < ImageTitles.Count; i++)
        {
            iTextSharp.text.Image trails;
            try
            {
                trails = iTextSharp.text.Image.GetInstance(ImagePaths[i]);
            }
            catch (Exception)
            {

                return false;
            }

            trails.ScaleToFit(300, 300);
            trails.Alignment = Image.ALIGN_CENTER;
            Paragraph paragraph = new Paragraph(ImageTitles.ElementAt(i));
            Paragraph temp = new Paragraph(i.ToString());
            if (i+1 > 1 && (i+1) % 2 == 0)
            {

                doc.Add(trails);
                doc.Add(paragraph);
                doc.NewPage();
            }
            else
            {

                doc.Add(trails);
                doc.Add(paragraph);



            }
            PdfPTable table = new PdfPTable(1);

            // jpg, png or gif
               
            //  trails.SetAbsolutePosition(200, 300 * i + 200);
            // table.AddCell(trails);
            // table.AddCell(ImageTitles.ElementAt(i));

            //doc.Add(paragraph);

        }

        doc.Close();
        return true;
    }
}


