using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingBackend.EntityFrameworkCore.Repositories;

namespace votingBackend.EntityFrameworkCore
{
    public class VoteDataRepository : IVoteDataRepository
    {
        private votingBackendDbContextFactory _dbContext;

        public VoteDataRepository()
        {
            _dbContext = new votingBackendDbContextFactory();
        }

        public Tuple<string, bool> AddElectorate(string name, string detail, string image)
        {
            Electorate newItem = new Electorate()
            {
                Name = name,
                Detail = detail,
                Image = image
            };

            using (var ctx = _dbContext.Create(new DbContextFactoryOptions()))
            {
                try
                {
                    ctx.ElectorateSet.Add(newItem);
                    ctx.SaveChanges();

                    if (newItem.Id >= 0)
                    {
                        return Tuple.Create("Success", true);
                    }
                    else
                    {
                        return Tuple.Create("Unable to register electorate", false);
                    }
                }
                catch (Exception ex)
                {
                    return Tuple.Create(ex.Message, false);
                }
            }
        }

        public Tuple<string, bool> AddCandidate(string name, string detail, string image)
        {
            Candidate newItem = new Candidate()
            {
                Name = name,
                Detail = detail,
                Image = image
            };

            using (var ctx = _dbContext.Create(new DbContextFactoryOptions()))
            {
                try
                {
                    ctx.CandidateSet.Add(newItem);
                    ctx.SaveChanges();

                    if (newItem.Id >= 0)
                    {
                        return Tuple.Create("Success", true);
                    }
                    else
                    {
                        return Tuple.Create("Unable to register Candidate", false);
                    }
                }
                catch (Exception ex)
                {
                    return Tuple.Create(ex.Message, false);
                }
            }
        }

        public Tuple<string, bool> NewReferendum(string name, string detail, string images)
        {
            Referendum newItem = new Referendum()
            {
                Name = name,
                Detail = detail,
                Images = images,
                Active = true
            };

            using (var ctx = _dbContext.Create(new DbContextFactoryOptions()))
            {
                try
                {
                    var update = (from r in ctx.ReferendumSet
                                  where r.Active == true
                                  select new Referendum
                                  {
                                      Id = r.Id,
                                      Name = r.Name,
                                      Detail = r.Detail,
                                      Images = r.Images,
                                      Active = r.Active
                                  }).ToList();

                    foreach (var item in update)
                    {
                        item.Active = false;

                        ctx.Entry(item).State = EntityState.Modified;
                    }

                    ctx.ReferendumSet.Add(newItem);
                    ctx.SaveChanges();

                    if (newItem.Id >= 0)
                    {
                        return Tuple.Create("Success", true);
                    }
                    else
                    {
                        return Tuple.Create("Unable to register Referendum", false);
                    }
                }
                catch (Exception ex)
                {
                    return Tuple.Create(ex.Message, false);
                }
            }
        }

        public Tuple<string, bool> AddParty(string name, string detail, string image)
        {
            Party newItem = new Party()
            {
                Name = name,
                Detail = detail,
                Image = image
            };

            using (var ctx = _dbContext.Create(new DbContextFactoryOptions()))
            {
                try
                {
                    ctx.PartySet.Add(newItem);
                    ctx.SaveChanges();

                    if (newItem.Id >= 0)
                    {
                        return Tuple.Create("Success", true);
                    }
                    else
                    {
                        return Tuple.Create("Unable to register Party", false);
                    }
                }
                catch (Exception ex)
                {
                    return Tuple.Create(ex.Message, false);
                }
            }
        }

        public Tuple<List<Electorate>, string, bool> GetElectorates()
        {
            List<Electorate> listItems = new List<Electorate>();

            using (var ctx = _dbContext.Create(new DbContextFactoryOptions()))
            {
                try
                {
                    var result = (from i in ctx.ElectorateSet
                                  select new Electorate
                                  {
                                      Id = i.Id,
                                      Name = i.Name,
                                      Detail = i.Detail,
                                      Image = i.Image
                                  }).ToList();

                    if (result.Count > 0)
                    {
                        listItems = result;

                        return Tuple.Create(listItems, "Success", true);
                    }
                    else
                    {
                        return Tuple.Create(listItems, "Unable to get Electorates", false);
                    }
                }
                catch (Exception ex)
                {
                    return Tuple.Create(listItems, ex.Message, false);
                }
            }
        }

        public Tuple<List<Candidate>, string, bool> GetCandidates()
        {
            List<Candidate> listItems = new List<Candidate>();

            using (var ctx = _dbContext.Create(new DbContextFactoryOptions()))
            {
                try
                {
                    var result = (from i in ctx.CandidateSet
                                  select new Candidate
                                  {
                                      Id = i.Id,
                                      Name = i.Name,
                                      Detail = i.Detail,
                                      Image = i.Image
                                  }).ToList();

                    if (result.Count > 0)
                    {
                        listItems = result;

                        return Tuple.Create(listItems, "Success", true);
                    }
                    else
                    {
                        return Tuple.Create(listItems, "Unable to get Candidates", false);
                    }
                }
                catch (Exception ex)
                {
                    return Tuple.Create(listItems, ex.Message, false);
                }
            }
        }

        public Tuple<List<Party>, string, bool> GetParties()
        {
            List<Party> listItems = new List<Party>();

            using (var ctx = _dbContext.Create(new DbContextFactoryOptions()))
            {
                try
                {
                    var result = (from i in ctx.PartySet
                                  select new Party
                                  {
                                      Id = i.Id,
                                      Name = i.Name,
                                      Detail = i.Detail,
                                      Image = i.Image
                                  }).ToList();

                    if (result.Count > 0)
                    {
                        listItems = result;

                        return Tuple.Create(listItems, "Success", true);
                    }
                    else
                    {
                        return Tuple.Create(listItems, "Unable to get Parties", false);
                    }
                }
                catch (Exception ex)
                {
                    return Tuple.Create(listItems, ex.Message, false);
                }
            }
        }

        public Tuple<Referendum, string, bool> GetReferendum()
        {
            Referendum listItems = new Referendum();

            using (var ctx = _dbContext.Create(new DbContextFactoryOptions()))
            {
                try
                {
                    var result = (from i in ctx.ReferendumSet
                                  where i.Active == true
                                  select new Referendum
                                  {
                                      Id = i.Id,
                                      Name = i.Name,
                                      Detail = i.Detail,
                                      Images = i.Images
                                  }).ToList();

                    if (result.Count != 0)
                    {
                        if (result.FirstOrDefault() != null)
                        {
                            listItems = result.FirstOrDefault();

                            return Tuple.Create(listItems, "Success", true);
                        }
                        else
                        {
                            return Tuple.Create(listItems, "Unable to get Referendum", false);
                        }
                    }
                    else
                    {
                        return Tuple.Create(listItems, "Unable to get Referendum", false);
                    }
                }
                catch (Exception ex)
                {
                    return Tuple.Create(listItems, ex.Message, false);
                }
            }
        }
    }
}
