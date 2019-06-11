﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using LoanCalculatorWebApp.Data;

namespace LoanCalculatorWebApp.Models
{
    
    public class LoanData : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "total loan amount is required")]
        [Range(1, Double.MaxValue)]
        public double LoanAmount { get; set; }

        [Required(ErrorMessage = "loan term in months is required")]
        [Range(1, int.MaxValue)]
        public int LoanTerm { get; set; }

        [Range(0, Double.MaxValue)]
        public double InterestRates { get; set; }

        public string NameOfLoan { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double TotalCost { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double MonthlyBill { get; set; }

        public void SetTotalCost()
        {
           TotalCost = LoanAmount + (InterestRates / 12 * LoanTerm);
        }

        public void SetMonthlyBill() {
            MonthlyBill = TotalCost / LoanTerm;
        }

    }
}
