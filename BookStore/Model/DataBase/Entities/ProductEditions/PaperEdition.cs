using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class PaperEdition : DataBaseEntity
    {
        public PrintedMatter PrintedMatter { get; set; }

        private string _typeCover;
        public string TypeCover
        {
            get => _typeCover;
            set => _typeCover = value.Length > 50 && value.Length <= 0
                ? throw new ArgumentOutOfRangeException("Тип обложки должен быть " +
                    "длинной больше 0 символов и не превышать длину в 100 символов!")
                : value;
        }

        private int _weight;
        public int Weight
        {
            get => _weight;
            set => _weight = value < 0
                ? throw new ArgumentOutOfRangeException("Вес изделия должен быть " +
                    "больше нуля!")
                : value;
        }


        private int _price;
        public int Price
        {
            get => _price;
            set => _price = value < 0
                ? throw new ArgumentOutOfRangeException("Цена изделия должна быть " +
                    "больше нуля!")
                : value;
        }

        public GoodTransportWaybill GoodTransportWaybill { get; set; }

        public PaperEdition(int idPrintedMatter, string typeCover, int weight, int price, int idGoodTransportWaybill)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            TypeCover = typeCover;
            Weight = weight;
            Price = price;
            GoodTransportWaybill = (GoodTransportWaybill)GetEntity(idGoodTransportWaybill, new GoodTransportWaybill());
        }

        public PaperEdition(long? id, int idPrintedMatter,
            string typeCover, int weight, int price, int idGoodTransportWaybill) : base(id)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter()); ;
            TypeCover = typeCover;
            Weight = weight;
            Price = price;
            GoodTransportWaybill = (GoodTransportWaybill)GetEntity(idGoodTransportWaybill, new GoodTransportWaybill());
        }

        public PaperEdition() { }

        public override string TableName => "paper_edition";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "id_printed_matter", PrintedMatter.Id },
                { "type_cover", TypeCover },
                { "weight", Weight },
                { "price", Price },
                { "id_good_transport_waybill", GoodTransportWaybill.Id }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new PaperEdition(Convert.ToInt32(g["id"]),
                    Convert.ToInt32(g["id_printed_matter"]),
                    g["type_cover"], Convert.ToInt32(g["weight"]), Convert.ToInt32(g["price"]),
                    Convert.ToInt32(g["id_good_transport_waybill"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
