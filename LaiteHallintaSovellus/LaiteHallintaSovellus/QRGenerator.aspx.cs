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

        // Metodi tallentaa seuraavan vapaan koodi id:n
        protected void SaveQrCount(int count)
        {
            // luodaan kirjoitin ja avataan tiedosto
            StreamWriter sw = new StreamWriter("C:\\Temp\\count.txt"); 

            // kirjoitetaan luku tiedostoon
            sw.WriteLine(count);

            // suljetaan streami
            sw.Close();
        }

        // Metodi palauttaa seuraavan vapaan koodi id:n
        static int getQrCount()
        {
                // luodaan kirjoitin ja avataan tiedosto
                StreamReader sr = new StreamReader("C:\\Temp\\count.txt");

                // luetaan rivi tiedostosta
                int count = int.Parse(sr.ReadLine());
  
                // suljetaan streami
                sr.Close();

                // palautetaan int arvo
                return count;
        }

        // Metodi muodostaa QR koodin ja tallentaa sen QRCodes kansioon
        protected int CreateQRCodeId()
        {

            string path = "c:\\Temp\\LaiteHallintaSovellus\\LaiteHallintaSovellus\\LaiteHallintaSovellus\\Images\\QRCodes\\";

            // Haetaan seuraava vapaa koodi
            int id = getQrCount();

            // Muodostetaan QR encoder
            QRCodeEncoder encoder = new QRCodeEncoder();

            // 30% virheenkorjaus
            encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            // Muodostetaan QR koodi
            Bitmap img = encoder.Encode("http://www.google.com/id=" + id);
            img.Save(path + id + ".jpg", ImageFormat.Jpeg);

            // Kasvatetaan ja tallennetaan uusi vapaa arvo
            SaveQrCount(id + 1);

            //palautetaan arvo
            return id;
        }


        // Metodi muodostaa koodeille pdf tiedoston
        protected void CreatePDF_OnClick(object sender, EventArgs e)
        {

            // Tekstityyli
            var bodyFont = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL);

            // Taulun luonti
            var qrTable = new PdfPTable(5);
            qrTable.HorizontalAlignment = 0;
            qrTable.SpacingBefore = 10;
            qrTable.SpacingAfter = 10;
            qrTable.DefaultCell.Border = 0;
                
            // Luodaan document objekti
            var document = new Document(PageSize.A4, 50, 50, 25, 25);

            // Luodaan PdfWriter objekti ja kirjoitin
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            // Avataan dokumentti kirjoittamista varten 
            document.Open();

            for (int i = 0; i < 40; i++)
            {
                // Haetaan seuraava koodi
                int id = CreateQRCodeId();            
                
                // Haetaan koodia vastaava kuva
                var img = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/QRCodes/"+id+".jpg"));

                // Muodostetaan uusi elementti tauluun
                var cell = new PdfPCell { PaddingLeft = 15, PaddingTop = 15, PaddingBottom = 15, PaddingRight = 15, Border = 0 };
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.AddElement(img);
                cell.AddElement(new Paragraph(id.ToString(), bodyFont));

                // Lisätään elementti tauluun
                qrTable.AddCell(cell);
            }

            // Lisätään taulu pdf-tiedostoon
            document.Add(qrTable);

            // Suljetaan dokumentti ja syötetään lopputulos selaimelle. 
            document.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=QRCodes.pdf"));
            Response.BinaryWrite(output.ToArray());
        }
    }
}