namespace SourceData.Models
{
    public class ZasobyModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SkladId { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
