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


            var stream = File.Open("美女.jpg", FileMode.Open);
            byte[] b = new byte[stream.Length];
            stream.Read(b, 0, (int)stream.Length);
            //构建formdata
            //构建

            StringBuilder sb = new StringBuilder();
            sb.Append(beginBoundary + string.Format(key, "filesize") + "465664");
            sb.Append("\r\n");
            sb.Append(beginBoundary + string.Format(key, "Filename") + "美女.jpg");
            sb.Append("\r\n");
            sb.Append(beginBoundary + string.Format(key, "fileheight") + "0");
            sb.Append("\r\n");
            sb.Append(beginBoundary + string.Format(key, "filewidth") + "0");
            sb.Append("\r\n");
            sb.Append(beginBoundary + string.Format(key, "newfilesize") + "465664");
            sb.Append("\r\n");
            sb.Append(beginBoundary + string.Format(key, "compresstime") + "439");
            sb.Append("\r\n");
            sb.Append(beginBoundary + string.Format(key, "filetype") + ".jpg");
            sb.Append("\r\n");
            sb.Append(beginBoundary + "Content-Disposition: form-data; name=\"filedata\"; filename=\"美女.jpg\"\r\nContent-Type: application/octet-stream");
            sb.Append("\r\n\r\n");


            byte[] buffer = new byte[sb.Length];
            buffer = Encoding.Default.GetBytes(sb.ToString());
            ms.Write(buffer, 0, buffer.Length);
            ms.Write(b, 0, b.Length);

            sb.Remove(0, sb.Length);
            sb.Append("\r\n");
            sb.Append(beginBoundary + "Content-Disposition: form-data; name=\"Upload\"");
            sb.Append("\r\n\r\n");
            sb.Append("Submit Query\r\n" + endBoundary);

            byte[] buf = new byte[sb.Length];
            buf = Encoding.Default.GetBytes(sb.ToString());
            ms.Write(buf, 0, buf.Length);

            var bufferms = new byte[ms.Length];
            ms.Read(bufferms, 0, (int)ms.Length);
            using (Stream s = req.GetRequestStream())
            {
                s.Write(bufferms, 0, bufferms.Length);
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
