using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class GoodTransportWaybill : DataBaseEntity
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
                ? throw new ArgumentOutOfRangeException("Имя товароотправителя должно быть " +
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

        private string _loadingPoint;
        public string LoadingPoint
        {
            get => _loadingPoint;
            set => _loadingPoint = value.Length <= 0
                ? throw new ArgumentOutOfRangeException("Название точки отправления должно быть " +
                    "длинной больше 0 символов!")
                : value;
        }

        private string _shippingPoint;
        public string ShippingPoint
        {
            get => _shippingPoint;
            set => _shippingPoint = value.Length <= 0
                ? throw new ArgumentOutOfRangeException("Название точки прибытия должно быть " +
                    "длинной больше 0 символов!")
                : value;
        }

        public GoodTransportWaybill(int registrationNumber, string consignor, string customer, string loadingPoint, string shippingPoint)
        {
            RegistrationNumber = registrationNumber;
            Consignor = consignor;
            Customer = customer;
            LoadingPoint = loadingPoint;
            ShippingPoint = shippingPoint;
        }

        public GoodTransportWaybill(long? id, int registrationNumber, string consignor,
            string customer, string loadingPoint, string shippingPoint) : base(id)
        {
            RegistrationNumber = registrationNumber;
            Consignor = consignor;
            Customer = customer;
            LoadingPoint = loadingPoint;
            ShippingPoint = shippingPoint;
        }

        public GoodTransportWaybill() { }

        public override string TableName => "good_transport_waybill";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "registration_number", RegistrationNumber },
                { "consignor", Consignor },
                { "customer", Customer },
                { "loading_point", LoadingPoint },
                { "shipping_point", ShippingPoint }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new GoodTransportWaybill(Convert.ToInt32(g["id"]), Convert.ToInt32(g["registration_number"]),
                    g["consignor"], g["customer"], g["loading_point"], g["shipping_point"]);

                entities.Add(entity);
            }

            return entities;
        }
    }
}
