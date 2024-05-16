using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using JoaoDiasDev.ListGenius.UI.Server.Data;

namespace JoaoDiasDev.ListGenius.UI.Server.Controllers
{
    public partial class Exportjoaodiasdev_list_geniusController : ExportController
    {
        private readonly joaodiasdev_list_geniusContext context;
        private readonly joaodiasdev_list_geniusService service;

        public Exportjoaodiasdev_list_geniusController(joaodiasdev_list_geniusContext context, joaodiasdev_list_geniusService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/joaodiasdev_list_genius/groups/csv")]
        [HttpGet("/export/joaodiasdev_list_genius/groups/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGroupsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetGroups(), Request.Query, false), fileName);
        }

        [HttpGet("/export/joaodiasdev_list_genius/groups/excel")]
        [HttpGet("/export/joaodiasdev_list_genius/groups/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGroupsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetGroups(), Request.Query, false), fileName);
        }

        [HttpGet("/export/joaodiasdev_list_genius/products/csv")]
        [HttpGet("/export/joaodiasdev_list_genius/products/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProductsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProducts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/joaodiasdev_list_genius/products/excel")]
        [HttpGet("/export/joaodiasdev_list_genius/products/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProductsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProducts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/joaodiasdev_list_genius/productslists/csv")]
        [HttpGet("/export/joaodiasdev_list_genius/productslists/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProductsListsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProductsLists(), Request.Query, false), fileName);
        }

        [HttpGet("/export/joaodiasdev_list_genius/productslists/excel")]
        [HttpGet("/export/joaodiasdev_list_genius/productslists/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProductsListsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProductsLists(), Request.Query, false), fileName);
        }

        [HttpGet("/export/joaodiasdev_list_genius/sharedproducts/csv")]
        [HttpGet("/export/joaodiasdev_list_genius/sharedproducts/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSharedProductsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSharedProducts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/joaodiasdev_list_genius/sharedproducts/excel")]
        [HttpGet("/export/joaodiasdev_list_genius/sharedproducts/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSharedProductsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSharedProducts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/joaodiasdev_list_genius/subgroups/csv")]
        [HttpGet("/export/joaodiasdev_list_genius/subgroups/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubGroupsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSubGroups(), Request.Query, false), fileName);
        }

        [HttpGet("/export/joaodiasdev_list_genius/subgroups/excel")]
        [HttpGet("/export/joaodiasdev_list_genius/subgroups/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSubGroupsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSubGroups(), Request.Query, false), fileName);
        }

        [HttpGet("/export/joaodiasdev_list_genius/users/csv")]
        [HttpGet("/export/joaodiasdev_list_genius/users/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUsers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/joaodiasdev_list_genius/users/excel")]
        [HttpGet("/export/joaodiasdev_list_genius/users/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUsers(), Request.Query, false), fileName);
        }
    }
}
