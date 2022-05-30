namespace SWEN2_Tourplanner_Models {
    public interface ITourLog {
        string? Comment { get; set; }
        string? Date { get; set; }
        int Difficulty { get; set; }
        int Id { get; set; }
        int Rating { get; set; }
        string? Time { get; set; }
        string Tourname { get; set; }
    }
}