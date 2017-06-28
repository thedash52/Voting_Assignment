using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingBackend.Authorization.Users;
using votingBackend.EntityFrameworkCore.Models;
using votingBackend.EntityFrameworkCore.Repositories;

namespace votingBackend.EntityFrameworkCore
{
    public class UserRepository : IUserRespository
    {
        private votingBackendDbContext _dbContext;
        private DbContextOptionsBuilder<votingBackendDbContext> builder;

        public UserRepository()
        {
            builder = new DbContextOptionsBuilder<votingBackendDbContext>();
            builder.UseSqlServer("Data Source=tcp:developmentdaniel.database.windows.net,1433;Initial Catalog=votingBackend_db;User Id=danieln@developmentdaniel;Password=tHedAshc379sq;");
        }

        public Tuple<UserVote, string, bool> Authenticate(string first, string last, string dob, string electoral)
        {
            UserVote user = new UserVote();

            using (var ctx = _dbContext = new votingBackendDbContext(builder.Options))
            {
                try
                {
                    var result = (from u in ctx.UserVoteSet
                                 where u.FirstName == first && u.LastName == last && u.DoB == DateTime.Parse(dob) && u.ElectoralId == electoral
                                  select new UserVote
                                  {
                                      Id = u.Id,
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      DoB = u.DoB,
                                      ElectoralId = u.ElectoralId,
                                      ElectorateId = u.ElectorateId,
                                      CandidateIds = u.CandidateIds,
                                      PartyId = u.PartyId,
                                      Referendum = u.Referendum,
                                      VoteSaved = u.VoteSaved
                                  }).ToList();

                    if (result.Count == 1)
                    {
                        if (result.FirstOrDefault() != null)
                        {
                            user = result.FirstOrDefault();

                            return Tuple.Create(user, "Success", true);
                        }
                        else
                        {
                            return Tuple.Create(user, "Invalid Login", false);
                        }
                    }
                    else
                    {
                        return Tuple.Create(user, "Invalid Login", false);
                    }
                    
                }
                catch (Exception ex)
                {
                    return Tuple.Create(user, ex.Message, false);
                }
            }
        }

        public Tuple<string, bool> Register(string first, string last, string dob, string electoralId)
        {
            UserVote user = new UserVote()
            {
                FirstName = first,
                LastName = last,
                DoB = DateTime.Parse(dob),
                ElectoralId = electoralId,
                VoteSaved = false
            };

            using (var ctx = _dbContext = new votingBackendDbContext(builder.Options))
            {
                try
                {
                    ctx.UserVoteSet.Add(user);
                    ctx.SaveChanges();
                    if (user.Id >= 0)
                    {
                        return Tuple.Create("Success", true);
                    }
                    else
                    {
                        return Tuple.Create("Unable to register user", false);
                    }
                }
                catch
                {
                    return Tuple.Create("Unable to register user", false);
                }
            }
        }

        public Tuple<string, bool> SaveVote(int id, int electorateId, string candidateIds, int partyId, bool referendum)
        {
            using(var ctx = _dbContext = new votingBackendDbContext(builder.Options))
            {
                try
                {
                    UserVote user = ctx.UserVoteSet.Single<UserVote>(s => s.Id == id);

                    if (user.VoteSaved)
                    {
                        Tuple.Create("Vote Already Saved", false);
                    }

                    user.ElectorateId = electorateId;
                    user.CandidateIds = candidateIds;
                    user.PartyId = partyId;
                    user.Referendum = referendum;
                    user.VoteSaved = true;

                    ctx.Entry(user).State = EntityState.Modified;
                    ctx.SaveChanges();
                    
                    return Tuple.Create("Vote Saved", true);
                }
                catch
                {
                    return Tuple.Create("Unable to save vote", false);
                }
            }
        }
    }
}
