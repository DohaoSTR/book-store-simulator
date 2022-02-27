using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class ElectronicEdition : DataBaseEntity
    {
        public PrintedMatter PrintedMatter { get; set; }

        private int _price;
        public int Price
        {
            get => _price;
            set => _price = value < 0
                ? throw new ArgumentOutOfRangeException("Цена изделия должна быть " +
                    "больше нуля!")
                : value;
        }

        public ElectronicEdition(int idPrintedMatter, int price)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            Price = price;
        }

        public ElectronicEdition(long? id,
            int idPrintedMatter, int price) : base(id)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            Price = price;
        }

        public ElectronicEdition() { }

        public override string TableName => "electronic_edition";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "id_printed_matter", PrintedMatter.Id },
                { "price", Price }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new ElectronicEdition(Convert.ToInt32(g["id"]),
                    Convert.ToInt32(g["id_printed_matter"]),
                    Convert.ToInt32(g["price"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
