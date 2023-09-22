using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Portsea.Utils.Net
{
    public class ImageDownloader : IImageDownloader, IDisposable
    {
        private readonly HttpClient httpClient;
        private bool disposed;

        public ImageDownloader(HttpClient httpClient = null)
        {
            this.httpClient = httpClient ?? new HttpClient();
        }

        public byte[] DownloadImageBytes(Uri uri)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            var task = Task.Run(() => this.httpClient.GetByteArrayAsync(uri));
            return task.GetAwaiter().GetResult();
        }

        public async Task<byte[]> DownloadImageBytesAsync(Uri uri)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            return await this.httpClient.GetByteArrayAsync(uri);
        }

        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.httpClient.Dispose();
            GC.SuppressFinalize(this);
            this.disposed = true;
        }
    }
}
