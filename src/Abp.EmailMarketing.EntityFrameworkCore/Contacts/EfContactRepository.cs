using Abp.EmailMarketing.EntityFrameworkCore;
using Abp.EmailMarketing.GroupContacts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.EmailMarketing.Contacts
{
    public class EfContactRepository : 
        EfCoreRepository<EmailMarketingDbContext, Contact, Guid>, IContactRepository
    {
        private readonly IGroupRepository _groupRepository;
        public EfContactRepository(IDbContextProvider<EmailMarketingDbContext> dbContextProvider, IGroupRepository groups)
            : base(dbContextProvider)
        {
            _groupRepository = groups;
        }

        public async Task<Contact> FindByEmailAsync(string email)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(e => e.Email.Equals(email));
        }

        public async Task<List<Contact>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            //var dbSet = _emailMarketingDbContext.Emails;
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    c => c.Email.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task CreateContact(Contact contact)
        {
            var context = await GetDbContextAsync();
            var list = contact.ContactGroups;
            contact.ContactGroups = null;
            await context.Contacts.AddAsync(contact);
            foreach (var g in list)
            {
                await context.ContactGroups.AddAsync(g);
            }
            await context.SaveChangesAsync();
        }

        public async Task UpdateContact(Guid id, Contact contact)
        {
            var context = await GetDbContextAsync();
            var oldContact = context.Contacts.FirstOrDefault(c => c.Id.ToString().Equals(id.ToString()));

            context.Entry(oldContact).CurrentValues.SetValues(contact);

            /*foreach (var item in oldContact.Groups)
            {
                if (!contact.Groups.Any(g => g.Id.ToString().Equals(item.Id.ToString()))){
                    oldContact.Groups.Remove(item);
                }
            }

            foreach(var group in contact.Groups)
            {
                var groupInDB = oldContact.Groups.SingleOrDefault(
                    g => g.Id.ToString().Equals(group.Id.ToString())
                );

                if (groupInDB != null)
                    // Update groups
                    context.Entry(groupInDB).CurrentValues.SetValues(group);
                else
                {
                    // Add courses relationships
                    context.Groups.Attach(group);
                    oldContact.Groups.Add(group);
                }
            }*/
            
            await context.SaveChangesAsync();
        }
    }
}
