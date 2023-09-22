using System;
using System.Threading.Tasks;

namespace Portsea.Utils.Net
{
    /// <summary>
    /// Download image data asynchronously from the <paramref name="uri"/>.
    /// </summary>
    /// <param name="uri">The URI for the image to download.</param>
    /// <returns>Image data as a byte array.</placeholder></returns>
    public interface IImageDownloader
    {
        byte[] DownloadImageBytes(Uri uri);

        Task<byte[]> DownloadImageBytesAsync(Uri uri);
    }
}
