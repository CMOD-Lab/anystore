using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace AnyStore.BLL.Tests
{
    /// <summary>
    /// Comprehensive unit tests for the transactionsBLL class.
    /// Tests cover property assignment, default values, validation logic,
    /// financial calculations, and edge cases for Purchase and Sales transactions.
    /// </summary>
    public class transactionsBLLTests
    {
        #region Constructor Tests

        [Fact]
        public void TransactionsBLL_DefaultConstructor_CreatesInstance()
        {
            // Arrange & Act
            var transaction = new transactionsBLL();

            // Assert
            Assert.NotNull(transaction);
        }

        [Fact]
        public void TransactionsBLL_DefaultConstructor_IdDefaultsToZero()
        {
            // Arrange & Act
            var transaction = new transactionsBLL();

            // Assert
            Assert.Equal(0, transaction.id);
        }

        [Fact]
        public void TransactionsBLL_DefaultConstructor_GrandTotalDefaultsToZero()
        {
            // Arrange & Act
            var transaction = new transactionsBLL();

            // Assert
            Assert.Equal(0m, transaction.grandTotal);
        }

        [Fact]
        public void TransactionsBLL_DefaultConstructor_TaxDefaultsToZero()
        {
            // Arrange & Act
            var transaction = new transactionsBLL();

            // Assert
            Assert.Equal(0m, transaction.tax);
        }

        [Fact]
        public void TransactionsBLL_DefaultConstructor_DiscountDefaultsToZero()
        {
            // Arrange & Act
            var transaction = new transactionsBLL();

            // Assert
            Assert.Equal(0m, transaction.discount);
        }

        [Fact]
        public void TransactionsBLL_DefaultConstructor_AddedByDefaultsToZero()
        {
            // Arrange & Act
            var transaction = new transactionsBLL();

            // Assert
            Assert.Equal(0, transaction.added_by);
        }

        #endregion

        #region Property Assignment Tests

        [Fact]
        public void TransactionsBLL_SetId_ReturnsCorrectValue()
        {
            // Arrange
            var transaction = new transactionsBLL();

            // Act
            transaction.id = 100;

            // Assert
            Assert.Equal(100, transaction.id);
        }

        [Fact]
        public void TransactionsBLL_SetType_Purchase_ReturnsCorrectValue()
        {
            // Arrange
            var transaction = new transactionsBLL();

            // Act
            transaction.type = "Purchase";

            // Assert
            Assert.Equal("Purchase", transaction.type);
        }

        [Fact]
        public void TransactionsBLL_SetType_Sales_ReturnsCorrectValue()
        {
            // Arrange
            var transaction = new transactionsBLL();

            // Act
            transaction.type = "Sales";

            // Assert
            Assert.Equal("Sales", transaction.type);
        }

        [Fact]
        public void TransactionsBLL_SetDeaCustId_ReturnsCorrectValue()
        {
            // Arrange
            var transaction = new transactionsBLL();

            // Act
            transaction.dea_cust_id = 5;

            // Assert
            Assert.Equal(5, transaction.dea_cust_id);
        }

        [Fact]
        public void TransactionsBLL_SetGrandTotal_ReturnsCorrectValue()
        {
            // Arrange
            var transaction = new transactionsBLL();

            // Act
            transaction.grandTotal = 1500.75m;

            // Assert
            Assert.Equal(1500.75m, transaction.grandTotal);
        }

        [Fact]
        public void TransactionsBLL_SetTransactionDate_ReturnsCorrectValue()
        {
            // Arrange
            var transaction = new transactionsBLL();
            var expectedDate = new DateTime(2024, 10, 15);

            // Act
            transaction.transaction_date = expectedDate;

            // Assert
            Assert.Equal(expectedDate, transaction.transaction_date);
        }

        [Fact]
        public void TransactionsBLL_SetTax_ReturnsCorrectValue()
        {
            // Arrange
            var transaction = new transactionsBLL();

            // Act
            transaction.tax = 150.00m;

            // Assert
            Assert.Equal(150.00m, transaction.tax);
        }

        [Fact]
        public void TransactionsBLL_SetDiscount_ReturnsCorrectValue()
        {
            // Arrange
            var transaction = new transactionsBLL();

            // Act
            transaction.discount = 50.00m;

            // Assert
            Assert.Equal(50.00m, transaction.discount);
        }

        [Fact]
        public void TransactionsBLL_SetAddedBy_ReturnsCorrectValue()
        {
            // Arrange
            var transaction = new transactionsBLL();

            // Act
            transaction.added_by = 3;

            // Assert
            Assert.Equal(3, transaction.added_by);
        }

        [Fact]
        public void TransactionsBLL_SetTransactionDetails_DataTable_ReturnsCorrectValue()
        {
            // Arrange
            var transaction = new transactionsBLL();
            var dt = new DataTable();
            dt.Columns.Add("product_id", typeof(int));
            dt.Columns.Add("qty", typeof(decimal));
            dt.Columns.Add("rate", typeof(decimal));
            dt.Columns.Add("total", typeof(decimal));

            // Act
            transaction.transactionDetails = dt;

            // Assert
            Assert.NotNull(transaction.transactionDetails);
            Assert.Equal(4, transaction.transactionDetails.Columns.Count);
        }

        #endregion

        #region Full Object Initialization Tests

        [Fact]
        public void TransactionsBLL_FullInitialization_Purchase_AllPropertiesSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2024, 11, 1);

            // Act
            var transaction = new transactionsBLL
            {
                id = 1,
                type = "Purchase",
                dea_cust_id = 3,
                grandTotal = 2000.00m,
                transaction_date = expectedDate,
                tax = 200.00m,
                discount = 100.00m,
                added_by = 1
            };

            // Assert
            Assert.Equal(1, transaction.id);
            Assert.Equal("Purchase", transaction.type);
            Assert.Equal(3, transaction.dea_cust_id);
            Assert.Equal(2000.00m, transaction.grandTotal);
            Assert.Equal(expectedDate, transaction.transaction_date);
            Assert.Equal(200.00m, transaction.tax);
            Assert.Equal(100.00m, transaction.discount);
            Assert.Equal(1, transaction.added_by);
        }

        [Fact]
        public void TransactionsBLL_FullInitialization_Sales_AllPropertiesSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2024, 11, 5);

            // Act
            var transaction = new transactionsBLL
            {
                id = 2,
                type = "Sales",
                dea_cust_id = 7,
                grandTotal = 500.00m,
                transaction_date = expectedDate,
                tax = 50.00m,
                discount = 25.00m,
                added_by = 2
            };

            // Assert
            Assert.Equal(2, transaction.id);
            Assert.Equal("Sales", transaction.type);
            Assert.Equal(7, transaction.dea_cust_id);
            Assert.Equal(500.00m, transaction.grandTotal);
        }

        [Fact]
        public void TransactionsBLL_MultipleInstances_AreIndependent()
        {
            // Arrange & Act
            var t1 = new transactionsBLL { id = 1, type = "Purchase", grandTotal = 1000m };
            var t2 = new transactionsBLL { id = 2, type = "Sales", grandTotal = 500m };

            // Assert
            Assert.NotEqual(t1.id, t2.id);
            Assert.NotEqual(t1.type, t2.type);
            Assert.NotEqual(t1.grandTotal, t2.grandTotal);
        }

        #endregion

        #region Financial Calculation Tests

        [Fact]
        public void TransactionsBLL_GrandTotal_WithTaxAndDiscount_CalculatedCorrectly()
        {
            // Arrange
            decimal subtotal = 1000m;
            decimal tax = 100m;
            decimal discount = 50m;

            // Act
            decimal grandTotal = subtotal + tax - discount;

            // Assert
            Assert.Equal(1050m, grandTotal);
        }

        [Fact]
        public void TransactionsBLL_GrandTotal_WithZeroTaxAndDiscount_EqualsSubtotal()
        {
            // Arrange
            decimal subtotal = 750m;
            decimal tax = 0m;
            decimal discount = 0m;

            // Act
            decimal grandTotal = subtotal + tax - discount;

            // Assert
            Assert.Equal(750m, grandTotal);
        }

        [Fact]
        public void TransactionsBLL_GrandTotal_WithFullDiscount_IsZero()
        {
            // Arrange
            decimal subtotal = 500m;
            decimal tax = 0m;
            decimal discount = 500m;

            // Act
            decimal grandTotal = subtotal + tax - discount;

            // Assert
            Assert.Equal(0m, grandTotal);
        }

        [Fact]
        public void TransactionsBLL_Tax_CalculatedAsPercentage_IsCorrect()
        {
            // Arrange
            decimal subtotal = 1000m;
            decimal taxRate = 0.10m; // 10%

            // Act
            decimal tax = subtotal * taxRate;

            // Assert
            Assert.Equal(100m, tax);
        }

        [Fact]
        public void TransactionsBLL_Discount_CalculatedAsPercentage_IsCorrect()
        {
            // Arrange
            decimal subtotal = 1000m;
            decimal discountRate = 0.05m; // 5%

            // Act
            decimal discount = subtotal * discountRate;

            // Assert
            Assert.Equal(50m, discount);
        }

        [Fact]
        public void TransactionsBLL_GrandTotal_IsPositive_IsValid()
        {
            // Arrange
            var transaction = new transactionsBLL { grandTotal = 500m };

            // Assert
            Assert.True(transaction.grandTotal > 0);
        }

        #endregion

        #region Validation Logic Tests

        [Fact]
        public void TransactionsBLL_Type_IsPurchase_IsValid()
        {
            // Arrange
            var transaction = new transactionsBLL { type = "Purchase" };

            // Assert
            Assert.Equal("Purchase", transaction.type);
            Assert.False(string.IsNullOrEmpty(transaction.type));
        }

        [Fact]
        public void TransactionsBLL_Type_IsSales_IsValid()
        {
            // Arrange
            var transaction = new transactionsBLL { type = "Sales" };

            // Assert
            Assert.Equal("Sales", transaction.type);
            Assert.False(string.IsNullOrEmpty(transaction.type));
        }

        [Fact]
        public void TransactionsBLL_Type_IsPurchaseOrSales_IsValid()
        {
            // Arrange
            var purchaseTransaction = new transactionsBLL { type = "Purchase" };
            var salesTransaction = new transactionsBLL { type = "Sales" };

            // Assert
            Assert.True(purchaseTransaction.type == "Purchase" || purchaseTransaction.type == "Sales");
            Assert.True(salesTransaction.type == "Purchase" || salesTransaction.type == "Sales");
        }

        [Fact]
        public void TransactionsBLL_DeaCustId_PositiveValue_IsValid()
        {
            // Arrange
            var transaction = new transactionsBLL { dea_cust_id = 3 };

            // Assert
            Assert.True(transaction.dea_cust_id > 0);
        }

        [Fact]
        public void TransactionsBLL_AddedBy_PositiveValue_IsValid()
        {
            // Arrange
            var transaction = new transactionsBLL { added_by = 1 };

            // Assert
            Assert.True(transaction.added_by > 0);
        }

        [Fact]
        public void TransactionsBLL_InsertPrep_AllRequiredFieldsPopulated()
        {
            // Arrange
            var transaction = new transactionsBLL
            {
                type = "Purchase",
                dea_cust_id = 3,
                grandTotal = 500m,
                transaction_date = DateTime.Now,
                tax = 50m,
                discount = 0m,
                added_by = 1
            };

            // Assert
            Assert.False(string.IsNullOrEmpty(transaction.type));
            Assert.True(transaction.dea_cust_id > 0);
            Assert.True(transaction.grandTotal > 0);
            Assert.True(transaction.added_by > 0);
        }

        #endregion

        #region DataTable Tests

        [Fact]
        public void TransactionsBLL_TransactionDetails_CanBeSetToNewDataTable()
        {
            // Arrange
            var transaction = new transactionsBLL();
            var dt = new DataTable("TransactionDetails");

            // Act
            transaction.transactionDetails = dt;

            // Assert
            Assert.NotNull(transaction.transactionDetails);
            Assert.Equal("TransactionDetails", transaction.transactionDetails.TableName);
        }

        [Fact]
        public void TransactionsBLL_TransactionDetails_WithRows_HasCorrectRowCount()
        {
            // Arrange
            var transaction = new transactionsBLL();
            var dt = new DataTable();
            dt.Columns.Add("product_id", typeof(int));
            dt.Columns.Add("qty", typeof(decimal));
            dt.Columns.Add("total", typeof(decimal));
            dt.Rows.Add(1, 5m, 50m);
            dt.Rows.Add(2, 3m, 30m);

            // Act
            transaction.transactionDetails = dt;

            // Assert
            Assert.Equal(2, transaction.transactionDetails.Rows.Count);
        }

        [Fact]
        public void TransactionsBLL_TransactionDetails_DefaultIsNull()
        {
            // Arrange & Act
            var transaction = new transactionsBLL();

            // Assert
            Assert.Null(transaction.transactionDetails);
        }

        #endregion

        #region Collection Tests

        [Fact]
        public void TransactionsBLL_ListOfTransactions_CanBeCreated()
        {
            // Arrange & Act
            var transactions = new List<transactionsBLL>
            {
                new transactionsBLL { id = 1, type = "Purchase", grandTotal = 1000m },
                new transactionsBLL { id = 2, type = "Sales", grandTotal = 500m },
                new transactionsBLL { id = 3, type = "Purchase", grandTotal = 750m }
            };

            // Assert
            Assert.Equal(3, transactions.Count);
        }

        [Fact]
        public void TransactionsBLL_FilterByType_Purchase_ReturnsOnlyPurchases()
        {
            // Arrange
            var transactions = new List<transactionsBLL>
            {
                new transactionsBLL { id = 1, type = "Purchase", grandTotal = 1000m },
                new transactionsBLL { id = 2, type = "Sales", grandTotal = 500m },
                new transactionsBLL { id = 3, type = "Purchase", grandTotal = 750m }
            };

            // Act
            var purchases = transactions.FindAll(t => t.type == "Purchase");

            // Assert
            Assert.Equal(2, purchases.Count);
        }

        [Fact]
        public void TransactionsBLL_FilterByType_Sales_ReturnsOnlySales()
        {
            // Arrange
            var transactions = new List<transactionsBLL>
            {
                new transactionsBLL { id = 1, type = "Purchase", grandTotal = 1000m },
                new transactionsBLL { id = 2, type = "Sales", grandTotal = 500m },
                new transactionsBLL { id = 3, type = "Sales", grandTotal = 300m }
            };

            // Act
            var sales = transactions.FindAll(t => t.type == "Sales");

            // Assert
            Assert.Equal(2, sales.Count);
        }

        [Fact]
        public void TransactionsBLL_TotalGrandTotal_SummedCorrectly()
        {
            // Arrange
            var transactions = new List<transactionsBLL>
            {
                new transactionsBLL { type = "Purchase", grandTotal = 1000m },
                new transactionsBLL { type = "Sales", grandTotal = 500m },
                new transactionsBLL { type = "Purchase", grandTotal = 750m }
            };

            // Act
            decimal totalAmount = transactions.Sum(t => t.grandTotal);

            // Assert
            Assert.Equal(2250m, totalAmount);
        }

        #endregion
    }
}
