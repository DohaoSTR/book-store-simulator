using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class SoldPrintedMatter : DataBaseEntity
    {
        public PrintedMatter PrintedMatter { get; set; }

        public DateTime SoldDate { get; set; }

        private int _count;
        public int Count
        {
            get => _count;
            set => _count = value > 0
                ? value
                : throw new ArgumentOutOfRangeException("Количество товара должно быть больше 0!");
        }

        public Discount Discount { get; set; }

        public DiscountCard DiscountCard { get; set; }

        public GiftCard GiftCard { get; set; }

        public SoldPrintedMatter(int idPrintedMatter, int count, DateTime soldDate,
            int idDiscount, int idDiscountCard, int idGiftCard)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            SoldDate = soldDate;
            Count = count;
            Discount = (Discount)GetEntity(idDiscount, new Discount());
            DiscountCard = (DiscountCard)GetEntity(idDiscountCard, new DiscountCard());
            GiftCard = (GiftCard)GetEntity(idGiftCard, new GiftCard());
        }

        public SoldPrintedMatter(long? id, int idPrintedMatter, int count, DateTime soldDate,
            int idDiscount, int idDiscountCard, int idGiftCard) : base(id)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            SoldDate = soldDate;
            Count = count;
            Discount = (Discount)GetEntity(idDiscount, new Discount());
            DiscountCard = (DiscountCard)GetEntity(idDiscountCard, new DiscountCard());
            GiftCard = (GiftCard)GetEntity(idGiftCard, new GiftCard());
        }

        public SoldPrintedMatter() { }

        public override string TableName => "sold_printed_matter";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "id_printed_matter", PrintedMatter.Id },
                { "count", Count },
                { "sold_date", SoldDate.ToString("yyyy-MM-dd") },
                { "id_discount", Discount.Id },
                { "id_discount_card", DiscountCard.Id },
                { "id_gift_card", GiftCard.Id } };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new SoldPrintedMatter(Convert.ToInt32(g["id"]),
                    Convert.ToInt32(g["id_printed_matter"]),
                    Convert.ToInt32(g["count"]),
                    Convert.ToDateTime(g["sold_date"]),
                    Convert.ToInt32(g["id_discount"]),
                    Convert.ToInt32(g["id_discount_card"]),
                    Convert.ToInt32(g["id_gift_card"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
