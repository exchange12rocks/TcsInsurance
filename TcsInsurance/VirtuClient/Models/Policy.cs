namespace VirtuClient.Models
{
    public class Policy
    {
        public string ProductID { get; set; }
        /// <summary>
        /// Дата выписки полиса
        /// </summary>
        public string DocumentDate { get; set; }
        /// <summary>
        /// Дата акцептации
        /// </summary>
        public string AcceptationDate { get; set; }
        /// <summary>
        /// Дата начала срока страхования
        /// </summary>
        public string EffectiveDate { get; set; }
        /// <summary>
        /// Дата окончания срока страхования
        /// </summary>
        public string ExpirationDate { get; set; }
        /// <summary>
        /// Имя страхователя
        /// </summary>
        public string InsuredName { get; set; }
        /// <summary>
        /// создатель полиса
        /// </summary>
        public string CreatorUser { get; set; }
        /// <summary>
        /// имя создателя полиса
        /// </summary>
        public string CreatorName { get; set; }
        /// <summary>
        /// ID продавца
        /// </summary>
        public string InsurerRepresentId { get; set; }
        /// <summary>
        /// ID подразделения продавца
        /// </summary>
        public string SallerDivisionID { get; set; }
        /// <summary>
        /// подразделение продавца
        /// </summary>
        public string SallerDivision { get; set; }
        /// <summary>
        /// имя продавца
        /// </summary>
        public string InsurerRepresentName { get; set; }
        /// <summary>
        /// ID стратегии
        /// </summary>
        public string InvestmentStrategy { get; set; }
        /// <summary>
        /// наименование стратегии
        /// </summary>
        public string InvestmentStrategyRaw { get; set; }
        /// <summary>
        /// объект опциона
        /// </summary>
        public Investmentstrategydata InvestmentStrategyData { get; set; }
        /// <summary>
        /// валюта стратегии
        /// </summary>
        public string StrategyCurrency { get; set; }
        /// <summary>
        /// наименование валюты стратегии
        /// </summary>
        public string StrategyCurrencyRaw { get; set; }
        /// <summary>
        /// ID периода страхования
        /// </summary>
        public string InsurancePeriod { get; set; }
        /// <summary>
        /// наименование периода страхования
        /// </summary>
        public string InsurancePeriodRaw { get; set; }
        /// <summary>
        /// коэффициент участия
        /// </summary>
        public decimal ParticipationCoefficient { get; set; }
        /// <summary>
        /// ID страховой суммы
        /// </summary>
        public string InsuranceSum { get; set; }
        /// <summary>
        /// частота выплаты
        /// </summary>
        public string ContributionsFrequency { get; set; }
        /// <summary>
        /// сумма страховой премии
        /// </summary>
        public decimal Premium { get; set; }
        /// <summary>
        /// ID валюты
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// фамилия страхователя
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// имя страхователя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// отчество страхователя
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// дата рождения страхователя
        /// </summary>
        public string BirthDate { get; set; }
        /// <summary>
        /// место рождения страхователя
        /// </summary>
        public string BirthPlace { get; set; }
        /// <summary>
        /// пол страхователя
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// ИНН страхователя
        /// </summary>
        public string INN { get; set; }
        /// <summary>
        /// гражданство РФ
        /// </summary>
        public string Cityzenship { get; set; }
        /// <summary>
        /// иное гражданство
        /// </summary>
        public string OtherCityzenship { get; set; }
        /// <summary>
        /// резидент РФ
        /// </summary>
        /// 

        //Документ, подтверждающий право на пребывание в РФ:
        public string Resident { get; set; }
        /// <summary>
        /// вид документа
        /// </summary>
        public string StayingDocumentType { get; set; }
        /// <summary>
        /// наименование документа
        /// </summary>
        public string StayingDocumentTypeRaw { get; set; }
        /// <summary>
        /// серия
        /// </summary>
        public string StayingDocumentSerial { get; set; }
        /// <summary>
        /// номер
        /// </summary>
        public string StayingDocumentNumber { get; set; }
        /// <summary>
        /// дата выдачи
        /// </summary>
        public string StayingDocumentDate { get; set; }
        public string StayingDocumentOrganisation { get; set; }
        public string StayingDocumentEndDate { get; set; }
        public string MigrationCardSerial { get; set; }
        public string MigrationCardNumber { get; set; }
        public string MigrationCardDate { get; set; }
        public string StartStayingDate { get; set; }
        public string EndStayingDate { get; set; }
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
        public string InsuredCityzenship { get; set; }
        public string InsuredOtherCityzenship { get; set; }
        public string InsuredResident { get; set; }
        public string InsuredStayingDocumentType { get; set; }
        public string InsuredStayingDocumentTypeRaw { get; set; }
        public string InsuredStayingDocumentSerial { get; set; }
        public string InsuredStayingDocumentNumber { get; set; }
        public string InsuredStayingDocumentDate { get; set; }
        public string InsuredStayingDocumentEndDate { get; set; }
        public string InsuredStayingDocumentOrganisation { get; set; }
        public string InsuredMigrationCardSerial { get; set; }
        public string InsuredMigrationCardNumber { get; set; }
        public string InsuredMigrationCardDate { get; set; }
        public string InsuredStartStayingDate { get; set; }
        public string InsuredEndStayingDate { get; set; }
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
        public string ID { get; set; }
        public string StatusID { get; set; }
        public string DocumentStatusID { get; set; }
        public string SERIAL { get; set; }
        public string NUMBER { get; set; }
        public string url { get; set; }
        public string StatusName { get; set; }
        public string NUMBAR { get; set; }
        public string ProductName { get; set; }
    }

    public class Investmentstrategydata
    {
        public string BaseIndex { get; set; }
        public decimal BaseIndexOnStartDate { get; set; }
        public int Coefficient { get; set; }
        public decimal CurrencyRate { get; set; }
        public string Date1 { get; set; }
        public string Date2 { get; set; }
        public string Date3 { get; set; }
        public string Date4 { get; set; }
        public string ExpirationDate { get; set; }
        public decimal FIPart { get; set; }
        public decimal GFPart { get; set; }
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
        public decimal OptionPriceRUR { get; set; }
        public string OptionType { get; set; }
        public string OptionTypeRaw { get; set; }
        public int OptionValue { get; set; }
        public int OptionValueRUR { get; set; }
        public decimal Profitability { get; set; }
        public string SellingEndDate { get; set; }
        public string SellingStartDate { get; set; }
        public string VersionCode { get; set; }
        public decimal RateOfReturn { get; set; }
        public decimal FIPartRUR { get; set; }
        public decimal GFPartRUR { get; set; }
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
