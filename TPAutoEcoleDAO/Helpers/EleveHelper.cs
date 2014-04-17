using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPAutoEcoleDAO.Helpers
{
    public class EleveHelper
    {
        #region Fields

        private TPAutoEcoleEntities _db;

        #endregion

        #region Thread-safe, lazy Singleton

        /// <summary>
        /// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static EleveHelper Current
        {
            get
            {
                return Nested.EcoleHelper;
            }
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly EleveHelper EcoleHelper = new EleveHelper();
        }

        #endregion

        #region Fonctions publiques utiles

        /// <summary>
        /// Retourne une liste d'objet
        /// </summary>
        /// <returns>Liste d'objet métier</returns>
        public List<Eleve> GetList()
        {
            using (_db = new TPAutoEcoleEntities())
            {
                return _db.Eleve.ToList();
            }
        }

        public void Insert(Eleve eleve)
        {
            using (_db = new TPAutoEcoleEntities())
            {
                eleve.id = Guid.NewGuid();

                _db.AddToEleve(eleve);
                _db.SaveChanges();
            }
        }

        public void Update(Eleve eleve)
        {
            using (_db = new TPAutoEcoleEntities())
            {
                _db.Eleve.Attach(eleve);
                _db.ObjectStateManager.ChangeObjectState(eleve, System.Data.EntityState.Modified);
                _db.SaveChanges();
            }
        }

        public int LessonCount(Eleve el)
        {
            using (_db = new TPAutoEcoleEntities())
            {
                _db.Eleve.Attach(el);

                return el.Lecon.Count();
            }
        }

        public void Refresh(Eleve eleve)
        {
            using (_db = new TPAutoEcoleEntities())
            {
                _db.Eleve.Attach(eleve);

                _db.Refresh(System.Data.Objects.RefreshMode.StoreWins, eleve);
            }
        }

        public void TestLinq()
        {
            using (_db = new TPAutoEcoleEntities())
            {
                var query = from eleve in _db.Eleve
                            select eleve;
            }
        }

        #endregion
    }
}
