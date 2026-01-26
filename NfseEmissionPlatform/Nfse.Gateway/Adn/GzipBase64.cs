using System.IO.Compression;
using System.Text;

namespace Nfse.Gateway.Adn
{
    public static class GzipBase64
    {
        public static string FromUtf8String(string xml)
        {
            byte[] input = Encoding.UTF8.GetBytes(xml);

            using MemoryStream output = new MemoryStream();
            using (GZipStream gzip = new GZipStream(output, CompressionLevel.SmallestSize, leaveOpen: true))
            {
                gzip.Write(input, 0, input.Length);
            }

            return Convert.ToBase64String(output.ToArray());
        }
    }
}
