using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class DiscountCard : DataBaseEntity
    {
        private int _registrationNumber;
        public int RegistrationNumber
        {
            get => _registrationNumber;
            set => _registrationNumber = value < 0
                ? throw new ArgumentOutOfRangeException("Регистрационный номер должен быть " +
                    "больше нуля!")
                : value;
        }

        private int _bonusValue;
        public int BonusValue
        {
            get => _bonusValue;
            set => _bonusValue = value < 0
                ? throw new ArgumentOutOfRangeException("Кол-во бонусов не может быть " +
                    "меньше нуля!")
                : value;
        }

        public DiscountCard(int registrationNumber, int bonusValue)
        {
            RegistrationNumber = registrationNumber;
            BonusValue = bonusValue;
        }

        public DiscountCard(long? id, int registrationNumber, int bonusValue) : base(id)
        {
            RegistrationNumber = registrationNumber;
            BonusValue = bonusValue;
        }

        public DiscountCard()
        {
        }

        public override string TableName => "discount_card";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "registration_number", RegistrationNumber },
                { "bonus_value", BonusValue }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new DiscountCard(Convert.ToInt32(g["id"]), Convert.ToInt32(g["registration_number"]),
                    Convert.ToInt32(g["bonus_value"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
