using System;
using System.Collections.Generic;
using Xunit;

namespace AnyStore.BLL.Tests
{
    /// <summary>
    /// Comprehensive unit tests for the DeaCustBLL class.
    /// Tests cover property assignment, default values, validation logic, and edge cases
    /// for both Dealer and Customer entities.
    /// </summary>
    public class DeaCustBLLTests
    {
        #region Constructor Tests

        [Fact]
        public void DeaCustBLL_DefaultConstructor_CreatesInstance()
        {
            // Arrange & Act
            var dc = new DeaCustBLL();

            // Assert
            Assert.NotNull(dc);
        }

        [Fact]
        public void DeaCustBLL_DefaultConstructor_IdDefaultsToZero()
        {
            // Arrange & Act
            var dc = new DeaCustBLL();

            // Assert
            Assert.Equal(0, dc.id);
        }

        [Fact]
        public void DeaCustBLL_DefaultConstructor_AddedByDefaultsToZero()
        {
            // Arrange & Act
            var dc = new DeaCustBLL();

            // Assert
            Assert.Equal(0, dc.added_by);
        }

        #endregion

        #region Property Assignment Tests

        [Fact]
        public void DeaCustBLL_SetId_ReturnsCorrectValue()
        {
            // Arrange
            var dc = new DeaCustBLL();

            // Act
            dc.id = 10;

            // Assert
            Assert.Equal(10, dc.id);
        }

        [Fact]
        public void DeaCustBLL_SetType_Dealer_ReturnsCorrectValue()
        {
            // Arrange
            var dc = new DeaCustBLL();

            // Act
            dc.type = "Dealer";

            // Assert
            Assert.Equal("Dealer", dc.type);
        }

        [Fact]
        public void DeaCustBLL_SetType_Customer_ReturnsCorrectValue()
        {
            // Arrange
            var dc = new DeaCustBLL();

            // Act
            dc.type = "Customer";

            // Assert
            Assert.Equal("Customer", dc.type);
        }

        [Fact]
        public void DeaCustBLL_SetName_ReturnsCorrectValue()
        {
            // Arrange
            var dc = new DeaCustBLL();

            // Act
            dc.name = "ABC Suppliers";

            // Assert
            Assert.Equal("ABC Suppliers", dc.name);
        }

        [Fact]
        public void DeaCustBLL_SetEmail_ReturnsCorrectValue()
        {
            // Arrange
            var dc = new DeaCustBLL();

            // Act
            dc.email = "supplier@abc.com";

            // Assert
            Assert.Equal("supplier@abc.com", dc.email);
        }

        [Fact]
        public void DeaCustBLL_SetContact_ReturnsCorrectValue()
        {
            // Arrange
            var dc = new DeaCustBLL();

            // Act
            dc.contact = "123-456-7890";

            // Assert
            Assert.Equal("123-456-7890", dc.contact);
        }

        [Fact]
        public void DeaCustBLL_SetAddress_ReturnsCorrectValue()
        {
            // Arrange
            var dc = new DeaCustBLL();

            // Act
            dc.address = "789 Business Park";

            // Assert
            Assert.Equal("789 Business Park", dc.address);
        }

        [Fact]
        public void DeaCustBLL_SetAddedDate_ReturnsCorrectValue()
        {
            // Arrange
            var dc = new DeaCustBLL();
            var expectedDate = new DateTime(2024, 3, 20);

            // Act
            dc.added_date = expectedDate;

            // Assert
            Assert.Equal(expectedDate, dc.added_date);
        }

        [Fact]
        public void DeaCustBLL_SetAddedBy_ReturnsCorrectValue()
        {
            // Arrange
            var dc = new DeaCustBLL();

            // Act
            dc.added_by = 3;

            // Assert
            Assert.Equal(3, dc.added_by);
        }

        #endregion

        #region Full Object Initialization Tests

        [Fact]
        public void DeaCustBLL_FullInitialization_Dealer_AllPropertiesSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2024, 5, 10);

