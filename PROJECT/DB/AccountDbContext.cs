using Microsoft.EntityFrameworkCore;
using PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJECT.DB
{
    public class AccountDbContext : DbContext
    {
        private AccountDbContext() { }
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }
        public DbSet<AccountModel> AccountCollection { get; set; }

        public DbSet<OrderModel> OrderCollection { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                DbUtils.ConfigureDBConnection(optionsBuilder);
            }
        }

        private static void LogMessage(string msg)
        {
            DbUtils.LogMessage(msg);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("tblLogin");
                entity.Property(e => e.Username).HasColumnName("Username");
                entity.Property(e => e.Password).HasColumnName("Password");
                entity.Property(e => e.Email).HasColumnName("Email").HasMaxLength(50);

            });

            modelBuilder.Entity<OrderModel>(entity =>
            {
                entity.HasKey(e => e.OrderId);
                entity.ToTable("tblOrderDetails");
                entity.Property(e => e.DealerName).HasColumnName("DealerName").HasMaxLength(50);
                entity.Property(e => e.GSTNo).HasColumnName("GSTNo").HasMaxLength(50);
                entity.Property(e => e.Mobile).HasColumnName("Mobile").HasMaxLength(50);
                entity.Property(e => e.Email).HasColumnName("Email").HasMaxLength(50);
                entity.Property(e => e.SoldToParty).HasColumnName("SoldToParty").HasMaxLength(50);
                entity.Property(e => e.ShipToParty).HasColumnName("ShipToParty").HasMaxLength(50);
                entity.Property(e => e.Currency).HasColumnName("Currency").HasMaxLength(50);
                entity.Property(e => e.Incoterms).HasColumnName("Incoterms").HasMaxLength(50);
                entity.Property(e => e.Material).HasColumnName("Material").HasMaxLength(50);
                entity.Property(e => e.Description).HasColumnName("Description").HasMaxLength(50);
                entity.Property(e => e.Quantity).HasColumnName("Quantity").HasMaxLength(50);
                entity.Property(e => e.MinQTY).HasColumnName("MinQTY").HasMaxLength(50);
                entity.Property(e => e.QTY).HasColumnName("QTY").HasMaxLength(50);
                entity.Property(e => e.MRP).HasColumnName("MRP").HasMaxLength(50);
                entity.Property(e => e.Rate).HasColumnName("Rate").HasMaxLength(50);
                entity.Property(e => e.Amount).HasColumnName("Amount").HasMaxLength(50);
                entity.Property(e => e.Status).HasColumnName("Status").HasMaxLength(50);
                entity.Property(e => e.PaymentMode).HasColumnName("PaymentMode").HasMaxLength(50);
                entity.Property(e => e.CreatedOrderTime).HasColumnName("CreatedOrderTime");
            });
        }

        public async Task<AccountModel> GetLoginDetails(AccountModel obj)
        {
            AccountModel account = null;

            if (obj != null)
            {
                try
                {
                    account = await Task.Run(() => DoQueryLoginDetails(obj));
                }


                catch (ArgumentNullException ex) { LogMessage(ex.Message); }
                catch (Exception ex) { LogMessage(ex.Message); }

            }
            return account;
        }

        private AccountModel DoQueryLoginDetails(AccountModel objnew)
        {
            bool ok = false;
            AccountModel theList = null;

            try
            {
                string query = LoginQuery() + " WHERE UserName COLLATE FINNISH_SWEDISH_CS_AI=" + DbUtils.QuoteMe(objnew.Username) + "and Password =" + DbUtils.QuoteMe(objnew.Password);

                List<AccountModel> aiList = AccountCollection.FromSqlRaw(query).ToList();
                if (aiList != null && aiList.Count > 0)
                {
                    theList = aiList[0];
                }
                ok = true;
            }

            catch (InvalidOperationException ex) { LogMessage(ex.Message); }
            catch (ArgumentNullException ex) { LogMessage(ex.Message); }
            catch (Exception ex) { LogMessage(ex.Message); }

            finally { if (!ok) Database.CloseConnection(); }

            return theList;
        }

        private string LoginQuery()
        {
            return _selectFields;
        }

        private const string _selectFields = "SELECT Id, Username, Email, Password FROM tblLogin";
    
    
    
    
    
    
    }
}
