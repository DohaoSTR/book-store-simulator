using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class PriceList : DataBaseEntity
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

        public override string TableName => "price_list";

        public PriceList(int idPrintedMatter, int count)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            Count = count;
        }

        public PriceList(long? id, int idPrintedMatter, int count) : base(id)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            Count = count;
        }

        public PriceList() { }

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "id_printed_matter", PrintedMatter.Id },
                { "count", Count }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new PriceList(Convert.ToInt32(g["id"]),
                     Convert.ToInt32(g["id_printed_matter"]),
                     Convert.ToInt32(g["count"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
