using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fody;
using Microsoft.EntityFrameworkCore;
using MySafe.Tests.Ef.Models;
using Xamarin.Essentials.Interfaces;

namespace MySafe.Tests.Ef.Repositories
{
    public class EfSecureStorageRepository : ISecureStorage
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<SecureStorageEntity> _dbSet;

        public EfSecureStorageRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<SecureStorageEntity>();
        }

        public async Task<string> GetAsync(string key)
        {
            var entity = await _dbSet.FindAsync(key);

            return entity?.Value;
        }

        public async Task SetAsync(string key, string value)
        {
            var dbEntity = await _context.SecureStorage.FindAsync(key); 
            
            if (dbEntity == null)
            {
                await _context.SecureStorage.AddAsync(new SecureStorageEntity()
                {
                    Key = key,
                    Value = value
                });

                await _context.SaveChangesAsync();
                return;
            }

            if (dbEntity.Value != value)
            {
                dbEntity.Value = value;
                _dbSet.Attach(dbEntity);
                _context.Entry(dbEntity).Property(p => p.Value).IsModified = true;
            }

            await _context.SaveChangesAsync();
            var entity2 = await _dbSet.FindAsync(key);

            if (entity2.Value != value)
            {
                Trace.WriteLine("");
            }
        }

        public bool Remove(string key)
        {
            var entity =_dbSet.Find(key);
            
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public void RemoveAll()
        {
            _dbSet.RemoveRange();
        }
    }
}
