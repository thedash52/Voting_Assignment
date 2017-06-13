using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace votingBackend.EntityFrameworkCore.Repositories
{
    public interface IVoteDataRepository : IRepository
    {
        Tuple<string, bool> AddElectorate(string name, string detail, string image);

        Tuple<string, bool> AddCandidate(string name, string detail, string image);

        Tuple<string, bool> NewReferendum(string name, string detail, string images);

        Tuple<string, bool> AddParty(string name, string detail, string image);

        Tuple<List<Electorate>, string, bool> GetElectorates();

        Tuple<List<Candidate>, string, bool> GetCandidates();

        Tuple<List<Party>, string, bool> GetParties();

        Tuple<Referendum, string, bool> GetReferendum();
    }
}
