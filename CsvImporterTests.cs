using Covid.Models;
using Covid.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace CovidTestProject
{
    public class CsvImporterTests
    {
        [Fact]
        public void TestGetDailyCountRecords()
        {
            var csvImporter = new CsvImporter();

            var dailyCounts = csvImporter.GetDailyCountRecords();

            Assert.IsType<List<DailyCount>>(dailyCounts);
            Assert.NotEmpty(dailyCounts);
        }
    }
}



