namespace SWEN2_REST.BL {
    public interface IMapQuestContext {
        Task<byte[]> GetMapAsync(string from, string to);
        Task<string> GetRouteAsync(string from, string to, string type);
    }
}