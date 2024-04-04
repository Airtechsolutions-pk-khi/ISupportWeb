namespace ISupportWeb.Models.BLL
{
    public class TicketBLL
    {
        public int TicketID { get; set; }
        public string TicketNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TicketType { get; set; }
        public string Timeslots { get; set; }
        public string ConsultancyCharges { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int? TicketStatus { get; set; }
        public int? StatusID { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
