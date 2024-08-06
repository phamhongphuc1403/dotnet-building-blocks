using BuildingBlock.Core.Domain.Shared.Constants;

namespace Tests.Core.Domain.Constants;

public class CacheKeyRegistryTest
{
    public class ShouldSetValueToUpperCase
    {
        [Fact]
        public void CaseGetEmailConfirmationByUserIdKey()
        {
            // Arrange
            const string value = "value";
            var recordKey = CacheKeyRegistry.GetEmailConfirmationByUserIdKey(value);

            // Act
            var result = recordKey.Value;

            // Assert
            Assert.Equal($"email-{value}".ToUpper(), result);
        }

        [Fact]
        public void CaseGetRolesByUserIdKey()
        {
            // Arrange
            var value = "value";
            var recordKey = CacheKeyRegistry.GetRolesByUserIdKey(value);

            // Act
            var result = recordKey.Value;

            // Assert
            Assert.Equal($"role-{value}".ToUpper(), result);
        }

        [Fact]
        public void CaseGetPermissionsByRoleNameKey()
        {
            // Arrange
            var value = "value";
            var recordKey = CacheKeyRegistry.GetPermissionsByRoleNameKey(value);

            // Act
            var result = recordKey.Value;

            // Assert
            Assert.Equal($"permission-{value}".ToUpper(), result);
        }
    }
}