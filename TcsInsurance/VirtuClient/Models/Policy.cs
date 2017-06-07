using System;

namespace VirtuClient.Models
{
    public class Policy
    {
        public string ProductID { get; set; }
        public string DocumentDate { get; set; }
        public string AcceptationDate { get; set; }
        public string EffectiveDate { get; set; }
        public string ExpirationDate { get; set; }
        public string InsuredName { get; set; }
        public string AmountCurrencyName { get; set; }
        public string AmountCurrencyCode { get; set; }
        public string SumInsuredCurrencyCode { get; set; }
        public string CreatorUser { get; set; }
        public string CreatorName { get; set; }
        public string InsurerRepresentId { get; set; }
        public string SallerDivisionID { get; set; }
        public string SallerDivision { get; set; }
        public string InsurerRepresentName { get; set; }
        public string InvestmentStrategy { get; set; }
        public string InvestmentStrategyRaw { get; set; }
        public Investmentstrategydata InvestmentStrategyData { get; set; }
        public string StrategyCurrency { get; set; }
        public string StrategyCurrencyRaw { get; set; }
        public string InsurancePeriod { get; set; }
        public string InsurancePeriodRaw { get; set; }
        public int ParticipationCoefficient { get; set; }
        public string InsuranceSum { get; set; }
        public string ManualInsuranceSum { get; set; }
        public string ContributionsFrequency { get; set; }
        public int Premium { get; set; }
        public string Currency { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Sex { get; set; }
        public string INN { get; set; }
        public string Cityzenship { get; set; }
        public string InsuranceSumType { get; set; }
        public string OtherCityzenship { get; set; }
        public string Resident { get; set; }
        public string StayingDocumentType { get; set; }
        public string StayingDocumentTypeRaw { get; set; }
        public string StayingDocumentSerial { get; set; }
        public string StayingDocumentNumber { get; set; }
        public object StayingDocumentDate { get; set; }
        public string StayingDocumentOrganisation { get; set; }
        public object StayingDocumentEndDate { get; set; }
        public string MigrationCardSerial { get; set; }
        public string MigrationCardNumber { get; set; }
        public object MigrationCardDate { get; set; }
        public object StartStayingDate { get; set; }
        public object EndStayingDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AddressInputType { get; set; }
        public Address Address { get; set; }
        public string AddressText { get; set; }
        public bool AgreeForSpam { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeRaw { get; set; }
        public string PassportSerial { get; set; }
        public string PassportNumber { get; set; }
        public string PassportDate { get; set; }
        public string PassportOrganisation { get; set; }
        public string PassportCode { get; set; }
        public string ActInOwnInterests { get; set; }
        public string InsuredActInOwnInterests { get; set; }
        public string ActInPublicFaceInterest { get; set; }
        public string IsCreatorOfPublicOrganisation { get; set; }
        public string IsResidentOfEconomicZone { get; set; }
        public string IsPublicFace { get; set; }
        public bool InsuredIsInsurer { get; set; }
        public string InsuredLastName { get; set; }
        public string InsuredFirstName { get; set; }
        public string InsuredPatronymic { get; set; }
        public string InsuredBirthDate { get; set; }
        public string InsuredBirthPlace { get; set; }
        public string InsuredSex { get; set; }
        public string InsuredINN { get; set; }
        public string InsuredCityzenship { get; set; }
        public string InsuredOtherCityzenship { get; set; }
        public string InsuredResident { get; set; }
        public string InsuredStayingDocumentType { get; set; }
        public string InsuredStayingDocumentTypeRaw { get; set; }
        public string InsuredStayingDocumentSerial { get; set; }
        public string InsuredStayingDocumentNumber { get; set; }
        public object InsuredStayingDocumentDate { get; set; }
        public object InsuredStayingDocumentEndDate { get; set; }
        public string InsuredStayingDocumentOrganisation { get; set; }
        public string InsuredMigrationCardSerial { get; set; }
        public string InsuredMigrationCardNumber { get; set; }
        public object InsuredMigrationCardDate { get; set; }
        public object InsuredStartStayingDate { get; set; }
        public object InsuredEndStayingDate { get; set; }
        public string InsuredPhone { get; set; }
        public string InsuredEmail { get; set; }
        public string InsuredAddressInputType { get; set; }
        public Insuredaddress InsuredAddress { get; set; }
        public string InsuredAddressText { get; set; }
        public bool InsuredAgreeForSpam { get; set; }
        public string InsuredDocumentType { get; set; }
        public string InsuredDocumentTypeRaw { get; set; }
        public string InsuredPassportSerial { get; set; }
        public string InsuredPassportNumber { get; set; }
        public string InsuredPassportDate { get; set; }
        public string InsuredPassportOrganisation { get; set; }
        public string InsuredPassportCode { get; set; }
        public string InsuredActInPublicFaceInterest { get; set; }
        public string InsuredIsCreatorOfPublicOrganisation { get; set; }
        public string InsuredIsResidentOfEconomicZone { get; set; }
        public string InsuredIsPublicFace { get; set; }
        public string IsSuccessor { get; set; }
        public object[] Beneficiaries { get; set; }
        public string PaymentType { get; set; }
        public string PaymentDocumentDate { get; set; }
        public string SellerComment { get; set; }
        public string ReceiptDate { get; set; }
        public int ReceiptSum { get; set; }
        public string SalesChannel { get; set; }
        public Bayout[] Bayout { get; set; }
        public string UserTown { get; set; }
        public string KvPartner1Percent { get; set; }
        public string KvPartner1Rub { get; set; }
        public string KvPartner2Percent { get; set; }
        public string KvPartner2Rub { get; set; }
        public string Partner1Name { get; set; }
        public string Partner2Name { get; set; }
        public string CheafName { get; set; }
        public string SellerDivisionHierarchyInfo { get; set; }
        public bool ScanWasSent { get; set; }
        public string SalesPartner { get; set; }
        public string SalesPartner2 { get; set; }
        public string IsForeignTaxpayer { get; set; }
        public string StatusID { get; set; }
        public string DocumentStatusID { get; set; }
        public string SERIAL { get; set; }
        public string NUMBER { get; set; }
        public string url { get; set; }
        public string ID { get; set; }
        public object FilialName { get; set; }
        public string StatusName { get; set; }
        public string NUMBAR { get; set; }
        public string ProductName { get; set; }
    }

    public class Investmentstrategydata
    {
        public string BaseIndex { get; set; }
        public int BaseIndexOnStartDate { get; set; }
        public int Coefficient { get; set; }
        public int CurrencyRate { get; set; }
        public object Date1 { get; set; }
        public object Date2 { get; set; }
        public object Date3 { get; set; }
        public object Date4 { get; set; }
        public string ExpirationDate { get; set; }
        public float FIPart { get; set; }
        public float GFPart { get; set; }
        public string ID { get; set; }
        public int InsuranceCurrencyRate { get; set; }
        public string InvestmentStartDate { get; set; }
        public string InvestmentStrategy { get; set; }
        public string InvestmentStrategyRaw { get; set; }
        public bool IsActive { get; set; }
        public string OptionKind { get; set; }
        public string OptionID { get; set; }
        public string OptionPeriod { get; set; }
        public string OptionCurrency { get; set; }
        public string OptionCurrencyRaw { get; set; }
        public int OptionPrice { get; set; }
        public int OptionPriceRUR { get; set; }
        public string OptionType { get; set; }
        public string OptionTypeRaw { get; set; }
        public int OptionValue { get; set; }
        public int OptionValueRUR { get; set; }
        public int Profitability { get; set; }
        public object SellingEndDate { get; set; }
        public object SellingStartDate { get; set; }
        public string VersionCode { get; set; }
        public int RateOfReturn { get; set; }
        public bool isNotBought { get; set; }
        public int FIPartRUR { get; set; }
        public int GFPartRUR { get; set; }
    }

    public class Address
    {
        public string KLADRCode { get; set; }
    }

    public class Insuredaddress
    {
        public string KLADRCode { get; set; }
    }

    public class Bayout
    {
        public string InsPeriod { get; set; }
        public string InsSum { get; set; }
    }

}