namespace SWEN2_Tourplanner_Models {
    public interface ITour {
        int Childfriendliness { get; set; }
        string Description { get; set; }
        double Distance { get; set; }
        string From { get; set; }
        string? ImageLocation { get; set; }
        string Info { get; set; }
        Dictionary<int, TourLog> Logs { get; set; }
        string Name { get; set; }
        int Popularity { get; set; }
        string Time { get; set; }
        string To { get; set; }
        string TransportType { get; set; }

        int AddLog(TourLog log);
        void CalcChildfriendliness();
        void CalcPopularity();
        int GeneratePDF();
        int RemoveLog(int id);
        int UpdateLog(int id, TourLog log);
    }
}