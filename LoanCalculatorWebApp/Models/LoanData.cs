using LoanCalculatorWebApp.Data;
using System;
using System.ComponentModel.DataAnnotations;

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
