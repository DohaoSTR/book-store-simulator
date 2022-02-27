using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class SchoolBook : DataBaseEntity
    {
        public PrintedMatter PrintedMatter { get; set; }

        public ConformityDeclaration ConformityDeclaration { get; set; }

        public StateRegistrationCertificate StateRegistrationCertificate { get; set; }

        public SanitaryEpidemiologicalCertificate SanitaryEpidemiologicalCertificate { get; set; }

        public SchoolBook(int printedMatter, int conformityDeclaration,
           int stateRegistrationCertificate, int sanitaryEpidemiologicalCertificate)
        {
            PrintedMatter = (PrintedMatter)GetEntity(printedMatter, new PrintedMatter());
            ConformityDeclaration = (ConformityDeclaration)GetEntity(conformityDeclaration, new ConformityDeclaration());
            StateRegistrationCertificate = (StateRegistrationCertificate)GetEntity(stateRegistrationCertificate, new StateRegistrationCertificate());
            SanitaryEpidemiologicalCertificate = (SanitaryEpidemiologicalCertificate)GetEntity(sanitaryEpidemiologicalCertificate, new SanitaryEpidemiologicalCertificate());
        }

        public SchoolBook(long? id, int printedMatter, int conformityDeclaration,
            int stateRegistrationCertificate, int sanitaryEpidemiologicalCertificate)
            : base(id)
        {
            PrintedMatter = (PrintedMatter)GetEntity(printedMatter, new PrintedMatter());
            ConformityDeclaration = (ConformityDeclaration)GetEntity(conformityDeclaration, new ConformityDeclaration());
            StateRegistrationCertificate = (StateRegistrationCertificate)GetEntity(stateRegistrationCertificate, new StateRegistrationCertificate());
            SanitaryEpidemiologicalCertificate = (SanitaryEpidemiologicalCertificate)GetEntity(sanitaryEpidemiologicalCertificate, new SanitaryEpidemiologicalCertificate());
        }

        public SchoolBook() { }

        public override string TableName => "school_book";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "id_printed_matter", PrintedMatter.Id },
                { "id_conformity_declaration", ConformityDeclaration.Id },
                { "id_state_registration_certificate", StateRegistrationCertificate.Id },
                { "id_sanitary_epidemiological_certificate", SanitaryEpidemiologicalCertificate.Id }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new SchoolBook(Convert.ToInt32(g["id"]),
                    Convert.ToInt32(g["id_printed_matter"]),
                    Convert.ToInt32(g["id_conformity_declaration"]),
                    Convert.ToInt32(g["id_state_registration_certificate"]),
                    Convert.ToInt32(g["id_sanitary_epidemiological_certificate"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
