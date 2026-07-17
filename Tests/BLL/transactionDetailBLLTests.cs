using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AnyStore.BLL.Tests
{
    /// <summary>
    /// Comprehensive unit tests for the transactionDetailBLL class.
    /// Tests cover property assignment, default values, validation logic,
    /// total calculations, and edge cases for transaction line items.
    /// </summary>
    public class transactionDetailBLLTests
    {
        #region Constructor Tests

        [Fact]
        public void TransactionDetailBLL_DefaultConstructor_CreatesInstance()
        {
            // Arrange & Act
            var td = new transactionDetailBLL();

            // Assert
            Assert.NotNull(td);
        }

        [Fact]
        public void TransactionDetailBLL_DefaultConstructor_IdDefaultsToZero()
        {
            // Arrange & Act
            var td = new transactionDetailBLL();

            // Assert
            Assert.Equal(0, td.id);
        }

        [Fact]
        public void TransactionDetailBLL_DefaultConstructor_ProductIdDefaultsToZero()
        {
            // Arrange & Act
            var td = new transactionDetailBLL();

            // Assert
            Assert.Equal(0, td.product_id);
        }

        [Fact]
        public void TransactionDetailBLL_DefaultConstructor_RateDefaultsToZero()
        {
            // Arrange & Act
            var td = new transactionDetailBLL();

            // Assert
            Assert.Equal(0m, td.rate);
        }

        [Fact]
        public void TransactionDetailBLL_DefaultConstructor_QtyDefaultsToZero()
        {
            // Arrange & Act
            var td = new transactionDetailBLL();

            // Assert
            Assert.Equal(0m, td.qty);
        }

        [Fact]
        public void TransactionDetailBLL_DefaultConstructor_TotalDefaultsToZero()
        {
            // Arrange & Act
            var td = new transactionDetailBLL();

            // Assert
            Assert.Equal(0m, td.total);
        }

        [Fact]
        public void TransactionDetailBLL_DefaultConstructor_DeaCustIdDefaultsToZero()
        {
            // Arrange & Act
            var td = new transactionDetailBLL();

            // Assert
            Assert.Equal(0, td.dea_cust_id);
        }

        [Fact]
        public void TransactionDetailBLL_DefaultConstructor_AddedByDefaultsToZero()
        {
            // Arrange & Act
            var td = new transactionDetailBLL();

            // Assert
            Assert.Equal(0, td.added_by);
        }

        #endregion

        #region Property Assignment Tests

        [Fact]
        public void TransactionDetailBLL_SetId_ReturnsCorrectValue()
        {
            // Arrange
            var td = new transactionDetailBLL();

            // Act
            td.id = 50;

            // Assert
            Assert.Equal(50, td.id);
        }

        [Fact]
        public void TransactionDetailBLL_SetProductId_ReturnsCorrectValue()
        {
            // Arrange
            var td = new transactionDetailBLL();

            // Act
            td.product_id = 8;

            // Assert
            Assert.Equal(8, td.product_id);
        }

        [Fact]
        public void TransactionDetailBLL_SetRate_ReturnsCorrectValue()
        {
            // Arrange
            var td = new transactionDetailBLL();

            // Act
            td.rate = 75.50m;

            // Assert
            Assert.Equal(75.50m, td.rate);
        }

        [Fact]
        public void TransactionDetailBLL_SetQty_ReturnsCorrectValue()
        {
            // Arrange
            var td = new transactionDetailBLL();

            // Act
            td.qty = 10m;

            // Assert
            Assert.Equal(10m, td.qty);
        }

        [Fact]
        public void TransactionDetailBLL_SetTotal_ReturnsCorrectValue()
        {
            // Arrange
            var td = new transactionDetailBLL();

            // Act
            td.total = 755.00m;

            // Assert
            Assert.Equal(755.00m, td.total);
        }

        [Fact]
        public void TransactionDetailBLL_SetDeaCustId_ReturnsCorrectValue()
        {
            // Arrange
            var td = new transactionDetailBLL();

            // Act
            td.dea_cust_id = 4;

            // Assert
            Assert.Equal(4, td.dea_cust_id);
        }

        [Fact]
        public void TransactionDetailBLL_SetAddedDate_ReturnsCorrectValue()
        {
            // Arrange
            var td = new transactionDetailBLL();
            var expectedDate = new DateTime(2024, 12, 1);

            // Act
            td.added_date = expectedDate;

            // Assert
            Assert.Equal(expectedDate, td.added_date);
        }

        [Fact]
        public void TransactionDetailBLL_SetAddedBy_ReturnsCorrectValue()
        {
            // Arrange
            var td = new transactionDetailBLL();

            // Act
            td.added_by = 2;

            // Assert
            Assert.Equal(2, td.added_by);
        }

        #endregion

        #region Full Object Initialization Tests

        [Fact]
        public void TransactionDetailBLL_FullInitialization_AllPropertiesSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2024, 12, 15);

            // Act
            var td = new transactionDetailBLL
            {
                id = 10,
                product_id = 3,
                rate = 100m,
                qty = 5m,
                total = 500m,
                dea_cust_id = 2,
                added_date = expectedDate,
                added_by = 1
            };

            // Assert
            Assert.Equal(10, td.id);
            Assert.Equal(3, td.product_id);
            Assert.Equal(100m, td.rate);
            Assert.Equal(5m, td.qty);
            Assert.Equal(500m, td.total);
            Assert.Equal(2, td.dea_cust_id);
            Assert.Equal(expectedDate, td.added_date);
            Assert.Equal(1, td.added_by);
        }

        [Fact]
        public void TransactionDetailBLL_MultipleInstances_AreIndependent()
        {
            // Arrange & Act
            var td1 = new transactionDetailBLL { id = 1, product_id = 1, qty = 5m, rate = 10m };
            var td2 = new transactionDetailBLL { id = 2, product_id = 2, qty = 3m, rate = 20m };

            // Assert
            Assert.NotEqual(td1.id, td2.id);
            Assert.NotEqual(td1.product_id, td2.product_id);
            Assert.NotEqual(td1.qty, td2.qty);
            Assert.NotEqual(td1.rate, td2.rate);
        }

        #endregion

        #region Total Calculation Tests

        [Fact]
        public void TransactionDetailBLL_Total_EqualsRate_Times_Qty()
        {
            // Arrange
            var td = new transactionDetailBLL { rate = 100m, qty = 5m };

            // Act
            td.total = td.rate * td.qty;

            // Assert
            Assert.Equal(500m, td.total);
        }

        [Fact]
        public void TransactionDetailBLL_Total_WithDecimalRate_IsCorrect()
        {
            // Arrange
            var td = new transactionDetailBLL { rate = 9.99m, qty = 3m };

            // Act
            td.total = td.rate * td.qty;

            // Assert
            Assert.Equal(29.97m, td.total);
        }

        [Fact]
        public void TransactionDetailBLL_Total_WithDecimalQty_IsCorrect()
        {
            // Arrange
            var td = new transactionDetailBLL { rate = 50m, qty = 2.5m };

            // Act
            td.total = td.rate * td.qty;

            // Assert
            Assert.Equal(125m, td.total);
        }

        [Fact]
        public void TransactionDetailBLL_Total_WithZeroQty_IsZero()
        {
            // Arrange
            var td = new transactionDetailBLL { rate = 100m, qty = 0m };

            // Act
            td.total = td.rate * td.qty;

            // Assert
            Assert.Equal(0m, td.total);
        }

        [Fact]
        public void TransactionDetailBLL_Total_WithZeroRate_IsZero()
        {
            // Arrange
            var td = new transactionDetailBLL { rate = 0m, qty = 10m };

            // Act
            td.total = td.rate * td.qty;

            // Assert
            Assert.Equal(0m, td.total);
        }

        [Fact]
        public void TransactionDetailBLL_Total_WithLargeValues_IsCorrect()
        {
            // Arrange
            var td = new transactionDetailBLL { rate = 999.99m, qty = 100m };

            // Act
            td.total = td.rate * td.qty;

            // Assert
            Assert.Equal(99999m, td.total);
        }

        [Fact]
        public void TransactionDetailBLL_Total_IsGreaterThanRate_WhenQtyGreaterThanOne()
        {
            // Arrange
            var td = new transactionDetailBLL { rate = 50m, qty = 3m };

            // Act
            td.total = td.rate * td.qty;

            // Assert
            Assert.True(td.total > td.rate);
        }

        #endregion

        #region Validation Logic Tests

        [Fact]
        public void TransactionDetailBLL_ProductId_PositiveValue_IsValid()
        {
            // Arrange
            var td = new transactionDetailBLL { product_id = 2 };

            // Assert
            Assert.True(td.product_id > 0);
        }

        [Fact]
        public void TransactionDetailBLL_Rate_PositiveValue_IsValid()
        {
            // Arrange
            var td = new transactionDetailBLL { rate = 100m };

            // Assert
            Assert.True(td.rate > 0);
        }

        [Fact]
        public void TransactionDetailBLL_Qty_PositiveValue_IsValid()
        {
            // Arrange
            var td = new transactionDetailBLL { qty = 5m };

            // Assert
            Assert.True(td.qty > 0);
        }

        [Fact]
        public void TransactionDetailBLL_Total_PositiveValue_IsValid()
        {
            // Arrange
            var td = new transactionDetailBLL { total = 500m };

            // Assert
            Assert.True(td.total > 0);
        }

        [Fact]
        public void TransactionDetailBLL_DeaCustId_PositiveValue_IsValid()
        {
            // Arrange
            var td = new transactionDetailBLL { dea_cust_id = 3 };

            // Assert
            Assert.True(td.dea_cust_id > 0);
        }

        [Fact]
        public void TransactionDetailBLL_AddedBy_PositiveValue_IsValid()
        {
            // Arrange
            var td = new transactionDetailBLL { added_by = 1 };

            // Assert
            Assert.True(td.added_by > 0);
        }

        [Fact]
        public void TransactionDetailBLL_InsertPrep_AllRequiredFieldsPopulated()
        {
            // Arrange
            var td = new transactionDetailBLL
            {
                product_id = 2,
                rate = 100m,
                qty = 5m,
                total = 500m,
                dea_cust_id = 3,
                added_date = DateTime.Now,
                added_by = 1
            };

            // Assert
            Assert.True(td.product_id > 0);
            Assert.True(td.rate > 0);
            Assert.True(td.qty > 0);
            Assert.True(td.total > 0);
            Assert.True(td.dea_cust_id > 0);
            Assert.True(td.added_by > 0);
        }

        #endregion

        #region Edge Case Tests

        [Fact]
        public void TransactionDetailBLL_SetRate_FractionalValue_IsAllowed()
        {
            // Arrange
            var td = new transactionDetailBLL();

            // Act
            td.rate = 0.01m;

            // Assert
            Assert.Equal(0.01m, td.rate);
        }

        [Fact]
        public void TransactionDetailBLL_SetQty_FractionalValue_IsAllowed()
        {
            // Arrange
            var td = new transactionDetailBLL();

            // Act
            td.qty = 0.5m;

            // Assert
            Assert.Equal(0.5m, td.qty);
        }

        [Fact]
        public void TransactionDetailBLL_SetId_MaxInt_IsAllowed()
        {
            // Arrange
            var td = new transactionDetailBLL();

            // Act
            td.id = int.MaxValue;

            // Assert
            Assert.Equal(int.MaxValue, td.id);
        }

        [Fact]
        public void TransactionDetailBLL_SetAddedDate_Today_IsAllowed()
        {
            // Arrange
            var td = new transactionDetailBLL();
            var today = DateTime.Today;

            // Act
            td.added_date = today;

            // Assert
            Assert.Equal(today, td.added_date);
        }

        #endregion

        #region Collection Tests

        [Fact]
        public void TransactionDetailBLL_ListOfDetails_CanBeCreated()
        {
            // Arrange & Act
            var details = new List<transactionDetailBLL>
            {
                new transactionDetailBLL { id = 1, product_id = 1, rate = 10m, qty = 5m, total = 50m },
                new transactionDetailBLL { id = 2, product_id = 2, rate = 20m, qty = 3m, total = 60m },
                new transactionDetailBLL { id = 3, product_id = 3, rate = 15m, qty = 4m, total = 60m }
            };

            // Assert
            Assert.Equal(3, details.Count);
        }

        [Fact]
        public void TransactionDetailBLL_SumOfTotals_IsCorrect()
        {
            // Arrange
            var details = new List<transactionDetailBLL>
            {
                new transactionDetailBLL { product_id = 1, rate = 10m, qty = 5m, total = 50m },
                new transactionDetailBLL { product_id = 2, rate = 20m, qty = 3m, total = 60m },
                new transactionDetailBLL { product_id = 3, rate = 15m, qty = 4m, total = 60m }
            };

            // Act
            decimal grandTotal = details.Sum(td => td.total);

            // Assert
            Assert.Equal(170m, grandTotal);
        }

        [Fact]
        public void TransactionDetailBLL_FilterByProductId_ReturnsCorrectDetails()
        {
            // Arrange
            var details = new List<transactionDetailBLL>
            {
                new transactionDetailBLL { id = 1, product_id = 1, total = 50m },
                new transactionDetailBLL { id = 2, product_id = 2, total = 60m },
                new transactionDetailBLL { id = 3, product_id = 1, total = 30m }
            };

            // Act
            var product1Details = details.FindAll(td => td.product_id == 1);

            // Assert
            Assert.Equal(2, product1Details.Count);
        }

        [Fact]
        public void TransactionDetailBLL_FilterByDeaCustId_ReturnsCorrectDetails()
        {
            // Arrange
            var details = new List<transactionDetailBLL>
            {
                new transactionDetailBLL { id = 1, dea_cust_id = 1, total = 50m },
                new transactionDetailBLL { id = 2, dea_cust_id = 2, total = 60m },
                new transactionDetailBLL { id = 3, dea_cust_id = 1, total = 30m }
            };

            // Act
            var customer1Details = details.FindAll(td => td.dea_cust_id == 1);

            // Assert
            Assert.Equal(2, customer1Details.Count);
        }

        [Fact]
        public void TransactionDetailBLL_TotalCalculation_MatchesRateTimesQty_ForAllItems()
        {
            // Arrange
            var details = new List<transactionDetailBLL>
            {
                new transactionDetailBLL { product_id = 1, rate = 10m, qty = 5m },
                new transactionDetailBLL { product_id = 2, rate = 20m, qty = 3m },
                new transactionDetailBLL { product_id = 3, rate = 15m, qty = 4m }
            };

            // Act - calculate totals
            foreach (var td in details)
            {
                td.total = td.rate * td.qty;
            }

            // Assert
            Assert.Equal(50m, details[0].total);
            Assert.Equal(60m, details[1].total);
            Assert.Equal(60m, details[2].total);
        }

        #endregion
    }
}
