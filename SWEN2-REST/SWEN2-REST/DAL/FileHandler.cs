using System.Drawing;
using System.Drawing.Imaging;

namespace SWEN2_REST.DAL {
    public class FileHandler : IFileHandler {
        private readonly ILogger<FileHandler> _logger;

        public FileHandler(ILogger<FileHandler> logger) {
            _logger = logger;
        }

        public async Task<string> SaveToFileAsync(string name, string base64) {
            try {
                await File.WriteAllTextAsync("../../SWEN2-DB/routeImages/" + name + ".txt", base64);
            } catch (Exception ex) {
                _logger.LogError("Error while saving image! " + ex);
                return "" + ex;
            }
            return "";
        }

        public string LoadFromFile(string name) {
            try {
                return File.ReadAllText(@"../../SWEN2-DB/routeImages/" + name + ".txt");
            } catch (Exception ex) {
                _logger.LogError("Error while loading image! " + ex);
                return null;
            }
        }

        internal int SaveImage(byte[] imageByteArray, string name) {
            try {
                Bitmap bmp;

                using (var ms = new MemoryStream(imageByteArray)) {
                    bmp = new Bitmap(ms);
                    bmp.Save("../../SWEN2-DB/routeImages/" + name + ".jpg", ImageFormat.Jpeg);
                }
            } catch (Exception ex) {
                _logger.LogError("Error while saving image! " + ex);
                return -1;
            }
            return 0;
        }
    }
}
