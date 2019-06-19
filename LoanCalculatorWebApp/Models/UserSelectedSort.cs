﻿using LoanCalculatorWebApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanCalculatorWebApp.Models
{
    public enum LoanDataProperties { LoanAmount, LoanTerm, InterestRate, TotalCost, MonthlyBill, Id }

    public enum SortOrder { Ascending, Descending }

    public class UserSelectedSort : IEntity
    {

        public string Property { get; set; }
        public string SortOrder { get; set; }

        public IEnumerable<SelectListItem> GetPropertiesList()
        {
            return
                Enum.GetValues(typeof(LoanDataProperties)).Cast<LoanDataProperties>().Select(p => new SelectListItem
                {
                    Text = p.ToString(),
                    Value = p.ToString()
                }).ToList();
        }

        public IEnumerable<SelectListItem> GetSortOrder()
        {
            return
                Enum.GetValues(typeof(SortOrder)).Cast<SortOrder>().Select(p => new SelectListItem
                {
                    Text = p.ToString(),
                    Value = p.ToString()
                }).ToList();
        }

    }
}
