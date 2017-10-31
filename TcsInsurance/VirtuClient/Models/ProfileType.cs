namespace VirtuClient.Models
{
    public enum ProfileType
    {
        /// <summary>
        /// Минимальный возраст застрахованного
        /// </summary>
        MinAge,
        /// <summary>
        /// Максимальный возраст застрахованного
        /// </summary>
        MaxAge,
        /// <summary>
        /// Максимальное кол-во выгодоприобретателей по смерти
        /// </summary>
        MaxBeneficiaries,
        /// <summary>
        /// Наименование партнера 1
        /// </summary>
        Partner1Name,
        /// <summary>
        /// Наименование партнера 2
        /// </summary>
        Partner2Name,
        /// <summary>
        /// КВ для партнера 1
        /// </summary>
        Partner1KV,
        /// <summary>
        /// КВ для партнера 2
        /// </summary>
        Partner2KV,
        /// <summary>
        /// Страховая компания (Страховщик)
        /// </summary>
        InsCompany,
        /// <summary>
        /// Адрес страховщика
        /// </summary>
        InsAddress,
        /// <summary>
        /// Телефон страховщика
        /// </summary>
        InsPhone,
        /// <summary>
        /// Факс страховщика
        /// </summary>
        InsFax,
        /// <summary>
        /// E-mail страховщика
        /// </summary>
        InsEmail,
        /// <summary>
        /// Сайт страховщика
        /// </summary>
        InsSite,
        /// <summary>
        /// Наименование Правил страхования / условия страхования
        /// </summary>
        InsRulesName,
        /// <summary>
        ///Правила/ Условия страхования утверждены приказом №, дата
        /// </summary>
        InsRulesOrder,
        /// <summary>
        /// Лицензии банка России (лицензия 1)
        /// </summary>
        InsLisense1,
        /// <summary>
        /// Лицензии банка России (лицензия 2)
        /// </summary>
        InsLisense2,
        /// <summary>
        /// ФИО представителя Страховщика в именительном падеже
        /// </summary>
        InsReprensentativeFio,
        /// <summary>
        /// Должность представителя Страховщика в именительном падеже
        /// </summary>
        InsReprensentativePost,
        /// <summary>
        /// Номер и дата доверенности представителя Страховщика
        /// </summary>
        WarrancyNumber,
        /// <summary>
        /// Факсимильная печать
        /// </summary>
        FacsimilePress,
        /// <summary>
        /// Факсимильная подпись
        /// </summary>
        FacsimileSign,
        /// <summary>
        /// Банк Уралсиб
        /// </summary>
        BankUralsib,
        /// <summary>
        /// Получатель платежа
        /// </summary>
        PaymentRecipient,
        /// <summary>
        /// Расчетный счет получателя платежа
        /// </summary>
        PaymentRecipientSettlmentAccount,
        /// <summary>
        /// Банк получателя платежа
        /// </summary>
        PaymentRecipientBank,
        /// <summary>
        /// ИНН получателя платежа
        /// </summary>
        PaymentRecipientInn,
        /// <summary>
        /// КПП получателя платежа
        /// </summary>
        PaymentRecipientKpp,
        /// <summary>
        /// Кор.счет банка получателя платежа
        /// </summary>
        PaymentRecipientCorrespondentAccount,
        /// <summary>
        /// БИК банка получателя платежа
        /// </summary>
        PaymentRecipientBankBik,
        /// <summary>
        /// Канал продаж
        /// </summary>
        SalesChannel,
        /// <summary>
        /// Должность руководителя в родительном падеже
        /// </summary>
        CheafName,
    }
}