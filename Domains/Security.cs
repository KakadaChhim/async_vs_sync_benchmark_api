namespace async_vs_sync_benchmark_api.Domain
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class NotAuditAttribute : Attribute
    {
    }
    public class SharedDomain
    {
        protected const int MAX_20000 = 20000;
        protected const int MAX_2000 = 2000;
        protected const int MAX_500 = 500;
        protected const int MAX_50 = 50;
        [NotAudit]
        public long Id { get; set; }
        [NotAudit]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [NotAudit]
        public string CreatedBy { get; set; } = "";
        [NotAudit]
        public DateTime? LastModifiedDate { get; set; } = null;
        [NotAudit]
        public string LastModifiedBy { get; set; } = "";
        [NotAudit]
        public bool Active { get; set; } = true;
    }
}

