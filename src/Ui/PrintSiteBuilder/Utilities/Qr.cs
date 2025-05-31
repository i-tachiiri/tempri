using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using System.Drawing;

namespace PrintSiteBuilder.Utilities
{
    public class Qr
    {
        public Bitmap GenerateQRCode(string url)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                using (var qrCode = new QRCode(qrCodeData))
                {
                    var darkGray = Color.FromArgb(77, 77, 77); // RGBで#4d4d4dに相当
                    var qrCodeImage = qrCode.GetGraphic(10, darkGray, Color.Transparent, false);
                    return qrCodeImage;
                }
            }
        }

    }
}
