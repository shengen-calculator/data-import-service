using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using DataImport.Common.Attribute;
using DataImport.Common.Enums;
using DataImport.Core.Domain;
using DataImport.Infrastructure.Services;
using ExcelDataReader;
using System.IO.Compression;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataImport.Core.Services
{
    [ExposeForDI]
    public class DataImportService : IDataImportService
    {
        private readonly IAppConfigService _appConfigService;
        private readonly ILogService _logService;
        private const int BatchSize = 100000;

        public DataImportService(IAppConfigService appConfigService, ILogService logService)
        {
            _appConfigService = appConfigService;
            _logService = logService;
        }

        public Task Import(Vendor vendor, byte[] bytes, string fileName)
        {
            var ext = Path.GetExtension(fileName);

            //decompression
            if (ext == ".zip")
            {
                bytes = Unzip(bytes, ref ext);
            }
            
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var stream = new MemoryStream(bytes);
            
            var currentRow = 1;

            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted, 
                Timeout = TimeSpan.MaxValue
            };

            using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                AppDbContext context = null;

                var reader = ext switch
                {
                    ".xlsx" => ExcelReaderFactory.CreateOpenXmlReader(stream),
                    ".xls" => ExcelReaderFactory.CreateReader(stream),
                    ".csv" => ExcelReaderFactory.CreateCsvReader(stream, new ExcelReaderConfiguration
                    {
                        FallbackEncoding = Encoding.GetEncoding(vendor.CodePage)
                    }),
                    _ => ExcelReaderFactory.CreateReader(stream)
                };
                
                try
                {
                    context = GetContext();
                    
                    context.ChangeTracker.AutoDetectChangesEnabled = false;

                    var obsoleteRecs = context.VendorPriceItems.Where(v => v.ProviderId == vendor.ProviderId);
                    
                    context.RemoveRange(obsoleteRecs);
                    
                    context.SaveChanges();
                    
                    do
                    {
                        while (reader.Read())
                        {
                            if (currentRow > vendor.HeaderRowCount)
                            {
                                if (!string.IsNullOrEmpty(reader.GetString(vendor.FieldOrder[0])) &&
                                    !string.IsNullOrEmpty(reader.GetString(vendor.FieldOrder[1])))
                                {
                                    var item = new VendorPriceItem
                                    {
                                        Brand = Right(reader.GetString(vendor.FieldOrder[0]), 50),
                                        Number = Right(reader.GetString(vendor.FieldOrder[1]), 25),
                                        Description = Right(reader.GetString(vendor.FieldOrder[2]), 80),
                                        Price = decimal.Round(Convert.ToDecimal(reader.GetString(vendor.FieldOrder[3])
                                            .Replace(',', '.')), 2),
                                        VendorNumber = Right(reader.GetString(vendor.FieldOrder[4]), 25),
                                        VendorId = (int) vendor.InternalId,
                                        ProviderId = vendor.ProviderId
                                    };
                                    if (vendor.BranchOrder.Length > 0)
                                    {
                                        item.BranchOne = Right(reader.GetString(vendor.BranchOrder[0]), 10);
                                    }
                                    if (vendor.BranchOrder.Length > 1)
                                    {
                                        item.BranchTwo = Right(reader.GetString(vendor.BranchOrder[1]), 10);
                                    } 
                                    if (vendor.BranchOrder.Length > 2)
                                    {
                                        item.BranchThree = Right(reader.GetString(vendor.BranchOrder[2]), 10);
                                    } 
                                    if (vendor.BranchOrder.Length > 3)
                                    {
                                        item.BranchFour = Right(reader.GetString(vendor.BranchOrder[3]), 10);
                                    } 
                                    if (vendor.BranchOrder.Length > 4)
                                    {
                                        item.BranchFive = Right(reader.GetString(vendor.BranchOrder[4]), 10);
                                    } 
                                    if (vendor.BranchOrder.Length > 5)
                                    {
                                        item.BranchSix = Right(reader.GetString(vendor.BranchOrder[5]), 10);
                                    } 
                                    if (vendor.BranchOrder.Length > 6)
                                    {
                                        item.BranchSeven = Right(reader.GetString(vendor.BranchOrder[6]), 10);
                                    } 
                                    if(vendor.BranchOrder.Length == 0)
                                    {
                                        item.Availability = Right(reader.GetString(vendor.FieldOrder[4]), 10);
                                    }
                                    
                                    context = AddToContext(context, item, currentRow, BatchSize, true);
                                }

                            }

                            currentRow++;
                        }
                    } while (reader.NextResult());


                    context.SaveChanges();
                    _logService.Log(Level.Info,$"File imported successfully", vendor.Name);
                }
                catch(Exception ex)
                {
                    _logService.Log(Level.Error,$"Import file error: {ex.Message}", vendor.Name);
                }
                finally
                {
                    stream.Dispose();
                    reader.Dispose();
                    context?.Dispose();
                }

                scope.Complete();
            }
            
            return Task.CompletedTask;
        }

        private AppDbContext AddToContext(AppDbContext context, VendorPriceItem entity, int count, int commitCount, bool recreateContext)
        {
            context.Set<VendorPriceItem>().Add(entity);

            if (count % commitCount != 0) return context;
            context.SaveChanges();
            if (!recreateContext) return context;
            context.Dispose();
            context = GetContext();
            context.ChangeTracker.AutoDetectChangesEnabled = false;

            return context;
        }

        private static string Right(string source, int length)
        {
            return source.Length <= length ? source : source.Substring(0, length);
        }

        private AppDbContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_appConfigService.ConnectionStringSettings.DefaultConnection);
            return new AppDbContext(optionsBuilder.Options);
        }

        private static byte[] Unzip(byte[] zippedBuffer, ref string ext)
        {
            using var zippedStream = new MemoryStream(zippedBuffer);
            using var archive = new ZipArchive(zippedStream);
            var entry = archive.Entries.FirstOrDefault();
            if (entry == null) return null;
            ext = Path.GetExtension(entry.Name);
            using var unzippedEntryStream = entry.Open();
            using var ms = new MemoryStream();
            unzippedEntryStream.CopyTo(ms);
            return ms.ToArray();
        }

        public Task StartHandler(Vendor vendor)
        {
            throw new System.NotImplementedException();
        }
    }
}