﻿namespace SWEN2_REST.DAL {
    public class FileHandler {
        public FileHandler() {

        }

        public async Task<int> SaveToFileAsync(string name, string base64) {
            try {
                await File.WriteAllTextAsync("../../SWEN2-DB/routeImages/" + name + ".txt", base64);
            } catch (Exception ex) {
                return -1;
            }
            return 0;
        }

        public string LoadFromFile(string name) {
            try {
                return File.ReadAllText(@"../../SWEN2-DB/routeImages/" + name + ".txt");
            } catch (Exception ex) {
                return null;
            }
        }
    }
}
