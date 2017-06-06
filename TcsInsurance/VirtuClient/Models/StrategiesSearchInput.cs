﻿namespace VirtuClient.Models
{
    public class StrategiesSearchInput
    {
        public string action { get; set; }
        public string method { get; set; }
        public int tid { get; set; }
        public string type { get; set; }
        public StrategiesSearchDataInput[] data { get; set; }
    }
    public class StrategiesSearchDataInput
    {
        /// <summary>
        /// Флаг, указывающий на необходимость загрузить активные опционы (активность опциона определяется подразделением). True – загрузка нужна, False – загрузка не нужна. 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Флаг, указывающий, нужно ли прочитать опцион со свойствами, переопределенными в рамках пользователя или базовый опцион. Переопределены могут быть следующие параметры опциона: FIPart, Coefficient, IsActive.
        /// </summary>
        public bool ReadRedefined { get; set; }
    }
}