namespace TelecomBillingAndConsumption.Service.HelperDtos
{
    public class SubscriberUsageSummaryResponse
    {
        public int SubscriberId { get; set; }
        public string SubscriberPhone { get; set; }
        public string PlanName { get; set; }

        // Used units for current month
        public int UsedCallMinutes { get; set; }
        public decimal UsedDataMB { get; set; }
        public int UsedSmsCount { get; set; }

        // Plan bundle limits
        public int CallMinutesBundle { get; set; }
        public decimal DataBundleMB { get; set; }
        public int SmsBundle { get; set; }

        // Units left before overage - never negative
        public int CallMinutesLeft { get; set; }       // = Max(0, CallMinutesBundle - UsedCallMinutes)
        public decimal DataMBLeft { get; set; }        // = Max(0, DataBundleMB - UsedDataMB)
        public int SmsLeft { get; set; }               // = Max(0, SmsBundle - UsedSmsCount)

        // Units used above bundle limit - zero if not exceeded
        public int CallMinutesOverBundle { get; set; }   // = Max(0, UsedCallMinutes - CallMinutesBundle)
        public decimal DataMBOverBundle { get; set; }    // = Max(0, UsedDataMB - DataBundleMB)
        public int SmsOverBundle { get; set; }           // = Max(0, UsedSmsCount - SmsBundle)


        // Tariff info (current rate for next unit)
        public decimal CurrentCallUnitPrice { get; set; }
        public decimal CurrentDataUnitPrice { get; set; }
        public decimal CurrentSmsUnitPrice { get; set; }

        // Will next unit be overage/double rate?
        public bool IsCallOverage { get; set; }
        public bool IsDataOverage { get; set; }
        public bool IsSmsOverage { get; set; }

        // Optionally: show the overage (double) rate if exceeded
        public decimal OverageCallUnitPrice { get; set; }
        public decimal OverageDataUnitPrice { get; set; }
        public decimal OverageSmsUnitPrice { get; set; }

        public int PeakCalls { get; set; }
        public int OffPeakCalls { get; set; }


        // Optionally: billing period info & next reset date
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}
