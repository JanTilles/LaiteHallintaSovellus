using System;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.Drawing.Imaging;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;

namespace LaiteHallintaSovellus
{

    public partial class QRGenerator : System.Web.UI.Page
    {

        int id = getQrCount();
       
        protected void Page_Load(object sender, EventArgs e)
        {           
        }

        protected void SaveQrCount(int count)
        {
            // create a writer and open the file
            StreamWriter sw = new StreamWriter("C:\\Temp\\count.txt"); 

            // write a line of text to the file
            sw.WriteLine(count);

            // close the stream
            sw.Close();
        }

        static int getQrCount()
        {
                // create reader & open file
                StreamReader sr = new StreamReader("C:\\Temp\\count.txt");

                // read a line of text
                int count = int.Parse(sr.ReadLine());
  
                // close the stream
                sr.Close();

                // return count
                return count;
        }

        protected void CreateCode_OnClick(object sender, EventArgs e)
        {
            string path = "c:\\Temp\\LaiteHallintaSovellus\\LaiteHallintaSovellus\\LaiteHallintaSovellus\\Images\\QRCodes\\";

            QRCodeEncoder encoder = new QRCodeEncoder();
         
            // 30% virheenkorjaus
            encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            for (int i = 0; i < 5; i++)
            {
                Bitmap img = encoder.Encode("http://www.google.com/id=" + id);
                img.Save(path + id +".jpg", ImageFormat.Jpeg);
                id = id + 1;
            }
            SaveQrCount(id);
        }

        protected void CreatePDF_OnClick(object sender, EventArgs e)
        {

            // Create Font Styles
            var titleFont = FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
            var subTitleFont = FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD);
            var boldTableFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD);
            var endingMessageFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.ITALIC);
            var bodyFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL);

            var txtOrderID = "ID tieto";

            // Create Table
            var orderInfoTable = new PdfPTable(2);
            orderInfoTable.HorizontalAlignment = 0;
            orderInfoTable.SpacingBefore = 10;
            orderInfoTable.SpacingAfter = 10;
            orderInfoTable.DefaultCell.Border = 0;
            orderInfoTable.SetWidths(new int[] { 1, 4 });

            
            orderInfoTable.AddCell(new Phrase("Koodi:", boldTableFont));

            // Get Image
            var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/QRCodes/1234.jpg"));
            //logo.SetAbsolutePosition(440, 800);
            orderInfoTable.AddCell(logo);

            // Create a Document object
            var document = new Document(PageSize.A4, 50, 50, 25, 25);

            // Create a new PdfWriter object, specifying the output stream
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            // Open the Document for writing
            document.Open();

            // Add the Paragraph object to the document
            document.Add(new Paragraph("Testi teksti ", bodyFont));

            // Add table to document
            document.Add(orderInfoTable);

            // Add code to document
            //document.Add(logo);

            // Close the Document - this saves the document contents to the output stream
            document.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Receipt-{0}.pdf", txtOrderID));
            Response.BinaryWrite(output.ToArray());
        }
    }
}