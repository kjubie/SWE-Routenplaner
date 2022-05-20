﻿namespace SWEN2_REST.DAL {
    public class FileHandler {
        private readonly ILogger<FileHandler> _logger;

        public FileHandler(ILogger<FileHandler> logger) {
            _logger = logger;
        }

        public async Task<int> SaveToFileAsync(string name, string base64) {
            try {
                await File.WriteAllTextAsync("../../SWEN2-DB/routeImages/" + name + ".txt", base64);
            } catch (Exception ex) {
                _logger.LogError("Error while saving image! " + ex);
                return -1;
            }
            return 0;
        }

        public string LoadFromFile(string name) {
            try {
                return File.ReadAllText(@"../../SWEN2-DB/routeImages/" + name + ".txt");
            } catch (Exception ex) {
                _logger.LogError("Error while loading image! " + ex);
                return null;
            }
        }

        internal async Task<int> SaveImage(byte[] result, string name) {
            try {
                ;// await File.WriteAllTextAsync("../../SWEN2-DB/routeImages/" + name + ".jpg", result);
            } catch (Exception ex) {
                _logger.LogError("Error while saving image! " + ex);
                return -1;
            }
            return 0;
        }
    }
}
