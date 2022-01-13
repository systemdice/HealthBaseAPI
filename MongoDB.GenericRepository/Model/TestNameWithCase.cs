using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Model
{
    public class TestNameWithCase
    {
        public string RefferDoctorName { get; set; }
        public string RefferDoctorCharge { get; set; }
        public string RefferDoctorCommission { get; set; }
        public string CollectionCenter { get; set; }
        public List<TestType> TestType { get; set; }

    }
    public class Name
    {
        public string TestName { get; set; }
        public int TestPrice { get; set; }
    }

    public class TestType
    {
        public string Description { get; set; }
        public string CollectionCenter { get; set; }
        public string parentTest { get; set; }
        public object Paid { get; set; }
        public object Discount { get; set; }
        public object DiscountType { get; set; }
        public int TotalSum { get; set; }
        public string grpPkgName { get; set; }
        public List<Name> names { get; set; }
    }
}
