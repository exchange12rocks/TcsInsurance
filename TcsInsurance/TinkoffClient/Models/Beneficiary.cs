using System;
using System.Collections.Generic;

namespace TinkoffClient.Models
{
    public class Beneficiary
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string lastName { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string firstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string patronymicName { get; set; }
        /// <summary>
        /// Серия паспорта
        /// </summary>
        public string documentSerie { get; set; }
        /// <summary>
        /// Номер паспорта
        /// </summary>
        public string documentNumber { get; set; }
        /// <summary>
        /// Дата выдачи паспорта
        /// </summary>
        public DateTime? documentIssueDate { get; set; }
        /// <summary>
        /// Организация, выдавшая документ
        /// </summary>
        public string documentOrganisation { get; set; }
        /// <summary>
        /// Код подразделения
        /// </summary>
        public string documentOrganisationCode { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime dateOfBirth { get; set; }
        /// <summary>
        /// Место рождения
        /// </summary>
        public string placeOfBirth { get; set; }
        /// <summary>
        /// Пол
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// ИНН
        /// </summary>
        public string inn { get; set; }
        /// <summary>
        /// Адрес (регистрации и проживания)
        /// </summary>
        public List<Address> adresses { get; set; }
        /// <summary>
        /// Семейное отношение (к Застрахованному)
        /// </summary>
        public int? relation { get; set; }
        /// <summary>
        /// Доля выгодоприобретателя
        /// </summary>
        public decimal percent { get; set; }
    }
}