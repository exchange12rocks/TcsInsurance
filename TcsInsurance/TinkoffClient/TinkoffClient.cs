using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using tinkoff.ru.partners.insurance.investing.types;
using VirtuClient.Models;
namespace TinkoffClient
{
    public static class PropertyHelper
    {
        public static string GetPropertyName<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }
            return body.Member.Name;
        }
    }
    public static class EnumerableExtensions
    {
        public static T Single<T>(this IEnumerable<T> enumerable, Expression<Func<T, string>> predicator, string key)
        {
            Func<T, string> predicatorFunc = predicator.Compile();
            var values = enumerable.Where(A => string.Equals(predicatorFunc(A), key, StringComparison.OrdinalIgnoreCase));
            if (values.Count() == 0)
            {
                throw new Exception($"Не найдено записей {typeof(T).Name} удовлетворяющих условию: {PropertyHelper.GetPropertyName(predicator)}={key}. Возможные значения: {string.Join(", ", enumerable.Select(predicatorFunc))}");
            }
            if (values.Count() > 1)
            {
                throw new Exception($"Найдено {values.Count()} записей {typeof(T).Name} удовлетворяющих условию: {PropertyHelper.GetPropertyName(predicator)}={key}. Возможные значения: {string.Join(", ", enumerable.Select(predicatorFunc))}");
            }
            return values.Single();
        }
        public static T First<T>(this IEnumerable<T> enumerable, Expression<Func<T, string>> predicator, string key)
        {
            Func<T, string> predicatorFunc = predicator.Compile();
            var values = enumerable.Where(A => string.Equals(predicatorFunc(A), key, StringComparison.OrdinalIgnoreCase));
            if (values.Count() == 0)
            {
                throw new Exception($"Не найдено записей {typeof(T).Name} удовлетворяющих условию: {PropertyHelper.GetPropertyName(predicator)}={key}. Возможные значения: {string.Join(", ", enumerable.Select(predicatorFunc))}");
            }
            return values.First();
        }
        public static T First<T, TValue>(this IEnumerable<T> enumerable, Expression<Func<T, TValue>> predicator, TValue key)
        {
            Func<T, TValue> predicatorFunc = predicator.Compile();
            var values = enumerable.Where(A => key.Equals(predicatorFunc(A)));
            if (values.Count() == 0)
            {
                throw new Exception($"Не найдено записей {typeof(T).Name} удовлетворяющих условию: {PropertyHelper.GetPropertyName(predicator)}={key}. Возможные значения: {string.Join(", ", enumerable.Select(predicatorFunc))}");
            }
            return values.First();
        }
        public static int Min<T>(this IEnumerable<T> enumerable, Expression<Func<T, string>> predicator, Func<string, int> converter)
        {
            Func<T, string> predicatorFunc = predicator.Compile();
            if (enumerable.Count() == 0)
            {
                throw new Exception($"Не найдено записей {typeof(T).Name} для вычисления Min");
            }
            return enumerable.Min(A => converter(predicatorFunc(A)));
        }
        public static int Max<T>(this IEnumerable<T> enumerable, Expression<Func<T, string>> predicator, Func<string, int> converter)
        {
            Func<T, string> predicatorFunc = predicator.Compile();
            if (enumerable.Count() == 0)
            {
                throw new Exception($"Не найдено записей {typeof(T).Name} для вычисления Max");
            }
            return enumerable.Min(A => converter(predicatorFunc(A)));
        }
    }
    public class TinkoffClient
    {
        private static IEnumerable<TResult> CrossJoin<T1, T2, TResult>(IEnumerable<T1> enumerable1, IEnumerable<T2> enumerable2, Func<T1, T2, TResult> func)
        {
            foreach (T1 t1 in enumerable1)
            {
                foreach (T2 t2 in enumerable2)
                {
                    yield return func(t1, t2);
                }
            }
        }
        private static string getDate(DateTime? value)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                return value.Value.ToString("yyyy-MM-dd");
            }
        }
        private static DateTime? getDate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                return DateTime.ParseExact(value, "yyyy-MM-dd", null);
            }
        }
        private static GetClassifierOutput getCurrency(currency? value, IEnumerable<GetClassifierOutput> currencies)
        {
            if (value == null)
            {
                return null;
            }
            else if (value.Value == currency.RUR)
            {
                return currencies.Single(A => string.Equals(A.Name, "РУБЛЬ", StringComparison.OrdinalIgnoreCase));
            }
            else if (value.Value == currency.USD)
            {
                return currencies.Single(A => string.Equals(A.Name, "ДОЛЛАР США", StringComparison.OrdinalIgnoreCase));
            }
            else if (value.Value == currency.EUR)
            {
                return currencies.Single(A => string.Equals(A.Name, "ЕВРО", StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                throw new ArgumentException("unknown currency value");
            }
        }
        private static currency? getCurrency(GetClassifierOutput value)
        {
            if (value == null)
            {
                return null;
            }
            else if (string.Equals(value.Name, "РУБЛЬ", StringComparison.OrdinalIgnoreCase))
            {
                return currency.RUR;
            }
            else if (string.Equals(value.Name, "ДОЛЛАР США", StringComparison.OrdinalIgnoreCase))
            {
                return currency.USD;
            }
            else if (string.Equals(value.Name, "ЕВРО", StringComparison.OrdinalIgnoreCase))
            {
                return currency.EUR;
            }
            else
            {
                throw new ArgumentException("unknown currency value");
            }
        }
        private static string getSex(sex value)
        {
            if (value == sex.male)
            {
                return "1";
            }
            else if (value == sex.female)
            {
                return "2";
            }
            else
            {
                throw new ArgumentException("unknown sex value");
            }
        }
        private static sex getSex(string value)
        {
            if (string.Equals(value, "1", StringComparison.OrdinalIgnoreCase))
            {
                return sex.male;
            }
            else if (string.Equals(value, "2", StringComparison.OrdinalIgnoreCase))
            {
                return sex.female;
            }
            else
            {
                throw new ArgumentException("unknown sex value");
            }
        }
        private static string getFullName(person person)
        {
            return $"{person.lastName ?? ""} {person.firstName ?? ""} {person.patronymicName ?? ""}".Trim();
        }
        private static string getFullAddress(address address)
        {
            List<string> result = new List<string>();
            if (!string.IsNullOrEmpty(address.index))
            {
                result.Add(address.index);
            }
            if (!string.IsNullOrEmpty(address.country))
            {
                result.Add(address.country);
            }
            if (!string.IsNullOrEmpty(address.region))
            {
                result.Add(address.region);
            }
            if (!string.IsNullOrEmpty(address.district))
            {
                result.Add(address.district);
            }
            if (!string.IsNullOrEmpty(address.city))
            {
                result.Add(address.city);
            }
            if (!string.IsNullOrEmpty(address.locality))
            {
                result.Add(address.locality);
            }
            if (!string.IsNullOrEmpty(address.street))
            {
                result.Add(address.street);
            }
            if (!string.IsNullOrEmpty(address.house))
            {
                if (string.IsNullOrEmpty(address.building))
                {
                    result.Add(address.house);
                }
                else
                {
                    result.Add(address.house + " " + address.building);
                }
            }
            if (!string.IsNullOrEmpty(address.flat))
            {
                result.Add(address.flat);
            }
            return string.Join(", ", result);
        }
        private static string getExtension(string transformFile)
        {
            string extension = Path.GetExtension(transformFile);
            if (string.Equals(extension, ".mrt", StringComparison.OrdinalIgnoreCase))
            {
                return ".pdf";
            }
            else
            {
                return extension;
            }
        }
        private static string getContentType(string extension)
        {
            if (string.Equals(extension, ".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return "application/pdf";
            }
            else if (string.Equals(extension, ".doc", StringComparison.OrdinalIgnoreCase))
            {
                return "application/msword";
            }
            else if (string.Equals(extension, ".docx", StringComparison.OrdinalIgnoreCase))
            {
                return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            }
            else
            {
                return null;
            }
        }
        private static string getPrintformCaption(policyDocumentType documentType)
        {
            if (documentType == policyDocumentType.Item1 || documentType == policyDocumentType.Item2)
            {
                return "Полис";
            }
            else if (documentType == policyDocumentType.Item)
            {
                return "Правила и согласие на обработку персональных данных";
            }
            else
            {
                throw new Exception($"неизвестный тип документа documentType: {documentType.ToString()}");
            }
        }

        private VirtuClient.VirtuClient virtuClient;
        public TinkoffClient(VirtuClient.VirtuClient virtuClient)
        {
            this.virtuClient = virtuClient;
        }

        public getProductsResponse GetProducts(GetProductsRequest parameter)
        {
            var product = this.virtuClient.GetProducts().Single(A => A.Name, "Верное решение");
            var risks = this.virtuClient.GetRisks(product.ID);
            var insuranceSums = this.virtuClient.GetInsuranceSums(product.ID);
            var insurancePeriods = this.virtuClient.GetInsurancePeriods(product.ID);
            var currencies = this.virtuClient.GetCurrencies(product.ID);
            var getBuyoutTariffs = this.virtuClient.GetTariffs(product.ID);
            var strategyDetails = this.virtuClient.StrategiesSearch(new StrategiesSearchInput()
            {
                IsActive = true,
                ReadRedefined = true,
            });
            return new getProductsResponse()
            {
                GetProductsResponse1 = new product[]
                {
                    new product()
                    {
                        id = product.ID,
                        name = product.Name,
                        shortDescription = product.Description,
                        fullDescription = product.Description,
                        currency = currency.RUR,
                        minPremiums = insuranceSums.Min(A => A.Name, int.Parse).ToString(),
                        maxPremiums = insuranceSums.Max(A => A.Name, int.Parse).ToString(),
                        insuranceRisks = CrossJoin(risks, insuranceSums.Select(A => A.Name).Distinct(), (risk, insuranceSum) => new risk()
                        {
                            id = risk.ID,
                            text = risk.Name,
                            sum = insuranceSum,
                        }).ToArray(),
                        policyTermOptions = insurancePeriods.Select(A => A.Name).ToArray(),
                        redemptionAmounts = getBuyoutTariffs.Select(A => new redemptionAmount()
                        {
                            sum = decimal.Parse(A.InsSum.value),
                            year = A.Year.value,
                            policyTerm = insurancePeriods.Single(B => B.ID, A.InsPeriod.value).Name,
                            currency = currency.RUR,
                            contributionsPeriodicity = periodicity.Item0,
                        }).ToArray(),
                        contributionsPeriodicityOptions = new periodicity[]
                        {
                            periodicity.Item0,
                        },
                        investParams = strategyDetails
                            .Select(strategyDetail => new investParam()
                            {
                                strategyId = strategyDetail.ID,
                                strategy = strategyDetail.InvestmentStrategyRaw,
                                coefficient = strategyDetail.Coefficient.Value,
                                contributionsPeriodicity = periodicity.Item0,
                                currencyInvest = getCurrency(currencies.Single(A => A.ID, strategyDetail.OptionCurrency)).Value,
                                policyTerm = insurancePeriods.Single(A => A.ID, strategyDetail.OptionPeriod).Name,
                                paymentsPeriodicitiesSpecified = true,
                                paymentsPeriodicities = periodicity.Item0,
                                coverCapitalSpecified = true,
                                coverCapital = 100,
                            }).ToArray(),
                    }
                }
            };
        }
        public CreatePolicyResponse CreatePolicy(CreatePolicyRequest parameter)
        {
            var product = this.virtuClient.GetProducts().Single(A => A.Name, "Верное решение");
            var risks = this.virtuClient.GetRisks(product.ID);
            var insuranceSums = this.virtuClient.GetInsuranceSums(product.ID);
            var insurancePeriods = this.virtuClient.GetInsurancePeriods(product.ID);
            var currencies = this.virtuClient.GetCurrencies(product.ID);
            var getBuyoutTariffs = this.virtuClient.GetTariffs(product.ID);
            var strategyDetails = this.virtuClient.StrategiesSearch(new StrategiesSearchInput()
            {
                IsActive = true,
                ReadRedefined = true,
            });
            var documentTypes = this.virtuClient.GetDocumentTypes(product.ID);
            var insuredDocumentTypes = this.virtuClient.InsuredDocumentTypes(product.ID);
            var calculate = this.virtuClient.Calculate(new CalculateInput()
            {
                ProductID = product.ID,
                Premium = parameter.amount.ToString(),
            });
            var status = this.virtuClient.GetStatuses(product.ID).Single(A => A.Name, "Проект");

            DateTime today = DateTime.Today;
            DateTime effectiveDate = today;
            int periodInYears = int.Parse(parameter.policyTerm);
            var insured = parameter.insurantIsInsured ? parameter.insurant : parameter.insured;

            var strategyDetail = strategyDetails.Single(A => A.ID, parameter.strategyId);
            var strategyCurrency = currencies.Single(A => A.ID, strategyDetail.OptionCurrency);
            var currency = getCurrency(parameter.currency, currencies);
            var insurancePeriod = insurancePeriods.Single(A => A.ID, strategyDetail.OptionPeriod);
            var insuranceSum = insuranceSums.FirstOrDefault(A => decimal.Parse(A.Name) == parameter.amount);
            var documentType = documentTypes.Single(A => A.Name, "Паспорт гражданина Российской Федерации");
            var policy = this.virtuClient.Save(new Policy()
            {
                ProductID = parameter.productId,
                DocumentDate = getDate(today),
                EffectiveDate = getDate(effectiveDate),
                ExpirationDate = getDate(effectiveDate.AddYears(periodInYears)),
                InsuredName = getFullName(insured),
                AmountCurrencyName = "",
                AmountCurrencyCode = "",
                SumInsuredCurrencyCode = "",
                //CreatorUser = null, //Хз
                //CreatorName = null, //хз
                //InsurerRepresentId = null, //Хз
                //SallerDivisionID = null, //Хз
                //SallerDivision = "\"Электронный\"",
                //InsurerRepresentName = null, //Хз
                InvestmentStrategy = strategyDetail.InvestmentStrategy,
                InvestmentStrategyRaw = strategyDetail.InvestmentStrategyRaw,
                InvestmentStrategyData = new Investmentstrategydata()
                {
                    BaseIndex = strategyDetail.BaseIndex,
                    BaseIndexOnStartDate = strategyDetail.BaseIndexOnStartDate,
                    Coefficient = strategyDetail.Coefficient,
                    CurrencyRate = strategyDetail.CurrencyRate,
                    ExpirationDate = strategyDetail.ExpirationDate,
                    FIPart = strategyDetail.FIPart,
                    GFPart = strategyDetail.GFPart,
                    ID = strategyDetail.ID,
                    InsuranceCurrencyRate = strategyDetail.InsuranceCurrencyRate,
                    InvestmentStartDate = strategyDetail.InvestmentStartDate,
                    InvestmentStrategy = strategyDetail.InvestmentStrategy,
                    InvestmentStrategyRaw = strategyDetail.InvestmentStrategyRaw,
                    IsActive = strategyDetail.IsActive,
                    OptionKind = strategyDetail.OptionKind,
                    OptionID = strategyDetail.OptionID,
                    OptionPeriod = strategyDetail.OptionPeriod,
                    OptionCurrency = strategyDetail.OptionCurrency,
                    OptionCurrencyRaw = strategyDetail.OptionCurrencyRaw,
                    OptionPrice = strategyDetail.OptionPrice,
                    OptionPriceRUR = strategyDetail.OptionPriceRUR,
                    OptionType = strategyDetail.OptionType,
                    OptionTypeRaw = strategyDetail.OptionTypeRaw,
                    OptionValue = strategyDetail.OptionValue,
                    OptionValueRUR = strategyDetail.OptionValueRUR,
                    Profitability = strategyDetail.Profitability,
                    SellingEndDate = strategyDetail.SellingEndDate,
                    SellingStartDate = strategyDetail.SellingStartDate,
                    VersionCode = strategyDetail.VersionCode,
                    RateOfReturn = strategyDetail.RateOfReturn,
                    isNotBought = strategyDetail.isNotBought,
                    FIPartRUR = strategyDetail.FIPartRUR,
                    GFPartRUR = strategyDetail.GFPartRUR,
                },
                StrategyCurrency = strategyCurrency.ID,
                StrategyCurrencyRaw = strategyCurrency.Name,

                InsurancePeriod = insurancePeriod.ID,
                InsurancePeriodRaw = insurancePeriod.Name,
                ParticipationCoefficient = strategyDetail.Coefficient,
                InsuranceSum = insuranceSum != null ? insuranceSum?.ID : "",
                ManualInsuranceSum = insuranceSum == null ? parameter.amount.ToString() : "",
                ContributionsFrequency = "1",
                Premium = parameter.amount,
                Currency = currency.ID,
                LastName = parameter.insurant.lastName,
                FirstName = parameter.insurant.firstName,
                Patronymic = parameter.insurant.patronymicName,
                BirthDate = getDate(parameter.insurant.dateOfBirth),
                BirthPlace = parameter.insurant.placeOfBirth,
                Sex = getSex(parameter.insurant.sex),
                INN = parameter.insurant.inn,
                Cityzenship = "1",
                InsuranceSumType = insuranceSum == null ? "2" : "1",
                OtherCityzenship = "",
                Resident = "1",
                StayingDocumentType = "",
                StayingDocumentTypeRaw = "",
                StayingDocumentSerial = "",
                StayingDocumentNumber = "",
                StayingDocumentDate = null,
                StayingDocumentOrganisation = "",
                StayingDocumentEndDate = null,
                MigrationCardSerial = "",
                MigrationCardNumber = "",
                MigrationCardDate = null,
                StartStayingDate = null,
                EndStayingDate = null,
                Phone = "не указан",
                Email = "не указан",
                AddressInputType = "2",
                Address = new Address()
                {
                    KLADRCode = "000000000000000",
                },
                AddressText = getFullAddress(parameter.insurant.addresses.First(A => A.type, addressType.registration)),
                AgreeForSpam = false,
                DocumentType = documentType.ID,
                DocumentTypeRaw = documentType.Name,
                PassportSerial = parameter.insurant.documentSerie,
                PassportNumber = parameter.insurant.documentNumber,
                PassportDate = getDate(parameter.insurant.documentIssueDate),
                PassportOrganisation = parameter.insurant.documentOrganisation,
                PassportCode = parameter.insurant.documentOrganisationCode,
                ActInOwnInterests = "1",
                ActInPublicFaceInterest = "5",
                IsCreatorOfPublicOrganisation = "2",
                IsResidentOfEconomicZone = "2",
                IsPublicFace = "2",

                InsuredIsInsurer = parameter.insurantIsInsured,

                // insured
                InsuredLastName = insured.lastName,
                InsuredFirstName = insured.firstName,
                InsuredPatronymic = insured.patronymicName,
                InsuredBirthDate = getDate(insured.dateOfBirth),
                InsuredBirthPlace = insured.placeOfBirth,
                InsuredSex = getSex(insured.sex),
                InsuredINN = insured.inn,
                InsuredCityzenship = "1",
                InsuredOtherCityzenship = "",
                InsuredResident = "1",
                InsuredStayingDocumentType = "",
                InsuredStayingDocumentTypeRaw = "",
                InsuredStayingDocumentSerial = "",
                InsuredStayingDocumentNumber = "",
                InsuredStayingDocumentDate = null,
                InsuredStayingDocumentOrganisation = "",
                InsuredStayingDocumentEndDate = null,
                InsuredMigrationCardSerial = "",
                InsuredMigrationCardNumber = "",
                InsuredMigrationCardDate = null,
                InsuredStartStayingDate = null,
                InsuredEndStayingDate = null,
                InsuredPhone = "не указан",
                InsuredEmail = "не указан",
                InsuredAddressInputType = "2",
                InsuredAddress = new Insuredaddress()
                {
                    KLADRCode = "000000000000000",
                },
                InsuredAddressText = getFullAddress(insured.addresses.First(A => A.type, addressType.registration)),
                InsuredAgreeForSpam = false,
                InsuredDocumentType = documentType.ID,
                InsuredDocumentTypeRaw = documentType.Name,
                InsuredPassportSerial = insured.documentSerie,
                InsuredPassportNumber = insured.documentNumber,
                InsuredPassportDate = getDate(insured.documentIssueDate),
                InsuredPassportOrganisation = insured.documentOrganisation,
                InsuredPassportCode = insured.documentOrganisationCode,
                InsuredActInOwnInterests = "1",
                InsuredActInPublicFaceInterest = "5",
                InsuredIsCreatorOfPublicOrganisation = "2",
                InsuredIsResidentOfEconomicZone = "2",
                InsuredIsPublicFace = "2",
                IsSuccessor = parameter.beneficiaries?.Any() == true ? "1" : "2",
                Beneficiaries = parameter.beneficiaries?.Select(beneficiary => new Beneficiary()
                {
                    LastName = beneficiary.lastName,
                    FirstName = beneficiary.firstName,
                    Patronymic = beneficiary.patronymicName,
                    BirthDate = getDate(beneficiary.dateOfBirth),
                    BirthPlace = beneficiary.placeOfBirth,
                    DocumentType = documentType.ID,
                    DocumentTypeRaw = documentType.Name,
                    DocumentSerial = beneficiary.documentSerie,
                    DocumentNumber = beneficiary.documentNumber,
                    DocumentDate = getDate(beneficiary.documentIssueDate),
                    DocumentOrganisation = beneficiary.documentOrganisation,
                    DocumentOrganisationCode = beneficiary.documentOrganisationCode,
                    Part = beneficiary.percent,
                })?.ToArray() ?? new Beneficiary[0],
                PaymentType = "",
                PaymentDocumentDate = getDate(today),
                SellerComment = "",
                ReceiptDate = null,
                ReceiptSum = 0,
                SalesChannel = "400",
                Bayout = getBuyoutTariffs.Select(A => new Bayout()
                {
                    InsPeriod = A.InsPeriod.value,
                    InsSum = A.InsSum.value,
                }).ToArray(),
                UserTown = "Москва",
                KvPartner1Percent = calculate.KvPartner1Percent,
                KvPartner1Rub = calculate.KvPartner1Rub,
                KvPartner2Percent = calculate.KvPartner2Percent,
                KvPartner2Rub = calculate.KvPartner2Rub,
                Partner1Name = "",
                Partner2Name = "",
                CheafName = "Генерального директора",
                SellerDivisionHierarchyInfo = "",
                ScanWasSent = false,
                SalesPartner = "",
                SalesPartner2 = "",
                IsForeignTaxpayer = "2",
                StatusID = status.ID,
                DocumentStatusID = status.ID,
            });
            return new CreatePolicyResponse()
            {
                policyId = policy.ID,
                policyNumber = policy.SERIAL + " " + policy.NUMBER,
            };
        }
        private static policyStatus getStatus(string status)
        {
            if (string.Equals(status, "Проект", StringComparison.OrdinalIgnoreCase))
            {
                return policyStatus.Item;
            }
            else if (string.Equals(status, "Действующий", StringComparison.OrdinalIgnoreCase))
            {
                return policyStatus.Item1;
            }
            else
            {
                throw new Exception($"неизвестный статус полиса, status: {status}");
            }

        }
        public GetPolicyResponse GetPolicy(GetPolicyRequest parameter)
        {
            var product = this.virtuClient.GetProducts().Single(A => A.Name, "Верное решение");
            var risks = this.virtuClient.GetRisks(product.ID);
            var currencies = this.virtuClient.GetCurrencies(product.ID);
            var getBuyoutTariffs = this.virtuClient.GetTariffs(product.ID);
            var strategyDetails = this.virtuClient.StrategiesSearch(new StrategiesSearchInput()
            {
                IsActive = true,
                ReadRedefined = true,
            });
            var insuredDocumentTypes = this.virtuClient.InsuredDocumentTypes(product.ID);
            var policy = this.virtuClient.Read(parameter.policyId);
            var strategyDetail = strategyDetails.First(A => A.InvestmentStrategy, policy.InvestmentStrategy);
            return new GetPolicyResponse()
            {
                amount = policy.Premium.Value,
                coefficient = policy.ParticipationCoefficient.Value,
                status = getStatus(policy.StatusName),
                strategy = policy.InvestmentStrategyRaw,
                fullDescription = product.Description,
                currency = getCurrency(currencies.Single(A => A.ID, policy.Currency)).Value,
                coverCapital = 100,
                profitability = strategyDetail.Profitability,
                effectiveDate = getDate(policy.EffectiveDate).Value,
                expirationDate = getDate(policy.ExpirationDate).Value,
                insuranceRisks = risks.Select(A => new risk()
                {
                    id = A.ID,
                    sum = policy.Premium.Value.ToString(),
                    text = A.Name,
                }).ToArray(),
                paymentsPlan = null,
                policyNumber = policy.SERIAL + " " + policy.NUMBER,
                productId = policy.ProductID,
                productName = product.Name,
                redemptionAmounts = getBuyoutTariffs.Select(A => new redemptionAmountInfo()
                {
                    sum = decimal.Parse(A.InsSum.value),
                    date = DateTime.Today.AddYears(int.Parse(A.Year.value))
                }).ToArray(),
            };
        }
        public GetPolicyDocumentResponse GetPolicyDocument(GetPolicyDocumentRequest parameter)
        {
            var productId = this.virtuClient.Read(parameter.policyId).ProductID;
            string printformCaption = getPrintformCaption(parameter.documentType);
            var printform = this.virtuClient.GetPrintforms(productId).Single(A => A.Value.Caption, printformCaption);
            return new GetPolicyDocumentResponse()
            {
                documentData = this.virtuClient.Print(new PrintInput()
                {
                    policyID = parameter.policyId,
                    viewID = printform.Key,
                }),
                documentName = printform.Value.Caption,
                contentType = getContentType(getExtension(printform.Value.Transform)),
            };
        }
        public AcceptPolicyResponse Accept(AcceptPolicyRequest parameter)
        {
            Policy policy = this.virtuClient.Read(parameter.policyId);
            var status = this.virtuClient.GetStatuses(policy.ProductID).Single(A => A.Name, "Действующий");
            policy.AcceptationDate = getDate(DateTime.Now);
            policy.ReceiptDate = getDate(DateTime.Now);
            policy.PaymentType = "4";
            policy.ReceiptSum = policy.Premium;
            policy.StatusID = status.ID;
            policy.DocumentStatusID = status.ID;
            policy.StatusName = status.Name;
            this.virtuClient.Accept(policy);
            return new AcceptPolicyResponse()
            {
                result = "OK",
            };
        }
    }
}