using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace votingBackend.VoteData.Dto
{
    [AutoMap(typeof(Electorate))]
    public class ElectorateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
    }
}