            // Act
            var dc = new DeaCustBLL
            {
                id = 1,
                type = "Dealer",
                name = "XYZ Wholesale",
                email = "xyz@wholesale.com",
                contact = "555-0100",
                address = "100 Industrial Road",
                added_date = expectedDate,
                added_by = 1
            };

            // Assert
            Assert.Equal(1, dc.id);
            Assert.Equal("Dealer", dc.type);
            Assert.Equal("XYZ Wholesale", dc.name);
            Assert.Equal("xyz@wholesale.com", dc.email);
            Assert.Equal("555-0100", dc.contact);
            Assert.Equal("100 Industrial Road", dc.address);
            Assert.Equal(expectedDate, dc.added_date);
            Assert.Equal(1, dc.added_by);
        }

        [Fact]
        public void DeaCustBLL_FullInitialization_Customer_AllPropertiesSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2024, 7, 15);

            // Act
            var dc = new DeaCustBLL
            {
                id = 2,
                type = "Customer",
                name = "John Customer",
                email = "john@customer.com",
                contact = "555-0200",
                address = "200 Residential Lane",
                added_date = expectedDate,
                added_by = 2
            };

            // Assert
            Assert.Equal(2, dc.id);
            Assert.Equal("Customer", dc.type);
            Assert.Equal("John Customer", dc.name);
        }

        [Fact]
        public void DeaCustBLL_MultipleInstances_AreIndependent()
        {
            // Arrange & Act
            var dealer = new DeaCustBLL { id = 1, type = "Dealer", name = "Dealer Co" };
            var customer = new DeaCustBLL { id = 2, type = "Customer", name = "Customer Inc" };

            // Assert
            Assert.NotEqual(dealer.id, customer.id);
            Assert.NotEqual(dealer.type, customer.type);
            Assert.NotEqual(dealer.name, customer.name);
        }

        #endregion

        #region Validation Logic Tests

        [Fact]
        public void DeaCustBLL_Type_IsDealer_IsValid()
        {
            // Arrange
            var dc = new DeaCustBLL { type = "Dealer" };

            // Assert
            Assert.Equal("Dealer", dc.type);
            Assert.False(string.IsNullOrEmpty(dc.type));
        }

        [Fact]
        public void DeaCustBLL_Type_IsCustomer_IsValid()
        {
            // Arrange
            var dc = new DeaCustBLL { type = "Customer" };

            // Assert
            Assert.Equal("Customer", dc.type);
            Assert.False(string.IsNullOrEmpty(dc.type));
        }

        [Fact]
        public void DeaCustBLL_Name_NotNullOrEmpty_IsValid()
        {
            // Arrange
            var dc = new DeaCustBLL { name = "Valid Name" };

            // Assert
            Assert.False(string.IsNullOrEmpty(dc.name));
        }

        [Fact]
        public void DeaCustBLL_Id_PositiveValue_IsValidForUpdate()
        {
            // Arrange
            var dc = new DeaCustBLL { id = 5 };

            // Assert
            Assert.True(dc.id > 0);
        }

        [Fact]
        public void DeaCustBLL_Id_PositiveValue_IsValidForDelete()
        {
            // Arrange
            var dc = new DeaCustBLL { id = 3 };

            // Assert
            Assert.True(dc.id > 0);
        }

        [Fact]
        public void DeaCustBLL_AddedBy_PositiveValue_IsValid()
        {
            // Arrange
            var dc = new DeaCustBLL { added_by = 1 };

            // Assert
            Assert.True(dc.added_by > 0);
        }

        [Fact]
        public void DeaCustBLL_InsertPrep_AllRequiredFieldsPopulated()
        {
            // Arrange
            var dc = new DeaCustBLL
            {
                type = "Dealer",
                name = "Test Dealer",
                email = "test@dealer.com",
                contact = "555-1111",
                address = "Test Address",
                added_date = DateTime.Now,
                added_by = 1
            };

            // Assert
            Assert.False(string.IsNullOrEmpty(dc.type));
            Assert.False(string.IsNullOrEmpty(dc.name));
            Assert.True(dc.added_by > 0);
        }

        #endregion

        #region Edge Case Tests

        [Fact]
        public void DeaCustBLL_SetId_ZeroValue_IsAllowed()
        {
            // Arrange
            var dc = new DeaCustBLL();

            // Act
            dc.id = 0;

            // Assert
            Assert.Equal(0, dc.id);
        }

        [Fact]
        public void DeaCustBLL_SetName_EmptyString_IsAllowed()
        {
            // Arrange
            var dc = new DeaCustBLL();

            // Act
            dc.name = string.Empty;

            // Assert
            Assert.Equal(string.Empty, dc.name);
        }

        [Fact]
        public void DeaCustBLL_SetEmail_EmptyString_IsAllowed()
        {
            // Arrange
            var dc = new DeaCustBLL();

            // Act
            dc.email = string.Empty;

            // Assert
            Assert.Equal(string.Empty, dc.email);
        }

        [Fact]
        public void DeaCustBLL_SetAddedDate_Today_IsAllowed()
        {
            // Arrange
            var dc = new DeaCustBLL();
            var today = DateTime.Today;

            // Act
            dc.added_date = today;

            // Assert
            Assert.Equal(today, dc.added_date);
        }

        [Fact]
        public void DeaCustBLL_SetId_MaxInt_IsAllowed()
        {
            // Arrange
            var dc = new DeaCustBLL();

            // Act
            dc.id = int.MaxValue;

            // Assert
            Assert.Equal(int.MaxValue, dc.id);
        }

        #endregion

        #region Collection Tests

        [Fact]
        public void DeaCustBLL_ListOfDealersAndCustomers_CanBeCreated()
        {
            // Arrange & Act
            var entities = new List<DeaCustBLL>
            {
                new DeaCustBLL { id = 1, type = "Dealer", name = "Dealer A" },
                new DeaCustBLL { id = 2, type = "Customer", name = "Customer B" },
                new DeaCustBLL { id = 3, type = "Dealer", name = "Dealer C" }
            };

            // Assert
            Assert.Equal(3, entities.Count);
        }

        [Fact]
        public void DeaCustBLL_FilterByType_Dealer_ReturnsOnlyDealers()
        {
            // Arrange
            var entities = new List<DeaCustBLL>
            {
                new DeaCustBLL { id = 1, type = "Dealer", name = "Dealer A" },
                new DeaCustBLL { id = 2, type = "Customer", name = "Customer B" },
                new DeaCustBLL { id = 3, type = "Dealer", name = "Dealer C" }
            };

            // Act
            var dealers = entities.FindAll(dc => dc.type == "Dealer");

            // Assert
            Assert.Equal(2, dealers.Count);
        }

        [Fact]
        public void DeaCustBLL_FilterByType_Customer_ReturnsOnlyCustomers()
        {
            // Arrange
            var entities = new List<DeaCustBLL>
            {
                new DeaCustBLL { id = 1, type = "Dealer", name = "Dealer A" },
                new DeaCustBLL { id = 2, type = "Customer", name = "Customer B" },
                new DeaCustBLL { id = 3, type = "Customer", name = "Customer C" }
            };

            // Act
            var customers = entities.FindAll(dc => dc.type == "Customer");

            // Assert
            Assert.Equal(2, customers.Count);
        }

        [Fact]
        public void DeaCustBLL_FindById_ReturnsCorrectEntity()
        {
            // Arrange
            var entities = new List<DeaCustBLL>
            {
                new DeaCustBLL { id = 1, name = "Entity One" },
                new DeaCustBLL { id = 2, name = "Entity Two" },
                new DeaCustBLL { id = 3, name = "Entity Three" }
            };

            // Act
            var found = entities.Find(dc => dc.id == 2);

            // Assert
            Assert.NotNull(found);
            Assert.Equal("Entity Two", found.name);
        }

        #endregion
    }
}
