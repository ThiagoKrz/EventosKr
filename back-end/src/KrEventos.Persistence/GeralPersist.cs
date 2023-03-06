using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KrEventos.Domain;
using KrEventos.Persistence.Contextos;
using KrEventos.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace KrEventos.Persistence.bin
{
    public class GeralPersist : IGeralPersist
    {
        private readonly KrEventosContext _context;
        public GeralPersist(KrEventosContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}