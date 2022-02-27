using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class Discount : DataBaseEntity
    {
        private int _percent;
        public int Percent
        {
            get => _percent;
            set => _percent = value > 0 ? value : throw new ArgumentOutOfRangeException("Процент должен быть больше нуля!");
        }

        public DateTime DateStart { get; set; }

        private DateTime _dateEnd;
        public DateTime DateEnd
        {
            get => _dateEnd;
            set => _dateEnd = value.Date < DateStart.Date
                ? throw new ArgumentOutOfRangeException("Дата окончания действия скидки не должна быть раньше даты начала !")
                : value;
        }

        public override string TableName => "discount";

        public Discount(int percent, DateTime dateStart, DateTime dateEnd)
        {
            Percent = percent;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        public Discount(long? id, int percent, DateTime dateStart, DateTime dateEnd) : base(id)
        {
            Percent = percent;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        public Discount() { }

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "percent", Percent },
                { "date_start", DateStart.ToString("yyyy-MM-dd") },
                { "date_end", DateEnd.ToString("yyyy-MM-dd") } };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new Discount(Convert.ToInt32(g["id"]), Convert.ToInt32(g["percent"]),
                    Convert.ToDateTime(g["date_start"]), Convert.ToDateTime(g["date_end"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
