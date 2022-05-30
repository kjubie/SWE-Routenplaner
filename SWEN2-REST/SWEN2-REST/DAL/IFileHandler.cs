namespace SWEN2_REST.DAL {
    public interface IFileHandler {
        string LoadFromFile(string name);
        Task<string> SaveToFileAsync(string name, string base64);
    }
}