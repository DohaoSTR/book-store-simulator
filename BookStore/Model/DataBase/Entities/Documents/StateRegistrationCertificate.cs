using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class StateRegistrationCertificate : DataBaseEntity
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

        private string _codeTransportUnion;
        public string CodeTransportUnion
        {
            get => _codeTransportUnion;
            set => _codeTransportUnion = value.Length > 10 || value.Length <= 0
                ? throw new ArgumentOutOfRangeException("ТН ВЭД должен быть " +
                    "меньше 10 символов и больше 0!")
                : value;
        }

        public DateTime RegistrationDate { get; set; }

        public StateRegistrationCertificate(int registrationNumber, string codeTransportUnion, DateTime registrationDate)
        {
            RegistrationNumber = registrationNumber;
            CodeTransportUnion = codeTransportUnion;
            RegistrationDate = registrationDate;
        }

        public StateRegistrationCertificate(long? id, int registrationNumber,
            string codeTransportUnion, DateTime registrationDate) : base(id)
        {
            RegistrationNumber = registrationNumber;
            CodeTransportUnion = codeTransportUnion;
            RegistrationDate = registrationDate;
        }

        public StateRegistrationCertificate() { }

        public override string TableName => "state_registration_certificate";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "registration_number", RegistrationNumber },
                { "code_transport_union", CodeTransportUnion },
                { "registration_date", RegistrationDate.ToString("yyyy-MM-dd") }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new StateRegistrationCertificate(Convert.ToInt32(g["id"]),
                    Convert.ToInt32(g["registration_number"]), g["code_transport_union"], Convert.ToDateTime(g["registration_date"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
