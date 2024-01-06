﻿using aehyok.Basic.Domains;
using aehyok.Basic.Services;
using aehyok.Core;
using aehyok.Core.Domains;
using aehyok.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = System.IO.File;

namespace aehyok.Basic.SeedData
{
    public class RegionSeedData : ISeedData, IScopedDependency
    {
        private readonly IServiceScopeFactory scopeFactory;

        public RegionSeedData(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public int Order => 0;

        public string ConfigPath { get; set; } = Path.Combine(AppContext.BaseDirectory, "Templates/Region.json");

        public async Task ApplyAsync(ScheduleTask model, Func<ScheduleTask, Task> action)
        {
            using var scope = scopeFactory.CreateScope();
            var regionService = scope.ServiceProvider.GetService<IRegionService>();

            if (!await regionService.ExistsAsync(a => a.ParentId == 0))
            {
                var defaultRegion = new Region
                {
                    Name = "默认区域",
                    Code = "0",
                    ParentId = 0
                };

                await regionService.InsertAsync(defaultRegion);
            }
            //var dateTime = File.GetLastWriteTimeUtc(dataJsonPath);


        }
    }
}
