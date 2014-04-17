using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPAutoEcoleDAO.Helpers
{
    public class VoitureHelper
    {
        #region Fields

        private TPAutoEcoleEntities _db;

        #endregion

        #region Thread-safe, lazy Singleton

        /// <summary>
        /// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static VoitureHelper Current
        {
            get
            {
                return Nested.VoitureHelper;
            }
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly VoitureHelper VoitureHelper = new VoitureHelper();
        }

        #endregion

        #region Fonctions publiques utiles

        /// <summary>
        /// Retourne une liste d'objet
        /// </summary>
        /// <returns>Liste d'objet métier</returns>
        public List<Voiture> GetList()
        {
            using (_db = new TPAutoEcoleEntities())
            {
                return _db.Voiture.ToList();
            }
        }

        public void Insert(Voiture voiture)
        {
            using (_db = new TPAutoEcoleEntities())
            {
                voiture.id = Guid.NewGuid();

                _db.AddToVoiture(voiture);
                _db.SaveChanges();
            }
        }

        public void Update(Voiture voiture)
        {
            using (_db = new TPAutoEcoleEntities())
            {
                _db.Voiture.Attach(voiture);
                _db.ObjectStateManager.ChangeObjectState(voiture, System.Data.EntityState.Modified);
                _db.SaveChanges();
            }
        }

        public void Refresh(Voiture voiture)
        {
            using (_db = new TPAutoEcoleEntities())
            {
                _db.Voiture.Attach(voiture);

                _db.Refresh(System.Data.Objects.RefreshMode.StoreWins, voiture);
            }
        }


        #endregion
    }
}
