using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class ISBN : DataBaseEntity
    {
        private int _EANUCC;
        public int EANUCC
        {
            get => _EANUCC;
            set => _EANUCC = value < 0 || value.ToString().Length > 3
                ? throw new ArgumentOutOfRangeException("EAN.UCC должен быть " +
                    "больше нуля и в нём не должно быть больше трёх символов!")
                : value;
        }

        private int _numberOfRegistrationGroup;
        public int NumberOfRegistrationGroup
        {
            get => _numberOfRegistrationGroup;
            set => _numberOfRegistrationGroup = value < 0 || value.ToString().Length != 1
                ? throw new ArgumentOutOfRangeException("Номер регистрационной группы должен быть " +
                    "больше нуля и состоять из одного числового символа!")
                : value;
        }

        private int _numberOfRegistrant;
        public int NumberOfRegistrant
        {
            get => _numberOfRegistrant;
            set => _numberOfRegistrant = value < 0
                ? throw new ArgumentOutOfRangeException("Номер регистранта должен быть " +
                    "больше нуля!")
                : value;
        }

        private int _numberOfEdition;
        public int NumberOfEdition
        {
            get => _numberOfEdition;
            set => _numberOfEdition = value < 0 || value.ToString().Length > 6
                ? throw new ArgumentOutOfRangeException("Номер издания должен быть " +
                    "больше нуля и состоять шести из символов!")
                : value;
        }

        private int _checkDigit;
        public int CheckDigit
        {
            get => _checkDigit;
            set => _checkDigit = value < 0 || value.ToString().Length != 1
                ? throw new ArgumentOutOfRangeException("Контрольная цифра должна быть " +
                    "больше нуля и состоять из одного символа!")
                : value;
        }

        public ISBN(int EANUCC, int numberOfRegistrationGroup, int numberOfRegistrant, int numberOfEdition, int checkDigit)
        {
            this.EANUCC = EANUCC;
            NumberOfRegistrationGroup = numberOfRegistrationGroup;
            NumberOfRegistrant = numberOfRegistrant;
            NumberOfEdition = numberOfEdition;
            CheckDigit = checkDigit;
        }

        public ISBN(long? id, int EANUCC, int numberOfRegistrationGroup,
            int numberOfRegistrant, int numberOfEdition, int checkDigit) : base(id)
        {
            this.EANUCC = EANUCC;
            NumberOfRegistrationGroup = numberOfRegistrationGroup;
            NumberOfRegistrant = numberOfRegistrant;
            NumberOfEdition = numberOfEdition;
            CheckDigit = checkDigit;
        }

        public ISBN() { }

        public override string TableName => "isbn";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "ean_ucc", EANUCC },
                { "number_of_registration_group", NumberOfRegistrationGroup },
                { "number_of_registrant", NumberOfRegistrant },
                { "number_of_edition", NumberOfEdition },
                { "check_digit", CheckDigit }};
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new ISBN(Convert.ToInt32(g["id"]), Convert.ToInt32(g["ean_ucc"]),
                    Convert.ToInt32(g["number_of_registration_group"]), Convert.ToInt32(g["number_of_registrant"]),
                    Convert.ToInt32(g["number_of_edition"]), Convert.ToInt32(g["check_digit"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
