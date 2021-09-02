using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _928.Web {
    public class MinifyFilter : Stream {

        private readonly Stream _stream;

        public override bool CanRead {
            get { return true; }
        }

        public override bool CanSeek {
            get { return true; }
        }

        public override bool CanWrite {
            get { return true; }
        }

        public override long Length {
            get { return 0; }
        }

        public override long Position { get; set; }

        public override void Flush() {
            _stream.Flush();
        }


        public override int Read(byte[] buffer, int offset, int count) {
            return _stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin) {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value) {
            _stream.SetLength(value);
        }

        public override void Close() {
            _stream.Close();
        }

        public override void Write(byte[] buffer, int offset, int count) {
            byte[] data = new byte[count];

            Buffer.BlockCopy(buffer, offset, data, 0, count);

            string content = Encoding.Default.GetString(buffer);
            //content = content.Replace("\r\n", string.Empty);
            content = Regex.Replace(content, @"(?s)\s+(?!(?:(?!</?pre\b).)*</pre>)", " ");
            content = Regex.Replace(content, @"(?s)\s*\n\s*(?!(?:(?!</?pre\b).)*</pre>)", "\n");
            content = Regex.Replace(content, @"(?s)\s*\>\s*\<\s*(?!(?:(?!</?pre\b).)*</pre>)", "><");
            content = Regex.Replace(content, @"(?s)<!--((?:(?!</?pre\b).)*?)-->(?!(?:(?!</?pre\b).)*</pre>)", "");

            byte[] output = Encoding.Default.GetBytes(content.Trim());

            _stream.Write(output, 0, output.GetLength(0));
        }

        public MinifyFilter(Stream stream) {
            _stream = stream;
        }
    }
}
