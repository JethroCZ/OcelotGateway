namespace SourceData.Models
{
    public class SkladModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
