using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using tinkoff.ru.partners.insurance.investing.types;
using TinkoffClient;
using TinkoffService.CbrServiceReference;
namespace TinkoffService.Helpers
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class ValuteData
    {

        private ValuteDataValuteCursOnDate[] valuteCursOnDateField;

        private uint onDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ValuteCursOnDate")]
        public ValuteDataValuteCursOnDate[] ValuteCursOnDate
        {
            get
            {
                return this.valuteCursOnDateField;
            }
            set
            {
                this.valuteCursOnDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint OnDate
        {
            get
            {
                return this.onDateField;
            }
            set
            {
                this.onDateField = value;
            }
        }
    }
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ValuteDataValuteCursOnDate
    {

        private string vnameField;

        private ushort vnomField;

        private decimal vcursField;

        private ushort vcodeField;

        private string vchCodeField;

        /// <remarks/>
        public string Vname
        {
            get
            {
                return this.vnameField;
            }
            set
            {
                this.vnameField = value;
            }
        }

        /// <remarks/>
        public ushort Vnom
        {
            get
            {
                return this.vnomField;
            }
            set
            {
                this.vnomField = value;
            }
        }

        /// <remarks/>
        public decimal Vcurs
        {
            get
            {
                return this.vcursField;
            }
            set
            {
                this.vcursField = value;
            }
        }

        /// <remarks/>
        public ushort Vcode
        {
            get
            {
                return this.vcodeField;
            }
            set
            {
                this.vcodeField = value;
            }
        }

        /// <remarks/>
        public string VchCode
        {
            get
            {
                return this.vchCodeField;
            }
            set
            {
                this.vchCodeField = value;
            }
        }
    }
    public class CbrHelper
    {
        public decimal getRate(DateTime date, currency currency)
        {
            if (currency == currency.RUR) return 1;
            DailyInfo cbrClient = new DailyInfo();
            cbrClient.Proxy = new WebProxy("http://msk01-wsncls02.uralsibins.ru:8080")
            {
                Credentials = new NetworkCredential(@"uralsibins\svcTinkoff", "USER4tinkoff"),
            };
            XmlNode resultXml = cbrClient.GetCursOnDateXML(date);
            XmlSerializer serializer = new XmlSerializer(typeof(ValuteData));
            ValuteData result = (ValuteData)serializer.Deserialize(new StringReader(resultXml.OuterXml));
            return result.ValuteCursOnDate.Single(A => A.VchCode, currency.ToString()).Vcurs;
        }
    }
}