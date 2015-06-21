using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoFit.Models;

namespace GoFit.Tests.MockContexts
{
    /// <summary>
    /// Overrides the DbSet type Find method so that Moq can call find without the 
    /// mock entity objects having primary keys
    /// </summary>
    /// <typeparam name="TEntity">The entity type to override Find for</typeparam>
    public class DbSetOverrideFind<TEntity> : DbSet<workout>
    {
        public override workout Find(params object[] keyValues)
        {
            var id = (int)keyValues.Single();
            return this.SingleOrDefault(w => w.id == id);
        }
    }

    //class DbSetOverrideFind<TEntity> : DbSet<TEntity>
    //{
    //    public override TEntity Find(params object[] keyValues)
    //    {
    //        var id = (int)keyValues.Single();
    //        return this.SingleOrDefault(w => w.);
    //    }
    //}
}
