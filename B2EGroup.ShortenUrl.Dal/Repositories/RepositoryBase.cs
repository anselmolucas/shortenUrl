using B2EGroup.ShortenUrl.Dal.Interfaces;
using B2EGroup.ShortenUrl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace B2EGroup.ShortenUrl.Dal.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryModel<T>, IDisposable where T : class
    {
        protected DbContext _context;

        private bool _saveChanges   = true;
        string errorMessage         = string.Empty;

        public RepositoryBase(bool SaveChanges = true)
        {
            _saveChanges = SaveChanges;
            _context = new ShortenUrlContext();
        }

        public T Save(T objectToSave)
        {
            var objectType  = objectToSave.GetType();
            int id          = 0;

            foreach (var p in objectType.GetProperties())           
                if (p.Name == "Id")
                    id = (int)p.GetValue(objectToSave);

            if (id > 0)
                objectToSave = Update(objectToSave);
            else
                objectToSave = Add(objectToSave);
            
            return objectToSave;
        }

        public T Add(T objectToAdd)
        {
            try
            {
                if (objectToAdd == null)
                    throw new ArgumentNullException("objectToAdd nulo");

                _context.Set<T>().Add(objectToAdd);

                if (_saveChanges)
                    _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validationErrors in e.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        errorMessage += Environment.NewLine + $"Propriedade: {validationError.PropertyName} / Erro: {validationError.ErrorMessage}";

                throw new Exception(errorMessage, e);
            }

            return objectToAdd;
        }

        public T Update(T objectToUpdate)
        {
            try
            {
                if (objectToUpdate == null)
                    throw new ArgumentNullException("objectToUpdate nulo");

                _context.Entry(objectToUpdate).State = EntityState.Modified;

                if (_saveChanges)
                    _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validationErrors in e.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        errorMessage += Environment.NewLine + $"Propriedade: {validationError.PropertyName} / Erro: {validationError.ErrorMessage}";

                throw new Exception(errorMessage, e);
            }

            return objectToUpdate;
        }

        public void Delete(T objectToDelete)
        {
            try
            {
                if (objectToDelete == null)
                    throw new ArgumentNullException("objectToDelete nulo");

                _context.Set<T>().Remove(objectToDelete);

                if (_saveChanges)
                    _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validationErrors in e.EntityValidationErrors)               
                    foreach (var validationError in validationErrors.ValidationErrors)                   
                        errorMessage += Environment.NewLine + $"Propriedade: {validationError.PropertyName} / Erro: {validationError.ErrorMessage}";                       

                throw new Exception(errorMessage, e);
            }
            catch (Exception e)
            {
                throw new Exception(errorMessage, e);
            }
        }

        public void Delete(params object[] idToDelete)
        {
            var objectToDelete = SelectPK(idToDelete);

            Delete(objectToDelete);            
        }

        public List<T> SelectAll()
        {
            return _context.Set<T>().ToList();
        }

        public T SelectPK(params object[] idToSelect)
        {
            return _context.Set<T>().Find(idToSelect);
        }

        public void SaveChanges()
        {
            try
            {                
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validationErrors in e.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        errorMessage += Environment.NewLine + $"Propriedade: {validationError.PropertyName} / Erro: {validationError.ErrorMessage}";

                throw new Exception(errorMessage, e);
            }           
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
