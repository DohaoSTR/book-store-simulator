using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class GiftCard : DataBaseEntity
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

        private int _faceValue;
        public int FaceValue
        {
            get => _faceValue;
            set => _faceValue = value < 0
                ? throw new ArgumentOutOfRangeException("Номинал карты не может быть " +
                    "меньше нуля!")
                : value;
        }
        public GiftCard(int registrationNumber, int faceValue)
        {
            RegistrationNumber = registrationNumber;
            FaceValue = faceValue;
        }

        public GiftCard(long? id, int registrationNumber, int faceValue) : base(id)
        {
            RegistrationNumber = registrationNumber;
            FaceValue = faceValue;
        }

        public GiftCard()
        {
        }

        public override string TableName => "gift_card";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "registration_number", RegistrationNumber },
                { "face_value", FaceValue }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new GiftCard(Convert.ToInt32(g["id"]), Convert.ToInt32(g["registration_number"]),
                    Convert.ToInt32(g["face_value"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
