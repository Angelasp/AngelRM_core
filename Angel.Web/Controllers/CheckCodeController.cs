﻿using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.IO;


namespace Angel.Web.Controllers
{
    public class CheckCodeController : Controller
    {
        //
        // GET: /CheckCode/
        public async Task Index()
        {

            Response.ContentType = "image/gif";
            using (var stream = CreateCheckCodeImage(GenerateCheckCode()))
            {
                var buffer = stream.ToArray();
                await Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        private string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                if (code == '0' || code == 'o' || code == 'L' || code == 'I')
                {
                    i = i - 1;
                }
                else
                {
                    checkCode += code.ToString();
                }
            }

            //	Response.Cookies.Add(new HttpCookie("CheckCode", checkCode));
            HttpContext.Session.SetString("checkcode", checkCode);

            return checkCode;
        }

        private static MemoryStream CreateCheckCodeImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return null;

            Bitmap image = new Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                g.Clear(Color.White);

                //画图片的背景噪音线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                // g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                MemoryStream ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

                //Response.Clear();
                //Response.ContentType = "image/Gif";
                ////ms.Write(ms.ToArray());

                // Response.BodyWriter.WriteAsync(ms.ToArray());
                //Response.Body.WriteAsync(ms.ToArray,0,ms.ToArray);
                return ms;
         }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }


    }

}
