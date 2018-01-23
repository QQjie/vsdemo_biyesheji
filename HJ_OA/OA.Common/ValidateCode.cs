using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;
using log4net;

namespace OA.Common
{
    public class ValidateCode
    {
        public ValidateCode() { }
       
        //颜色列表
        private Color[] colors = {Color.Black,Color.Red,Color.Blue,Color.Green,Color.Brown,Color.Orange,Color.Brown,Color.DarkBlue };
        //字体列表
        private string[] fonts = {"Times New Roman","MS Mincho","Book Antiqua","Gungsuh","PMingLiu","Impact" };

        //验证码的字符集，去掉了一些容易混淆的字符
        //char[] characters = { };
        char[] charaters = {'2','3','4','5','6','7','8','9','a','b','c','d','e','f','g','h','j','k','m','n','p','r','s','t','w','x','y','A','B','C','D','E','F','G','H','J','K','L','M','N','P','R','S','T','W','X','Y'};
        
        string validateStr = string.Empty;
        
        public string   CreateValidateCode(int length) 
        {
            Random rand = new Random();
            //生成验证码字符串
            for (int i = 0; i < length; i++)
            {
                validateStr += charaters[rand.Next(charaters.Length)];
            }
            return validateStr;
        }


        ///
        ///创建验证码的图片
        ///
        public byte[] CreateValidateGraphic(string validateCode)
        {
            //Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length*12.0),22);
            Bitmap image = new Bitmap(100,40);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random rand = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 20; i++)
                {
                    int x1 = rand.Next(image.Width);
                    int x2 = rand.Next(image.Width);
                    int y1 = rand.Next(image.Height);
                    int y2 = rand.Next(image.Height);
                    Color cl = Color.FromArgb(rand.Next(150, 255), rand.Next(150, 255), rand.Next(150, 255));
                    g.DrawLine(new Pen(cl), x1, y1, x2, y2);
                }
             
                //画图片的干扰点
                for (int i = 0; i < 80; i++)
                {
                    int x = rand.Next(image.Width);
                    int y = rand.Next(image.Height);
                    Color cl = Color.FromArgb(rand.Next(100, 255), rand.Next(100, 255), rand.Next(100, 255));
                    image.SetPixel(x, y, cl);
                }
                
                //画字符串
                for (int i = 0; i < validateCode.Length; i++)
                {
                   string fnt=fonts[rand.Next(fonts.Length)];
                   Font font = new Font(fnt,18);
                  
                   Color cl = Color.FromArgb(rand.Next(0, 200), rand.Next(0, 200), rand.Next(0, 200));
                   g.DrawString(validateCode[i].ToString(),font,new SolidBrush(cl),(float)i*17+5,(float)8);

               }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
            
                //输出图 片 流
                return stream.ToArray();
            }
            catch (Exception)
            {
                ILog logwriter = log4net.LogManager.GetLogger("DemoWriter");
                logwriter.Error("产生验证码发生错误");
                byte[] b=new byte[2];
                return b; 
           }
            finally {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}
