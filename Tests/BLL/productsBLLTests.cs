using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AnyStore.BLL.Tests
{
    /// <summary>
    /// Comprehensive unit tests for the productsBLL class.
    /// Tests cover property assignment, default values, validation logic, 
    /// quantity calculations, and edge cases.
    /// </summary>
    public class productsBLLTests
    {
        #region Constructor Tests

        [Fact]
        public void ProductsBLL_DefaultConstructor_CreatesInstance()
        {
            // Arrange & Act
            var product = new productsBLL();

            // Assert
            Assert.NotNull(product);
        }

        [Fact]
        public void ProductsBLL_DefaultConstructor_IdDefaultsToZero()
        {
            // Arrange & Act
            var product = new productsBLL();

            // Assert
            Assert.Equal(0, product.id);
        }

        [Fact]
        public void ProductsBLL_DefaultConstructor_RateDefaultsToZero()
        {
            // Arrange & Act
            var product = new productsBLL();

            // Assert
            Assert.Equal(0m, product.rate);
        }

        [Fact]
        public void ProductsBLL_DefaultConstructor_QtyDefaultsToZero()
        {
            // Arrange & Act
            var product = new productsBLL();

            // Assert
            Assert.Equal(0m, product.qty);
        }

        [Fact]
        public void ProductsBLL_DefaultConstructor_AddedByDefaultsToZero()
        {
            // Arrange & Act
            var product = new productsBLL();

            // Assert
            Assert.Equal(0, product.added_by);
        }

        #endregion

        #region Property Assignment Tests

        [Fact]
        public void ProductsBLL_SetId_ReturnsCorrectValue()
        {
            // Arrange
            var product = new productsBLL();

            // Act
            product.id = 15;

            // Assert
            Assert.Equal(15, product.id);
        }

        [Fact]
        public void ProductsBLL_SetName_ReturnsCorrectValue()
        {
            // Arrange
            var product = new productsBLL();

            // Act
            product.name = "Widget Pro";

            // Assert
            Assert.Equal("Widget Pro", product.name);
        }

        [Fact]
        public void ProductsBLL_SetCategory_ReturnsCorrectValue()
        {
            // Arrange
            var product = new productsBLL();

            // Act
            product.category = "Electronics";

            // Assert
            Assert.Equal("Electronics", product.category);
        }

        [Fact]
        public void ProductsBLL_SetDescription_ReturnsCorrectValue()
        {
            // Arrange
            var product = new productsBLL();

            // Act
            product.description = "A high-quality widget";

            // Assert
            Assert.Equal("A high-quality widget", product.description);
        }

        [Fact]
        public void ProductsBLL_SetRate_ReturnsCorrectValue()
        {
            // Arrange
            var product = new productsBLL();

            // Act
            product.rate = 29.99m;

            // Assert
            Assert.Equal(29.99m, product.rate);
        }

        [Fact]
        public void ProductsBLL_SetQty_ReturnsCorrectValue()
        {
            // Arrange
            var product = new productsBLL();

            // Act
            product.qty = 100m;

            // Assert
            Assert.Equal(100m, product.qty);
        }

        [Fact]
        public void ProductsBLL_SetAddedDate_ReturnsCorrectValue()
        {
            // Arrange
            var product = new productsBLL();
            var expectedDate = new DateTime(2024, 4, 1);

            // Act
            product.added_date = expectedDate;

            // Assert
            Assert.Equal(expectedDate, product.added_date);
        }

        [Fact]
        public void ProductsBLL_SetAddedBy_ReturnsCorrectValue()
        {
            // Arrange
            var product = new productsBLL();

            // Act
            product.added_by = 2;

            // Assert
            Assert.Equal(2, product.added_by);
        }

        #endregion

        #region Full Object Initialization Tests

        [Fact]
        public void ProductsBLL_FullInitialization_AllPropertiesSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2024, 8, 20);

            // Act
            var product = new productsBLL
            {
                id = 5,
                name = "Super Widget",
                category = "Hardware",
                description = "Industrial grade widget",
                rate = 49.99m,
                qty = 250m,
                added_date = expectedDate,
                added_by = 1
            };

            // Assert
            Assert.Equal(5, product.id);
            Assert.Equal("Super Widget", product.name);
            Assert.Equal("Hardware", product.category);
            Assert.Equal("Industrial grade widget", product.description);
            Assert.Equal(49.99m, product.rate);
            Assert.Equal(250m, product.qty);
            Assert.Equal(expectedDate, product.added_date);
            Assert.Equal(1, product.added_by);
        }

        [Fact]
        public void ProductsBLL_MultipleInstances_AreIndependent()
        {
            // Arrange & Act
            var product1 = new productsBLL { id = 1, name = "Product A", rate = 10m };
            var product2 = new productsBLL { id = 2, name = "Product B", rate = 20m };

            // Assert
            Assert.NotEqual(product1.id, product2.id);
            Assert.NotEqual(product1.name, product2.name);
            Assert.NotEqual(product1.rate, product2.rate);
        }

        #endregion

        #region Quantity Calculation Tests

        [Fact]
        public void ProductsBLL_IncreaseQty_AddsCorrectly()
        {
            // Arrange
            var product = new productsBLL { qty = 50m };
            decimal increaseAmount = 25m;

            // Act
            product.qty += increaseAmount;

            // Assert
            Assert.Equal(75m, product.qty);
        }

        [Fact]
        public void ProductsBLL_DecreaseQty_SubtractsCorrectly()
        {
            // Arrange
            var product = new productsBLL { qty = 50m };
            decimal decreaseAmount = 20m;

            // Act
            product.qty -= decreaseAmount;

            // Assert
            Assert.Equal(30m, product.qty);
        }

        [Fact]
        public void ProductsBLL_DecreaseQty_ToZero_ResultsInZero()
        {
            // Arrange
            var product = new productsBLL { qty = 10m };

            // Act
            product.qty -= 10m;

            // Assert
            Assert.Equal(0m, product.qty);
        }

        [Fact]
        public void ProductsBLL_CalculateTotal_RateTimesQty_IsCorrect()
        {
            // Arrange
            var product = new productsBLL { rate = 15.50m, qty = 4m };

            // Act
            decimal total = product.rate * product.qty;

            // Assert
            Assert.Equal(62.00m, total);
        }

        [Fact]
        public void ProductsBLL_CalculateTotal_WithDecimalRate_IsCorrect()
        {
            // Arrange
            var product = new productsBLL { rate = 9.99m, qty = 3m };

            // Act
            decimal total = product.rate * product.qty;

            // Assert
            Assert.Equal(29.97m, total);
        }

        [Fact]
        public void ProductsBLL_IncreaseQty_FromZero_IsCorrect()
        {
            // Arrange
            var product = new productsBLL { qty = 0m };

            // Act
            product.qty += 100m;

            // Assert
            Assert.Equal(100m, product.qty);
        }

        [Fact]
        public void ProductsBLL_IncreaseQty_LargeAmount_IsCorrect()
        {
            // Arrange
            var product = new productsBLL { qty = 1000m };

            // Act
            product.qty += 5000m;

            // Assert
            Assert.Equal(6000m, product.qty);
        }

        #endregion

        #region Validation Logic Tests

        [Fact]
        public void ProductsBLL_Name_NotNullOrEmpty_IsValid()
        {
            // Arrange
            var product = new productsBLL { name = "Valid Product" };

            // Assert
            Assert.False(string.IsNullOrEmpty(product.name));
        }

        [Fact]
        public void ProductsBLL_Category_NotNullOrEmpty_IsValid()
        {
            // Arrange
            var product = new productsBLL { category = "Electronics" };

            // Assert
            Assert.False(string.IsNullOrEmpty(product.category));
        }

        [Fact]
        public void ProductsBLL_Rate_PositiveValue_IsValid()
        {
            // Arrange
            var product = new productsBLL { rate = 10.00m };

            // Assert
            Assert.True(product.rate > 0);
        }

        [Fact]
        public void ProductsBLL_Qty_NonNegativeValue_IsValid()
        {
            // Arrange
            var product = new productsBLL { qty = 0m };

            // Assert
            Assert.True(product.qty >= 0);
        }

        [Fact]
        public void ProductsBLL_Id_PositiveValue_IsValidForUpdate()
        {
            // Arrange
            var product = new productsBLL { id = 7 };

            // Assert
            Assert.True(product.id > 0);
        }

        [Fact]
        public void ProductsBLL_InsertPrep_AllRequiredFieldsPopulated()
        {
            // Arrange
            var product = new productsBLL
            {
                name = "New Product",
                category = "Category A",
                description = "Description",
                rate = 25.00m,
                qty = 50m,
                added_date = DateTime.Now,
                added_by = 1
            };

            // Assert
            Assert.False(string.IsNullOrEmpty(product.name));
            Assert.False(string.IsNullOrEmpty(product.category));
            Assert.True(product.rate > 0);
            Assert.True(product.qty >= 0);
            Assert.True(product.added_by > 0);
        }

        #endregion

        #region Edge Case Tests

        [Fact]
        public void ProductsBLL_SetRate_ZeroValue_IsAllowed()
        {
            // Arrange
            var product = new productsBLL();

            // Act
            product.rate = 0m;

            // Assert
            Assert.Equal(0m, product.rate);
        }

        [Fact]
        public void ProductsBLL_SetRate_LargeDecimal_IsAllowed()
        {
            // Arrange
            var product = new productsBLL();

            // Act
            product.rate = 99999.99m;

            // Assert
            Assert.Equal(99999.99m, product.rate);
        }

        [Fact]
        public void ProductsBLL_SetQty_FractionalValue_IsAllowed()
        {
            // Arrange
            var product = new productsBLL();

            // Act
            product.qty = 2.5m;

            // Assert
            Assert.Equal(2.5m, product.qty);
        }

        [Fact]
        public void ProductsBLL_SetName_EmptyString_IsAllowed()
        {
            // Arrange
            var product = new productsBLL();

            // Act
            product.name = string.Empty;

            // Assert
            Assert.Equal(string.Empty, product.name);
        }

        [Fact]
        public void ProductsBLL_SetDescription_LongText_IsAllowed()
        {
            // Arrange
            var product = new productsBLL();
            var longDescription = new string('x', 500);

            // Act
            product.description = longDescription;

            // Assert
            Assert.Equal(500, product.description.Length);
        }

        #endregion

        #region Collection Tests

        [Fact]
        public void ProductsBLL_ListOfProducts_CanBeCreated()
        {
            // Arrange & Act
            var products = new List<productsBLL>
            {
                new productsBLL { id = 1, name = "Product A", category = "Cat1", rate = 10m, qty = 100m },
                new productsBLL { id = 2, name = "Product B", category = "Cat2", rate = 20m, qty = 50m },
                new productsBLL { id = 3, name = "Product C", category = "Cat1", rate = 30m, qty = 75m }
            };

            // Assert
            Assert.Equal(3, products.Count);
        }

        [Fact]
        public void ProductsBLL_FilterByCategory_ReturnsCorrectProducts()
        {
            // Arrange
            var products = new List<productsBLL>
            {
                new productsBLL { id = 1, name = "Product A", category = "Electronics" },
                new productsBLL { id = 2, name = "Product B", category = "Hardware" },
                new productsBLL { id = 3, name = "Product C", category = "Electronics" }
            };

            // Act
            var electronics = products.FindAll(p => p.category == "Electronics");

            // Assert
            Assert.Equal(2, electronics.Count);
        }

        [Fact]
        public void ProductsBLL_SortByRate_AscendingOrder_IsCorrect()
        {
            // Arrange
            var products = new List<productsBLL>
            {
                new productsBLL { id = 1, name = "Expensive", rate = 100m },
                new productsBLL { id = 2, name = "Cheap", rate = 10m },
                new productsBLL { id = 3, name = "Medium", rate = 50m }
            };

            // Act
            var sorted = products.OrderBy(p => p.rate).ToList();

            // Assert
            Assert.Equal(10m, sorted[0].rate);
            Assert.Equal(50m, sorted[1].rate);
            Assert.Equal(100m, sorted[2].rate);
        }

        [Fact]
        public void ProductsBLL_TotalInventoryValue_CalculatedCorrectly()
        {
            // Arrange
            var products = new List<productsBLL>
            {
                new productsBLL { name = "A", rate = 10m, qty = 5m },
                new productsBLL { name = "B", rate = 20m, qty = 3m },
                new productsBLL { name = "C", rate = 15m, qty = 4m }
            };

            // Act
            decimal totalValue = products.Sum(p => p.rate * p.qty);

            // Assert
            Assert.Equal(170m, totalValue); // 50 + 60 + 60
        }

        [Fact]
        public void ProductsBLL_FindById_ReturnsCorrectProduct()
        {
            // Arrange
            var products = new List<productsBLL>
            {
                new productsBLL { id = 1, name = "Product One" },
                new productsBLL { id = 2, name = "Product Two" },
                new productsBLL { id = 3, name = "Product Three" }
            };

            // Act
            var found = products.Find(p => p.id == 2);

            // Assert
            Assert.NotNull(found);
            Assert.Equal("Product Two", found.name);
        }

        #endregion
    }
}
