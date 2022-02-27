using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class SanitaryEpidemiologicalCertificate : DataBaseEntity
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

        private string _consignor;
        public string Consignor
        {
            get => _consignor;
            set => _consignor = value.Length > 100 && value.Length <= 0
                ? throw new ArgumentOutOfRangeException("Имя заказчика должно быть " +
                    "длинной больше 0 символов и не превышать длину в 100 символов!")
                : value;
        }

        private string _customer;
        public string Customer
        {
            get => _customer;
            set => _customer = value.Length > 100 && value.Length <= 0
                ? throw new ArgumentOutOfRangeException("Имя клиента должно быть " +
                    "длинной больше 0 символов и не превышать длину в 100 символов!")
                : value;
        }

        public DateTime RegistrationDate { get; set; }

        public SanitaryEpidemiologicalCertificate(int registrationNumber, string consignor, string customer, DateTime registrationDate)
        {
            RegistrationNumber = registrationNumber;
            Consignor = consignor;
            Customer = customer;
            RegistrationDate = registrationDate;
        }

        public SanitaryEpidemiologicalCertificate(long? id, int registrationNumber, string consignor,
            string customer, DateTime registrationDate) : base(id)
        {
            RegistrationNumber = registrationNumber;
            Consignor = consignor;
            Customer = customer;
            RegistrationDate = registrationDate;
        }

        public SanitaryEpidemiologicalCertificate() { }

        public override string TableName => "sanitary_epidemiological_certificate";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "registration_number", RegistrationNumber },
                { "consignor", Consignor },
                { "customer", Customer },
                { "registration_date", RegistrationDate.ToString("yyyy-MM-dd") }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new SanitaryEpidemiologicalCertificate(Convert.ToInt32(g["id"]),
                    Convert.ToInt32(g["registration_number"]), g["consignor"], g["customer"], Convert.ToDateTime(g["registration_date"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
