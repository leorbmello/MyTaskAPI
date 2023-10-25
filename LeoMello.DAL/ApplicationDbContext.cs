using LeoMello.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LeoMello.DAL
{
    /// <summary>
    ///     Represents our data-access-layer (DAL), allows us to control any database entity.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly ILogger<ApplicationDbContext> logger;

        public virtual DbSet<ApplicationRole> Roles { get; set; }
        public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<ApplicationUserClaim> UserClaims { get; set; }


        // TODO: Database entities


        public ApplicationDbContext(ILogger<ApplicationDbContext> logger, DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // Do nothing.
        }

        #region CRUD Operations

        /// <summary>
        ///     Process a INSERT operation on our database.
        /// </summary>
        public async Task<bool> CreateAsync<T>(T entity, CancellationToken cancellationToken = default)
            where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                this.Add<T>(entity);
                return await SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception ex)
            {
                logger.LogCritical("CreateAsync()-> Error: {}", ex.ToString());
                return false;
            }
        }

        /// <summary>
        ///     Process multiple INSERT operations on our database.
        /// </summary>
        public async Task<bool> CreateRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default)
            where T : class
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                foreach (var entity in entities)
                {
                    this.Add<T>(entity);
                }

                return await SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception ex)
            {
                logger.LogCritical("CreateRangeAsync()-> Error: {}", ex.ToString());
                return false;
            }
        }

        /// <summary>
        ///     Process an UPDATE operation on our database.
        /// </summary>
        public async Task<bool> SaveAsync<T>(T entity, CancellationToken cancellationToken = default)
            where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                this.Update<T>(entity);
                return await SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception ex) 
            {
                logger.LogCritical("SaveAsync()-> Error: {}", ex.ToString());
                return false;
            }
        }

        /// <summary>
        ///     Process multiple UPDATE operations on our database.
        /// </summary>
        public async Task<bool> SaveRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default)
            where T : class
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                foreach (var entity in entities)
                {
                    this.Update<T>(entity);
                }

                return await SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception ex)
            {
                logger.LogCritical("SaveRangeAsync()-> Error: {}", ex.ToString());
                return false;
            }
        }

        /// <summary>
        ///     Process a DELETE operation on our database.
        /// </summary>
        public async Task<bool> DeleteAsync<T>(T entity, CancellationToken cancellationToken = default)
            where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                this.Remove<T>(entity);
                return await SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception ex)
            {
                logger.LogCritical("DeleteAsync()-> Error: {}", ex.ToString());
                return false;
            }
        }

        /// <summary>
        ///     Process multiple DELETE operations on our database.
        /// </summary>
        public async Task<bool> DeleteRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default)
            where T : class
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                foreach (var entity in entities)
                {
                    this.Remove<T>(entity);
                }

                return await SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception ex)
            {
                logger.LogCritical("DeleteRangeAsync()-> Error: {}", ex.ToString());
                return false;
            }
        }

        #endregion
    }
}