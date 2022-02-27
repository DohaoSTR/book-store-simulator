using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class Publisher : DataBaseEntity
    {
        public string _name;
        public string Name
        {
            get => _name;
            set => _name = value.Length > 100 && value.Length <= 0
                ? throw new ArgumentOutOfRangeException("Имя издателя должно быть " +
                    "длинной больше 0 символов и не превышать длину в 100 символов!")
                : value;
        }

        public Publisher(string name)
        {
            Name = name;
        }

        public Publisher(long? id, string name) : base(id)
        {
            Name = name;
        }

        public Publisher() { }

        public override string TableName => "publisher";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "name", Name }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new Publisher(Convert.ToInt32(g["id"]), g["name"]);

                entities.Add(entity);
            }

            return entities;
        }
    }
}
