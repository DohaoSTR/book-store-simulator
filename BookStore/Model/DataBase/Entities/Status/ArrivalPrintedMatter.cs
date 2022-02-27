using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class ArrivalPrintedMatter : DataBaseEntity
    {
        public PrintedMatter PrintedMatter { get; set; }

        private int _count;
        public int Count
        {
            get => _count;
            set => _count = value > 0
                ? value
                : throw new ArgumentOutOfRangeException("Количество товара должно быть больше 0!");
        }

        public DateTime ArrivalDate { get; set; }

        public ArrivalPrintedMatter(int idPrintedMatter, int count, DateTime arrivalDate)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            Count = count;
            ArrivalDate = arrivalDate;
        }

        public ArrivalPrintedMatter(long? id, int idPrintedMatter, int count, DateTime arrivalDate) : base(id)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            Count = count;
            ArrivalDate = arrivalDate;
        }

        public ArrivalPrintedMatter() { }

        public override string TableName => "arrival_printed_matter";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "id_printed_matter", PrintedMatter.Id },
                { "count", Count },
                { "arrival_date", ArrivalDate.ToString("yyyy-MM-dd") } };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new ArrivalPrintedMatter(Convert.ToInt32(g["id"]),
                    Convert.ToInt32(g["id_printed_matter"]),
                    Convert.ToInt32(g["count"]), Convert.ToDateTime(g["arrival_date"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
