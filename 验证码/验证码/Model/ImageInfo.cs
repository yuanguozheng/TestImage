using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace 验证码.Model
{
    class ImageInfo
    {
        public string Info { set; get; }
        public void RecImageInfo()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://image.baidu.com/pictureup/uploadshitu?fr=flash");
            MemoryStream ms = new MemoryStream();
            //构建boundary
            string boundary = DateTime.Now.Ticks.ToString("x");
            //开始边界符
            string beginBoundary = string.Format("--{0}\r\n", boundary);
            //结束边界符
            string endBoundary = string.Format("--{0}--\r\n", boundary);
            //字符串的key
            string key = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n";
            //设置请求头
            req.Method = "POST";
            req.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);

            //构建formdata
            //构建
            using (Stream reqStream = req.GetRequestStream())
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(beginBoundary + string.Format(key, "filesize") + "465664");
                sb.AppendLine(beginBoundary + string.Format(key, "Filename") + "美女.jpg");
                sb.AppendLine(beginBoundary + string.Format(key, "fileheight") + "0");
                sb.AppendLine(beginBoundary + string.Format(key, "filewidth") + "0");
                sb.AppendLine(beginBoundary + string.Format(key, "newfilesize") + "465664");
                sb.AppendLine(beginBoundary + string.Format(key, "compresstime") + "0");
                sb.AppendLine(beginBoundary + string.Format(key, "filetype") + ".jpg");
                sb.AppendLine(beginBoundary + "Content-Disposition: form-data; name=\"filedata\"; filename=\"美女.jpg\"\r\nContent-Type: application/octet-stream");
                sb.Append("\r\n");

                byte[] strStream = Encoding.UTF8.GetBytes(sb.ToString());

                using (var stream = File.Open("美女.jpg", FileMode.Open))
                {
                    byte[] b = new byte[stream.Length];
                    stream.Read(b, 0, (int)stream.Length);
                    reqStream.Write(strStream, 0, strStream.Length);
                    reqStream.Write(b, 0, b.Length);
                }

                sb = new StringBuilder();
                sb.Append("\r\n");
                sb.AppendLine(beginBoundary + "Content-Disposition: form-data; name=\"Upload\"");
                sb.Append("\r\n");
                sb.Append("Submit Query\r\n" + endBoundary);
                byte[] bstrStream = Encoding.UTF8.GetBytes(sb.ToString());
                reqStream.Write(bstrStream, 0, bstrStream.Length);
            }

            using (Stream s = req.GetResponse().GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    //获取返回的网页
                   Info = reader.ReadToEnd();
                }
            }
        }
    }
}
