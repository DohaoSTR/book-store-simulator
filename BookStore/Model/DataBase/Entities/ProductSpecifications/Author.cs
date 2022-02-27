using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class Author : DataBaseEntity
    {
        public string _name;
        public string Name
        {
            get => _name;
            set => _name = value.Length > 50 && value.Length <= 0
                ? throw new ArgumentOutOfRangeException("Имя автора должно быть " +
                    "длинной больше 0 символов и не превышать длину в 50 символов!")
                : value;
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set => _surname = value.Length > 50 && value.Length <= 0
                ? throw new ArgumentOutOfRangeException("Фамилия автора должна быть " +
                    "длинной больше 0 символов и не превышать длину в 50 символов!")
                : value;
        }

        private string _patronymic;
        public string Patronymic
        {
            get => _patronymic;
            set => _patronymic = value == null
                    ? ""
                    : value.Length > 50
                    ? throw new ArgumentOutOfRangeException("Отчество автора должно быть " +
                        "длинной больше 0 символов и не превышать длину в 50 символов!")
                    : value;
        }

        public override string TableName => "author";

        public Author(string name, string surname, string patronymic)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
        }

        public Author(long? id, string name, string surname, string patronymic) : base(id)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
        }

        public Author() { }

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "name", Name },
                { "surname", Surname },
                { "patronymic", Patronymic } };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new Author(Convert.ToInt32(g["id"]), g["name"], g["surname"], g["patronymic"]);

                entities.Add(entity);
            }

            return entities;
        }
    }
}
