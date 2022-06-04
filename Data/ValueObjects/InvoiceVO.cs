using System;

namespace Data.ValueObjects
{
    public class InvoiceVO
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public double Value { get; set; }
        public CustomerVO customer { get; set; }
    }
}
