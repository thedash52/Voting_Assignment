using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace votingBackend.VoteData.Dto
{
    [AutoMap(typeof(Referendum))]
    public class ReferendumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Images { get; set; }
    }
}
